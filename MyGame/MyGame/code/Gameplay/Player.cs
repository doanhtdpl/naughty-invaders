using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    class Player : AnimatedEntity2D
    {
        public const float SPEED = 300;

        float cooldownTime = 0.0f;

        bool selected = false;

        public Player(string entityName, Vector3 position, float orientation)
            : base("characters", entityName, position, orientation)
        {
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
                Projectile p = new BasicProjectile("playerProjectile", position, new Vector2(50, 50), orientation, Vector2.UnitY);
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
