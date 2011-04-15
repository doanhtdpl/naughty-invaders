using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class Orange : Enemy
    {
        public enum tOrangeState { Wait, Parabola }

        const float ORANGE_GRAVITY = -10.0f;
        tOrangeState state;

        Vector2 velocity;
        
        public Orange(Vector3 position, float orientation)
            : base("orange", position, orientation)
        {
            life = 10.0f;
            setCollisions();
            state = tOrangeState.Parabola;

            velocity = new Vector2(Calc.randomScalar(-4.0f, 4.0f), Calc.randomScalar(12.0f, 17.0f));
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(0, 20), 40);
        }

        public override bool gotHitAtPart(CollidableEntity2D ce, int partIndex)
        {
            return false;
        }

        public override void update()
        {
            base.update();

            switch (state)
            {
                case tOrangeState.Wait:
                    Vector2 playerPosition = GamerManager.getGamerEntities()[0].Player.position2D;
                    if (playerPosition.X < Camera2D.getScreenCenter().X)
                    {
                        position2D = new Vector2(Camera2D.getScreenCenter().X + 200.0f, Camera2D.getScreenLeftBottomCorner().Y - 100.0f);
                    }
                    else
                    {
                        position2D = new Vector2(Camera2D.getScreenCenter().X - 200.0f, Camera2D.getScreenLeftBottomCorner().Y - 100.0f);
                    }
                break;
                case tOrangeState.Parabola:
                    Vector2 acceleration = new Vector2(0.0f, 0.0f + ORANGE_GRAVITY);
                    velocity += acceleration * SB.dt;
                    position2D += velocity;
                break;
            }
        }

        public override void render()
        {
            base.render();
        }
    }
}
