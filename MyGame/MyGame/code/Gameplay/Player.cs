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
        public static TEX sampleTex = new TEX();

        public const float SPEED = 300;

        float cooldownTime = 0.0f;

        bool selected = false;

        public Player(Vector3 position, Vector2 scale, float orientation, string entityName)
            : base(entityName, position, scale, orientation)
        {
        }

        public void update(ControlPad controls)
        {
            base.update();

            cooldownTime -= SB.dt;
            position2D += controls.LS * SB.dt * SPEED;
            orientation = Calc.directionToAngle(new Vector2(controls.RS.X, controls.RS.Y));

            if (controls.X_pressed() && cooldownTime <= 0.0f)
            {
                Projectile p = new BasicProjectile(position, new Vector2(50, 50), orientation);
                cooldownTime = p.cooldown;
                ProjectileManager.Instance.addProjectile(p);
            }

            if (controls.B_firstPressed())
            {
                newActionState = "lal";
            }
            if (controls.Y_firstPressed())
            {
                EnemyManager.Instance.addEnemy("grapes", this.position2D);
            }

            if (controls.A_firstPressed())
            {
                orientation += 0.1f;
            }

            //DebugManager.Instance.addLine(new Vector3(0, 0, 0), new Vector3(200, 200, 0), Color.Red);
            //DebugManager.Instance.addLine(new Vector3(0, 0, 0), new Vector3(200, -200, 0), Color.Green);
            //DebugManager.Instance.addLine(new Vector3(0, 0, 0), new Vector3(-200, 200, 0), Color.Blue);
            //DebugManager.Instance.addRectangle(new Vector2(0, 0), new Vector2(100, 100), Color.Red);
            //DebugManager.Instance.addRectangle(new Vector2(300, 300), new Vector2(200, 200), Color.Orange);
            //DebugManager.Instance.addRectangle(new Vector2(-100, 0), new Vector2(0, 100), Color.Green);

            //if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            //{
            //    Ray ray = EditorHelper.Instance.getMouseCursorRay();
            //    if (EditorHelper.Instance.rayVsEntity(ray, this))
            //    {
            //        selected = !selected;
            //    }
            //}
        }

        public override void render()
        {
            base.render();

            if (selected)
                EditorHelper.Instance.renderEntityQuad(this);
        }
    }
}
