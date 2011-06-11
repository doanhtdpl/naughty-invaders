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

        Vector3 velocity;
        
        public Orange(Vector3 position, float orientation)
            : base("orange", position, orientation, 3)
        {
            life = 10.0f;
            setCollisions();
            state = tOrangeState.Parabola;
        }

        public void initialize()
        {
            positionY = Camera2D.screen.Top - getRadius();

            float randomX = Calc.randomScalar(-4.0f, 4.0f);
            float randomY = Calc.randomScalar(16.0f, 19.0f);
            velocity = new Vector3(randomX, randomY, 8.0f);

            positionZ = -200.0f;
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(0, 20), 40);
        }

        public override bool gotHitAtPart(CollidableEntity2D ce, int partIndex)
        {
            ParticleManager.Instance.addParticles(entityName + "GotHit", this.position, Vector3.Zero, Color.White);
            SoundManager.Instance.playEffect(entityName + "GotHit");
            OrbManager.Instance.addOrbs(position2D,1,0,0,0);
            return true;
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
                    Vector3 acceleration = new Vector3(0.0f, ORANGE_GRAVITY, ORANGE_GRAVITY * 0.4f);
                    velocity += acceleration * SB.dt;
                    position += velocity;
                break;
            }
        }

        public override void render()
        {
            base.render();
        }
    }
}
