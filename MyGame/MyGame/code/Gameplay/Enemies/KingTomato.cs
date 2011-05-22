using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class KingTomato : Enemy
    {
        public enum tState { Commanding, Shitting, Recovering, Commanding2, Dying }
        public tState state { get; set; }

        const float LIFE = 500.0f;
        const float SHIT_TIME = 3.0f;
        const float SPAWN_ORB_TIME = 0.1f;

        Lifebar lifebar;
        float shitTimer = SHIT_TIME;

        
        float lastOrb = 0.0f;
        int orbsToSpawn = 250;

        public KingTomato(Vector3 position, float orientation)
            : base("kingTomato", position, orientation, 1)
        {
            life = LIFE;
            setCollisions();
            lifebar = new Lifebar("kingTomato", this, new Vector2(0.8f, 0.8f), new Vector2(0.0f, 170.0f));
            state = tState.Commanding;
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(0.0f, 0.0f), 80);
        }

        public override bool gotHitAtPart(CollidableEntity2D ce, int partIndex)
        {
            life -= ce.damage;
            return life > 0;
        }

        public override void die()
        {
            state = tState.Recovering;
        }

        public override void update()
        {
            base.update();

            switch (state)
            {
                case tState.Commanding:

                    if (life < 50.0f)
                    {
                        playAction("shitting");
                        state = tState.Shitting;
                    }
                    break;
                case tState.Shitting:
                    shitTimer -= SB.dt;
                    if (shitTimer < 0.0f)
                    {
                        CinematicManager.Instance.playCinematic("kingTomatoReturns");
                    }
                    break;
                case tState.Recovering:
                    if (life < 300.0f)
                    {
                        life += SB.dt * 100.0f;
                    }
                    else
                    {
                        state = tState.Commanding2;
                    }
                    break;
                case tState.Commanding2:

                    break;
                case tState.Dying:
                    lastOrb -= SB.dt;
                    if (lastOrb < 0.0f && orbsToSpawn > 0)
                    {
                        OrbManager.Instance.addOrbs(position2D, 10, 0, 0, 0, true);
                        lastOrb = SPAWN_ORB_TIME;
                        orbsToSpawn -= 10;
                    }
                    break;
            }
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
