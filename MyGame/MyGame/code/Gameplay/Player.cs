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

        // life stuff
        int lifes;
        float invulnerableTime;
        
        // actions stuff
        float fastShotCooldownTime;
        float dashCooldownTime;
        Vector2 dashVelocity;
        float dashParticleTimer;
        float bigShotChargeTimer;

        PlayerData data = new PlayerData();

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

            data.initNewData();
        }

        public void initStage(Vector2 position)
        {
            position2D = position;
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(0, 0), 40);
        }

        public override bool gotHitAtPart(int partIndex, float damage)
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
            dashCooldownTime -= SB.dt;

            Vector2 direction = Vector2.Zero;
            position += CameraManager.Instance.getCameraVelocityXY();
            if (dashVelocity != Vector2.Zero)
            {
                dashParticleTimer -= SB.dt;
                if (dashParticleTimer < 0.0f)
                {
                    dashParticleTimer = DASH_PARTICLE_SPAWN_TIME;
                    ParticleManager.Instance.addParticles("dash", position, new Vector3(dashVelocity * 0.05f, 0.0f));
                }
                position2D += dashVelocity * SB.dt;

                if (dashVelocity.Length() < DASH_SPEED_THRESHOLD)
                {
                    dashVelocity = Vector2.Zero;
                }
            }
            else
            {
                direction = controls.LS;
            }

            Vector2 nextPosition = position2D + direction * SB.dt * SPEED;

            GameplayHelper.Instance.updateEntityPosition(this, nextPosition, LevelManager.Instance.getLevelCollisions(), true);
            orientation = Calc.directionToAngle(new Vector2(controls.RS.X, controls.RS.Y));

            // dash move
            if (data.skills["dash"].obtained)
            {
                // update dash velocity
                dashVelocity -= dashVelocity * DASH_FRICTION * SB.dt;

                if (controls.A_firstPressed() && dashCooldownTime <= 0.0f)
                {
                    //playAction("attack");
                    dashVelocity = direction * DASH_VELOCITY;
                    dashCooldownTime = PlayerData.DASH_COOLDOWN;
                }
            }
            // fast shot attack
            if (controls.X_pressed() && fastShotCooldownTime <= 0.0f)
            {
                playAction("attack");
                Projectile p = new PlayerProjectile(position);
                fastShotCooldownTime = p.cooldown;
                ProjectileManager.Instance.addProjectile(p);
                ParticleManager.Instance.addParticles("burst", position + new Vector3(0, 50, 0), new Vector3(direction, 0.0f));
            }
            // big shot attack
            if (data.skills["powerShot"].obtained)
            {
                if (controls.Y_firstPressed() && dashCooldownTime <= 0.0f)
                {
                    dashVelocity = direction * DASH_VELOCITY;
                    dashCooldownTime = PlayerData.DASH_COOLDOWN;
                }
            }
        }

        public override void render()
        {
            base.render();
        }
    }
}
