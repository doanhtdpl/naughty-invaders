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
        public const float SPEED = 300;

        float cooldownTime = 0.0f;
        float life = 50.0f;

        public Player(string entityName, Vector3 position, float orientation)
            : base("characters", entityName, position, orientation)
        {
            active = true;
            setCollisions();
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(0, 0), 40);
        }

        public override bool gotHitAtPart(int partIndex, float damage)
        {
            life -= damage;
            //playAction("gotHit");
            return life > 0;
        }

        public void update(ControlPad controls)
        {
            base.update();

            position += CameraManager.Instance.getCameraVelocityXY();

            cooldownTime -= SB.dt;
            position2D += controls.LS * SB.dt * SPEED;
            orientation = Calc.directionToAngle(new Vector2(controls.RS.X, controls.RS.Y));

            if (controls.X_pressed() && cooldownTime <= 0.0f)
            {
                playAction("attack");
                Projectile p = new PlayerProjectile(position);
                cooldownTime = p.cooldown;
                ProjectileManager.Instance.addProjectile(p);
            }

            if (controls.A_firstPressed())
            {
                orientation += 0.1f;
            }
        }

        public override void render()
        {
            base.render();
        }
    }
}
