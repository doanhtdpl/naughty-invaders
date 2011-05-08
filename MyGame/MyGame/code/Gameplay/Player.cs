using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    class Player : CollidableEntity2D
    {
        public const float SPEED = 500;
        public const float INVULNERABLE_TIME = 3.0f;

        public const float DASH_FRICTION = 5.0f;
        public const float DASH_VELOCITY = 2000.0f;
        public const float DASH_SPEED_THRESHOLD = 500.0f;
        public const float DASH_PARTICLE_SPAWN_TIME = 0.03f;
        public const float MAX_BIG_SHOT_CHARGE = 2.0f;

        // life stuff
        public int lifes { get; set; }
        float invulnerableTime;
        
        // actions stuff
        float fastShotCooldownTime;
        float dashCooldownTime;
        Vector2 dashVelocity;
        float dashParticleTimer;
        float bigShotChargeTimer;
        bool bigShotCharging;
        float bigShotCooldownTime;

        public PlayerData data { get; set; }

        static Texture bigShotBall = null;

        public Player(string entityName, Vector3 position, float orientation)
            : base("characters", entityName, position, orientation, Color.White, 0)
        {
            entityState = tEntityState.Active;
            setCollisions();

            // life stuff
            lifes = 3;
            invulnerableTime = 0.0f;

            // actions stuff
            fastShotCooldownTime = 0.0f;
            dashCooldownTime = 0.0f;
            dashVelocity = Vector2.Zero;
            dashParticleTimer = 0.0f;
            bigShotChargeTimer = 0.0f;
            bigShotCharging = false;
            bigShotCooldownTime = 0.0f;

            data = new PlayerData();
            data.initNewData();

            if (bigShotBall == null)
            {
                bigShotBall = TextureManager.Instance.getTexture("projectiles/bigShotPlayer");
            }
        }

        public void initStage(Vector2 position)
        {
            position2D = position;
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(0, 0), 40);
        }

        public override bool gotHitAtPart(CollidableEntity2D ce, int partIndex)
        {
            if (invulnerableTime > 0.0f) return true;

            --lifes;
            if (lifes > 0)
            {
                invulnerableTime = INVULNERABLE_TIME;
            }
            return lifes > 0;
        }

        public void addOrb(Orb.tOrb type)
        {
            switch (type)
            {
                case Orb.tOrb.XP:
                    ++data.XP;
                    break;
                case Orb.tOrb.Life:
                    ++data.lifeOrbs;
                    break;
                case Orb.tOrb.Wish:
                    ++data.wishOrbs;
                    break;
                case Orb.tOrb.Pet:
                    ++data.petOrbs;
                    break;
            }
        }

        public void update(ControlPad controls)
        {
            base.update();

            // if invulnerable, update rendering
            if (invulnerableTime > 0.0f)
            {
                invulnerableTime -= SB.dt;
                if (invulnerableTime % 0.3f > 0.15f)
                {
                    renderState = tRenderState.NoRender;
                }
                else
                {
                    renderState = tRenderState.Render;
                }
            }

            fastShotCooldownTime -= SB.dt;
            bigShotCooldownTime -= SB.dt;
            dashCooldownTime -= SB.dt;

            Vector2 direction = Vector2.Zero;
            if (CameraManager.Instance.cameraMode == CameraManager.tCameraMode.Nodes)
            {
                position += CameraManager.Instance.getCameraVelocityXY();
            }

            if (dashVelocity != Vector2.Zero)
            {
                dashParticleTimer -= SB.dt;
                if (dashParticleTimer < 0.0f)
                {
                    dashParticleTimer = DASH_PARTICLE_SPAWN_TIME;
                    ParticleManager.Instance.addParticles("dash", position, new Vector3(dashVelocity * 0.05f, 0.0f), Color.White);
                }
                position2D += dashVelocity * SB.dt;

                if (dashVelocity.Length() < DASH_SPEED_THRESHOLD)
                {
                    dashVelocity = Vector2.Zero;
                }
            }
            else // in the middle of a dash the controls are blocked...
            {
                direction = controls.LS;
            }

            Vector2 nextPosition = position2D + direction * SB.dt * SPEED;

            GameplayHelper.Instance.updateEntityPosition(this, nextPosition, LevelManager.Instance.getLevelCollisions(), true);
            orientation = Calc.directionToAngle(new Vector2(controls.RS.X, controls.RS.Y));

            // dash move
            if (data.skills["dash1"].obtained)
            {
                // update dash velocity
                dashVelocity -= dashVelocity * DASH_FRICTION * SB.dt;

                if (controls.A_firstPressed() && dashCooldownTime <= 0.0f)
                {
                    //playAction("attack");
                    dashVelocity = direction * DASH_VELOCITY;
                    dashCooldownTime = data.getDashCooldown();
                }
            }
            // fast shot attack
            if (controls.X_pressed())
            {
                //EnemyManager.Instance.renderDebug();
                if (fastShotCooldownTime <= 0.0f)
                {
                    playAction("attack");
                    Projectile p = new PlayerFastShot(position);
                    fastShotCooldownTime = p.cooldown;
                    ProjectileManager.Instance.addProjectile(p);
                    ParticleManager.Instance.addParticles("playerFastShot", position + new Vector3(0, 50, 0), new Vector3(direction, 0.0f), Color.White);
                }
            }
            // big shot attack
            else if (data.skills["powerShot"].obtained)
            {
                if (controls.Y_firstPressed() && bigShotCooldownTime <= 0.0f)
                {
                    bigShotCharging = true;
                }

                if (bigShotCharging)
                {
                    if (controls.Y_pressed())
                    {
                        bigShotChargeTimer += SB.dt;
                    }
                    else if (controls.Y_released())
                    {
                        playAction("attack");
                        // value that goes from 1 to 3 (minimum and maximum charge)
                        float bigShotValue = MathHelper.Clamp(bigShotChargeTimer, 0.0f, MAX_BIG_SHOT_CHARGE) + 1.0f;
                        // from 0 to 1
                        float chargeValue = (bigShotValue - 1) * 0.5f;
                        Projectile p = new PlayerBigShot(position, bigShotValue, chargeValue);
                        ProjectileManager.Instance.addProjectile(p);
                        ParticleManager.Instance.addParticles("playerBigShot", position + new Vector3(0, 50, 0), new Vector3(direction, 0.0f), Color.White,
                            bigShotValue * 0.6f, (int)(20 * bigShotValue), bigShotValue);

                        bigShotCooldownTime = p.cooldown;
                        bigShotChargeTimer = 0.0f;
                        bigShotCharging = false;
                    }
                }
            }

            if (controls.RB_firstPressed())
            {
                EnemySpawnZone esz = new EnemySpawnZone("grape", new Rectangle(-300, 900, 600, 500), 3);
                EnemyManager.Instance.addEnemySpawnZone(esz);
            }
            if (controls.LT_firstPressed())
            {
                Trigger t = new Trigger();
                t.addFunction(true, "isPlayersLastLife", null);
                t.addFunction(false, "addParticles", null);
                TriggerManager.Instance.addTrigger(t);
            }
        }

        public override void render()
        {
            renderBigShot();
            base.render();
        }

        public void renderBigShot()
        {
            if (!bigShotCharging) return;
            
            float forceValue = (MathHelper.Clamp(bigShotChargeTimer, 0.0f, MAX_BIG_SHOT_CHARGE) / MAX_BIG_SHOT_CHARGE);

            if (forceValue == 1.0f)
            {
                forceValue += (float)Math.Sin((bigShotChargeTimer - MAX_BIG_SHOT_CHARGE) * 8.0f) * 0.05f;
            }

            bigShotBall.render( 
                SB.getWorldMatrix(position + new Vector3(0.0f, 60.0f, 0.0f), 0.0f, forceValue * 120.0f),
                new Color(forceValue, forceValue - 0.3f, forceValue - 0.3f, forceValue));
        }

        public override void requestDelete()
        {
            base.requestDelete();
            lifes = 3;
            playAction("idle", true);
        }
    }
}
