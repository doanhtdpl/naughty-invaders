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

        public Player(string name, Vector2 size)
        {
            position = Vector2.Zero;
            this.size = size;
            this.size = new Vector2(100, 100);

            sampleTex.initTEX( TextureManager.Instance.getColoredTexture(Color.White), 50, 50 );

            base.initialize(name);
            base.loadContent();
        }

        public void update(ControlPad controls)
        {
            base.update();

            cooldownTime -= SB.dt;
            position += controls.LS * SB.dt * SPEED;
            direction = controls.RS;
            orientation = Calc.directionToAngle(direction);

            if (controls.X_pressed() && cooldownTime <= 0.0f)
            {
                Projectile p = new BasicProjectile();
                p.position = position;
                p.size = new Vector2(20, 20);
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
