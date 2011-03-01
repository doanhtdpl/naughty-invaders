using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class Orange : Enemy
    {
        public enum tOrangeState { Wait, Start, Up, TopIdle, Down, End }

        const float UP_ACCELERATION = 50.0f;
        tOrangeState state;

        public Orange(Vector3 position, float orientation)
            : base("orange", position, orientation)
        {
            life = 10.0f;
            setCollisions();
            state = tOrangeState.Start;
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(0, 20), 40);
        }

        public override bool gotHitAtPart(int partIndex, float damage)
        {
            return false;
        }

        public override void update()
        {
            base.update();

            // update the parabolic move
            position += new Vector3(0, -UP_ACCELERATION, 0) * SB.dt;

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
                case tOrangeState.Start:
                break;
                case tOrangeState.Up:
                break;
                case tOrangeState.TopIdle:
                break;
                case tOrangeState.Down:
                break;
                case tOrangeState.End:
                break;
            }
        }

        public override void render()
        {
            base.render();
        }
    }
}
