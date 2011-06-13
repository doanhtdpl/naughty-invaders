using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class MacedoniaFruit : RenderableEntity2D
    {
        Vector3 gravity = new Vector3(0, -1500, 0);
        Vector3 direction;

        bool deadRequest;
        bool dead;
        float timeToDie;
        float timeAlive;

        public MacedoniaFruit(string name, Vector3 position)
            : base("projectiles", name, position, 0, Color.White)
        {
            direction = new Vector3(Calc.randomScalar(-450, 450), 300, 0);

            scale *= 0.7f;

            deadRequest = false;
            dead = false;
        }

        public override void update()
        {
            if (dead)
                return;

            base.update();

            timeAlive += SB.dt;
            if (timeAlive > 2.5f)
            {
                dead = true;
                return;
            }

            if (deadRequest)
            {
                timeToDie -= SB.dt;
                if (timeToDie < 0)
                    dead = true;
            }

            direction += gravity * SB.dt;
            position += direction * SB.dt;

            orientation += SB.dt * 5;
        }

        public override void render()
        {
            if(!dead && !deadRequest)
                base.render();
        }

        public void explode(bool epileptic = false)
        {
            if (!deadRequest)
            {
                deadRequest = true;
                timeToDie = 0.2f;
                if(epileptic)
                    ParticleManager.Instance.addParticles("getFruit", position + new Vector3(40, -10, 0), Vector3.Zero, Color.White);
                else
                    ParticleManager.Instance.addParticles("getFruit", position, Vector3.Zero, Color.White);
            }
        }

        public bool isDead()
        {
            return dead;
        }

        public bool isDying()
        {
            return deadRequest;
        }
    }
}
