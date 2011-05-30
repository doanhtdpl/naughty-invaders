using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    public class Player : CollidableEntity2D
    {
        public GamerEntity owner { get; set; }

        public enum tMode { Arcade, GarlicGun, SavingItems }
        public tMode mode { get; set; }

        public const float SPEED = 500;
        public const float INVULNERABLE_TIME = 3.0f;

        public const float DASH_FRICTION = 5.0f;
        public const float DASH_VELOCITY = 2000.0f;
        public const float DASH_SPEED_THRESHOLD = 500.0f;
        public const float DASH_PARTICLE_SPAWN_TIME = 0.03f;
        public const float MAX_BIG_SHOT_CHARGE = 2.0f;

        Vector2 shotDirection;

        // life stuff
        const int MAX_LIFES = 5;
        Lifebar[] lifebars = new Lifebar[MAX_LIFES];
        public int lifes { get; set; }
        public float lifeValue;
        float invulnerableTime;
        
        // actions stuff
        float garlicGunCooldownTime;
        float fastShotCooldownTime;
        float dashCooldownTime;
        Vector2 dashVelocity;
        float dashParticleTimer;
        float bigShotChargeTimer;
        bool bigShotCharging;
        float bigShotCooldownTime;

        static Texture2D starXP = null;
        static Texture bigShotBall = null;
        static Texture garlicGunTexture = null;
        static Texture garlicGunBandTexture = null;


        public Player( GamerEntity owner, string entityName, Vector3 position, float orientation)
            : base("characters", entityName, position, orientation, Color.White, 0)
        {
            this.owner = owner;

            entityState = tEntityState.Active;
            setCollisions();

            // life stuff
            lifebars[0] = new Lifebar("wish", this, new Vector2(0.25f, 0.25f), new Vector2(0.0f, -80.0f));
            lifebars[1] = new Lifebar("wish", this, new Vector2(0.34f, 0.34f), new Vector2(0.0f, -93.0f));
            lifebars[2] = new Lifebar("wish", this, new Vector2(0.43f, 0.43f), new Vector2(0.0f, -110.0f));
            lifes = 2;
            lifeValue = lifes;
            invulnerableTime = 0.0f;

            // actions stuff
            fastShotCooldownTime = 0.0f;
            dashCooldownTime = 0.0f;
            dashVelocity = Vector2.Zero;
            dashParticleTimer = 0.0f;
            bigShotChargeTimer = 0.0f;
            bigShotCharging = false;
            bigShotCooldownTime = 0.0f;

            mode = tMode.Arcade;

            if (bigShotBall == null) bigShotBall = TextureManager.Instance.getTexture("projectiles/bigShotPlayer");
            if (starXP == null) starXP = TextureManager.Instance.getTexture("GUI/menu/starXP");
            if (garlicGunTexture == null) garlicGunTexture = TextureManager.Instance.getTexture("characters/garlicGun");
            if (garlicGunBandTexture == null) garlicGunBandTexture = TextureManager.Instance.getTexture("characters/garlicGunBand");
        }

        public void initLevel()
        {
            orientation = 0.0f;
            lifeValue = lifes;
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(0, 0), 40);
        }

        public override bool gotHitAtPart(CollidableEntity2D ce, int partIndex)
        {
            if (invulnerableTime > 0.0f) return true;

            lifeValue -= 1.0f;
            if (lifeValue > 0)
            {
                invulnerableTime = INVULNERABLE_TIME;
            }
            return lifeValue > 0;
        }

        public void addLifeToMax()
        {
            lifes++;
            lifeValue += 1.0f;
        }
        public void addOrb(Orb.tOrb type)
        {
            switch (type)
            {
                case Orb.tOrb.XP:
                    ++owner.data.XP;
                    ++owner.data.totalXP;
                    break;
                case Orb.tOrb.Life:
                    ++owner.data.lifeOrbs;
                    lifeValue += 0.1f;
                    if (lifeValue > lifes)
                    {
                        lifeValue = lifes;
                    }
                    break;
            }
        }

        public void activateGarlicGun()
        {
            ParticleManager.Instance.addParticles("newItem", position + new Vector3(0, 0, 0), Vector3.Zero, Color.Red, 2.0f, 100);
            shotDirection = Vector2.UnitY;
            mode = tMode.GarlicGun;
        }

        public void updateArcadeMode(Vector2 direction, ControlPad controls)
        {
            // dash move
            if (owner.data.skills["dash1"].obtained)
            {
                // update dash velocity
                dashVelocity -= dashVelocity * DASH_FRICTION * SB.dt;

                if (controls.A_firstPressed() && dashCooldownTime <= 0.0f)
                {
                    //playAction("attack");
                    dashVelocity = direction * DASH_VELOCITY;
                    dashCooldownTime = owner.data.getDashCooldown();
                }
            }
            // fast shot attack
            if (controls.X_pressed())
            {
                //EnemyManager.Instance.renderDebug();
                if (fastShotCooldownTime <= 0.0f)
                {
                    playAction("attack");
                    Projectile p = null;
                    // applies to damage and scale of projectile and particles
                    float projectileFactor = 1.0f;
                    Color projectileColor = Color.White;
                    if (owner.data.skills["plasma"].obtained)
                    {
                        projectileFactor = 1.2f;
                        projectileColor = Color.Yellow;
                    }

                    p = new PlayerFastShot(position, projectileFactor, projectileColor);
                    ParticleManager.Instance.addParticles("playerFastShot", position + new Vector3(0, 50, 0), new Vector3(direction, 0.0f), projectileColor, projectileFactor);

                    fastShotCooldownTime = p.cooldown;
                    ProjectileManager.Instance.addProjectile(p);
                }
            }
            // big shot attack
            else if (owner.data.skills["powerShot"].obtained)
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
        }
        public void updateGarlicGunMode(ControlPad controls)
        {
            garlicGunCooldownTime -= SB.dt;

            // orient the player in the direction of move
            if (controls.getLS().LengthSquared() > 0.01f)
            {
                orientation = Calc.directionToAngle(new Vector2(controls.getLS().X, controls.getLS().Y)) - Calc.PiOver2;
            }

            // controls of garlic gun
            if (controls.getRS().LengthSquared() > 0.01f)
            {
                shotDirection = controls.getRS();
                shotDirection.Normalize();

                if (garlicGunCooldownTime <= 0.0f)
                {
                    playAction("attack");
                    Projectile p = new GarlicGunShot(position, shotDirection);
                    garlicGunCooldownTime = p.cooldown;
                    ProjectileManager.Instance.addProjectile(p);
                    Vector3 particlesDirection = (shotDirection.toVector3() * 700.0f) + Calc.randomVector3(new Vector3(30.0f, 30.0f, 30.0f), new Vector3(30.0f, 30.0f, 30.0f));
                    ParticleManager.Instance.addParticles("playerGarlicShot", position + (shotDirection.toVector3() * 100.0f), particlesDirection, Color.White);
                }
            }
        }
        public void updateInvulnerableAfterHit()
        {
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
        }
        void updateCameraMove()
        {
            if (CameraManager.Instance.cameraMode == CameraManager.tCameraMode.Nodes)
            {
                position += CameraManager.Instance.getCameraVelocityXY();
            }
        }
        void updateDash()
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
        public void update(ControlPad controls)
        {
            base.update();

            if (updateState == tUpdateState.NoUpdate) return;

            updateInvulnerableAfterHit();

            fastShotCooldownTime -= SB.dt;
            bigShotCooldownTime -= SB.dt;
            dashCooldownTime -= SB.dt;

            updateCameraMove();

            Vector2 direction = Vector2.Zero;
            if (dashVelocity != Vector2.Zero)
            {
                updateDash();
            }
            else // in the middle of a dash the controls are blocked...
            {
                if (mode == tMode.SavingItems)
                {
                    direction = controls.getLS();
                    direction.Y = 0.0f;
                    if (direction.X > 0.75f) direction.X = 1.0f;
                    if (direction.X < -0.75f) direction.X = -1.0f;
                }
                else
                {
                    direction = controls.getLS();
                }
            }

            Vector2 nextPosition = position2D + direction * SB.dt * SPEED;
            GameplayHelper.Instance.updateEntityPosition(this, nextPosition, LevelManager.Instance.getLevelCollisions(), true);

            switch (mode)
            {
                case tMode.Arcade:
                    updateArcadeMode(direction, controls);
                    break;
                case tMode.GarlicGun:
                    updateGarlicGunMode(controls);
                    break;
                case tMode.SavingItems:
                    break;
            }

#if DEBUG
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
            if (controls.RT_pressed())
            {
                lifeValue -= 0.01f;
            }
#endif
        }

        public void renderGUI()
        {
            if (renderState == tRenderState.NoRender) return;

            Viewport viewport = GraphicsManager.Instance.graphicsDevice.Viewport;
            Vector3 projectedPosition = viewport.Project(position + new Vector3(60, -50.0f, 0), Camera2D.projection, Camera2D.view, Matrix.Identity);
            starXP.render2D(projectedPosition.toVector2(), new Vector2(20.0f, 20.0f), Color.White, 0.0f, SpriteEffects.None, 1.0f, false);
            StringManager.render(owner.data.XP.ToString(), projectedPosition.toVector2() + new Vector2(10, -10), 0.5f, Color.Yellow, StringManager.tTextAlignment.Left, SB.font, 1000, 1000, Color.White, 1.0f, Vector2.Zero, StringManager.tStyle.Normal);

            switch (mode)
            {
                case tMode.Arcade:
                case tMode.GarlicGun:
                    // render lifebars
                    for (int i = 0; i < lifes; ++i)
                    {
                        if (lifebars[i] != null)
                        {
                            lifebars[i].lifePercentage = lifeValue - i;
                            lifebars[i].render(true);
                        }
                    }
                    break;
                case tMode.SavingItems:
                    break;
            }
        }
        public override void render()
        {
            if (renderState == tRenderState.NoRender) return;

            renderBigShot();
            base.render();
            if (mode == tMode.GarlicGun)
            {
                garlicGunBandTexture.render(position, orientation, 105.0f, Color.White);
                garlicGunTexture.render(position, Calc.directionToAngle(shotDirection) - Calc.PiOver2, 220.0f, Color.White);
            }
        }
        public void renderBigShot()
        {
            if (!bigShotCharging) return;
            
            float forceValue = (MathHelper.Clamp(bigShotChargeTimer, 0.0f, MAX_BIG_SHOT_CHARGE) / MAX_BIG_SHOT_CHARGE);

            if (forceValue == 1.0f)
            {
                forceValue += (float)Math.Sin((bigShotChargeTimer - MAX_BIG_SHOT_CHARGE) * 8.0f) * 0.05f;
            }

            bigShotBall.render(position + new Vector3(0.0f, 60.0f, 0.0f), 0.0f, forceValue * 120.0f,
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
