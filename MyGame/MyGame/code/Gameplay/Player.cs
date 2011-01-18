using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class Player : AnimatedEntity2D
    {
        public static TEX sampleTex = new TEX();

        public const float SPEED = 300;

        float cooldownTime = 0.0f;

        public Player(Vector3 position, Vector2 scale, float orientation, string entityName)
            : base(position, scale, orientation, entityName)
        {
        }

        public void update(ControlPad controls)
        {
            base.update();

            cooldownTime -= SB.dt;
            position2D += controls.LS * SB.dt * SPEED;
            direction2D = controls.RS;
            orientation = Calc.directionToAngle(new Vector2(direction.X, direction.Y));

            if (controls.X_pressed() && cooldownTime <= 0.0f)
            {
                Projectile p = new BasicProjectile();
                p.initializeWorldMatrix2D(position, new Vector2(50, 50), orientation);
                cooldownTime = p.cooldown;
                ProjectileManager.Instance.addProjectile(p);
            }

            if (controls.B_firstPressed())
            {
                newActionState = "lal";
            }
            if (controls.Y_firstPressed())
            {
                newActionState = "laughing";
            }
        }

        public override void render()
        {
            base.render();
        }
    }
}
