using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class KingTomato : Enemy
    {
        public enum tState { Commanding, Shitting, Recovering, Commanding2, Dying, Delete }
        public tState state { get; set; }

        const float LIFE = 3000.0f;
        const float LIFE_RETURNS = 700;
        const float RECOVERING_LIFE_SPEED = 500.0f;
        const float SPAWN_ORB_TIME = 0.05f;
        const int ORBS_TO_SPAWN = 150;
        public const float SPEED = 200.0f;

        Lifebar lifebar;

        
        float lastOrb = 0.0f;
        int orbsToSpawn = ORBS_TO_SPAWN;

        public KingTomato(Vector3 position, float orientation)
            : base("kingTomato", position, orientation, 1)
        {
            life = LIFE;
            setCollisions();
            lifebar = new Lifebar("kingTomato", this, new Vector2(0.8f, 0.8f), new Vector2(0.0f, 170.0f), Color.White);
            state = tState.Commanding;
            avoidDelete = true;
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(0, 0), 80);
            addCollision(new Vector2(-80, -20), 70);
            addCollision(new Vector2(80, -20), 70);
        }

        public override bool gotHitAtPart(CollidableEntity2D ce, int partIndex)
        {
            if (state == tState.Shitting || state == tState.Recovering) return true;
            return base.gotHitAtPart(ce, partIndex);
        }

        public override void die()
        {
            playAction("die");
            entityState = tEntityState.Dying;
        }

        public override void update()
        {
            base.update();

            switch (state)
            {
                case tState.Commanding:

                    if (life < 50.0f)
                    {
                        playAction("afraid");
                        state = tState.Shitting;
                    }
                    break;
                case tState.Shitting:
                    // TODO super ugly that state is changed in minigame king tomate
                    break;
                case tState.Recovering:
                    if (life < LIFE_RETURNS)
                    {
                        life += SB.dt * RECOVERING_LIFE_SPEED;
                    }
                    else
                    {
                        if(CinematicManager.Instance.cinematicToPlay == null)
                            state = tState.Commanding2;
                    }
                    break;
                case tState.Commanding2:
                    Vector3 directionTo = (GamerManager.getSessionOwner().Player.position - position);
                    directionTo.Normalize();
                    position += directionTo * SPEED * SB.dt;
                    if (life <= 0.0f)
                    {
                        state = tState.Dying;
                    }
                    break;
                case tState.Dying:
                    lastOrb -= SB.dt;
                    if (lastOrb < 0.0f && orbsToSpawn > 0)
                    {
                        OrbManager.Instance.addOrbs(position2D, 5, 0, 0, 0, true);
                        lastOrb = SPAWN_ORB_TIME;
                        orbsToSpawn -= 5;
                    }
                    if (orbsToSpawn <= 0)
                    {
                        ParticleManager.Instance.addParticles("kingTomatoExplode", position, Vector3.Zero, Color.White);
                        state = tState.Delete;
                    }
                    break;
                case tState.Delete:
                    break;
            }
        }

        public override void render()
        {
            base.render();
            lifebar.lifePercentage = life / LIFE;
            if (state != tState.Delete)
            {
                GraphicsManager.Instance.spriteBatchBegin();
                lifebar.render();
                GraphicsManager.Instance.spriteBatchEnd();
            }
        }
    }
}
