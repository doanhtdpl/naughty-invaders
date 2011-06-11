using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class Tomato : Enemy
    {
        const float MIN_SPEED = 400.0f;
        const float MAX_SPEED = 600.0f;
        const float LATERAL_SPEED = 1.0f;
        float speed;

        public Tomato(Vector3 position, float orientation)
            : base("tomato", position, orientation, 0.5f)
        {
            life = 10.0f;
            setCollisions();
            speed = Calc.randomScalar(MIN_SPEED, MAX_SPEED);
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(0.0f, 0.0f), 30);
        }

        public override void die()
        {
            base.die();
        }

        public override void update()
        {
            base.update();

            // always move down
            Vector3 posToAdd = new Vector3(0, -speed, 0) * SB.dt;
            // go to the player
            posToAdd.X += (GamerManager.getSessionOwner().Player.position.X - position.X) * LATERAL_SPEED * SB.dt;

            position += posToAdd;
        }

        public override void render()
        {
            base.render();
        }
    }
}
