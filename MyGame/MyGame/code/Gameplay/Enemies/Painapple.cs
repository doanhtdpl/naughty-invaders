using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class Painapple : Enemy
    {
        float LIFE = 5000;

        Lifebar lifebar;

        Vector3 initPos, wantedPos;

        enum tPainAppleStates { COME, IDLE, GO };
        tPainAppleStates state;

        public Painapple(Vector3 position, float orientation)
            : base("painapple", position, orientation, 1)
        {
            life = LIFE;
            setCollisions();

            lifebar = new Lifebar("macedonia", this, new Vector2(0.6f, 0.6f), new Vector2(0.0f, 140.0f), Color.White);

            changeState(tPainAppleStates.COME);
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

            switch (state)
            {
                case tPainAppleStates.COME:
                    break;
                case tPainAppleStates.IDLE:
                    break;
                case tPainAppleStates.GO:
                    break;
            }
        }

        void changeState(tPainAppleStates newState)
        {
            switch (state)
            {
                case tPainAppleStates.COME:
                    playAction("Idle");
                    break;
                case tPainAppleStates.IDLE:
                    playAction("Laugh");
                    break;
                case tPainAppleStates.GO:
                    playAction("Idle");
                    break;
            }
            state = newState;
        }

        public override void render()
        {
            base.render();

            lifebar.lifePercentage = life / LIFE;
            GraphicsManager.Instance.spriteBatchBegin();
            lifebar.render();
            GraphicsManager.Instance.spriteBatchEnd();
        }
    }
}
