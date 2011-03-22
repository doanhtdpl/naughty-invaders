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

        float cooldownTime;
        int lifes;
        float invulnerableTime;

        public Player(string entityName, Vector3 position, float orientation)
            : base("characters", entityName, position, orientation, Color.White, 0)
        {
            cooldownTime = 0.0f;
            entityState = tEntityState.Active;
            setCollisions();
            lifes = 3;
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

            position += CameraManager.Instance.getCameraVelocityXY();

            cooldownTime -= SB.dt;
            Vector2 nextPosition = position2D + controls.LS * SB.dt * SPEED;

            GameplayHelper.Instance.updateEntityPosition(this, nextPosition, LevelManager.Instance.getLevelCollisions(), true);
            orientation = Calc.directionToAngle(new Vector2(controls.RS.X, controls.RS.Y));

            KeyboardState currentKeyState = Keyboard.GetState();
            Vector2 keymove = Vector2.Zero;
            if (currentKeyState.IsKeyDown(Keys.Down))
                keymove.Y = -1;
            if (currentKeyState.IsKeyDown(Keys.Up))
                keymove.Y = 1;
            if (currentKeyState.IsKeyDown(Keys.Left))
                keymove.X = -1;
            if (currentKeyState.IsKeyDown(Keys.Right))
                keymove.X = 1;

            position2D += keymove * SB.dt * SPEED;

            if ((controls.X_pressed() || currentKeyState.IsKeyDown(Keys.A)) && cooldownTime <= 0.0f)
            {
                playAction("attack");
                Projectile p = new PlayerProjectile(position);
                cooldownTime = p.cooldown;
                ProjectileManager.Instance.addProjectile(p);
                ParticleManager.Instance.addParticles("burst", position + new Vector3(0, 50, 0), direction);
            }
        }

        public override void render()
        {
            base.render();
        }
    }
}
