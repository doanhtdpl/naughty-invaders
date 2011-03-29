using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class OrbManager
    {
        const float PLAYER_DISTANCE = 50000.0f;
        const float PICK_DISTANCE = 900.0f;
        const float ORB_SPEED = 800.0f;
        const float ORB_IDLE_SPEED = 1.0f;

        const float LIFE_ORB_PROBABILITY = 0.1f;
        const float WISH_ORB_PROBABILITY = 0.45f;

        static OrbManager instance = null;
        OrbManager()
        {
        }
        public static OrbManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrbManager();
                    Orb.texture = TextureManager.Instance.getTexture("particles/Orb");
                }
                return instance;
            }
        }
	    List<Orb> orbs = new List<Orb>();

        public void addRandomOrbs(int enemyLevel, Vector2 position)
        {
            int XP = enemyLevel * 2;
            if (Calc.randomScalar() < LIFE_ORB_PROBABILITY)
            {
                addOrbs(position, XP, enemyLevel, 0, 0);
            }
            else if (Calc.randomScalar() < LIFE_ORB_PROBABILITY + WISH_ORB_PROBABILITY)
            {
                addOrbs(position, XP, 0, enemyLevel, 0);
            }
            else
            {
                addOrbs(position, XP, 0, 0, enemyLevel);
            }
        }

        public void addOrbs(Vector2 position, int XP, int life, int wish, int pet)
        {
            for (int i = 0; i < XP; ++i)
            {
                addOrb(Orb.tOrb.XP, position);
            }
            for (int i = 0; i < life; ++i)
            {
                addOrb(Orb.tOrb.Life, position);
            }
            for (int i = 0; i < wish; ++i)
            {
                addOrb(Orb.tOrb.Wish, position);
            }
            for (int i = 0; i < pet; ++i)
            {
                addOrb(Orb.tOrb.Pet, position);
            }
        }
        public void addOrb(Orb.tOrb orbType, Vector2 position)
        {
            Orb orb = new Orb(orbType, position);
            orbs.Add(orb);
        }
	    public void update( )
        {
            Vector2 playerPosition = GamerManager.getGamerEntities()[0].Player.position2D;

	        // update each orb
	        for(int i=0; i<orbs.Count; ++i)
	        {
                // update time and delete the old orbs only if are not already attracted by the player
                if (!orbs[i].toPlayer)
                {
                    orbs[i].life -= SB.dt;
                    if (orbs[i].life < 0.0f)
                    {
                        GamerManager.getGamerEntities()[0].Player.addOrb(orbs[i].type);
                        orbs.RemoveAt(i);
                        --i;
                        continue;
                    }
                }
                else
                {
                    orbs[i].life = 5.0f;
                }

                // update velocity and position
                orbs[i].velocity -= orbs[i].velocity * Orb.FRICTION * SB.dt;
                orbs[i].position += orbs[i].velocity;

                orbs[i].position += Vector2.UnitY * 20.0f * (float)Math.Sin(orbs[i].life * 8.0f) * SB.dt;

                if (orbs[i].toPlayer)
                {
                    orbs[i].position += Vector2.Normalize(playerPosition - orbs[i].position) * ORB_SPEED * SB.dt;
                    if (Vector2.DistanceSquared(playerPosition, orbs[i].position) < PICK_DISTANCE)
                    {
                        // pick the orb and delete it
                        orbs.RemoveAt(i);
                        --i;
                        continue;
                    }
                    orbs[i].color.A = 255;
                }
                else if (Vector2.DistanceSquared(playerPosition, orbs[i].position) < PLAYER_DISTANCE)
                {
                    orbs[i].toPlayer = true;
                }
                else
                {
                    if (orbs[i].life < 1.0f)
                    {
                        orbs[i].render = orbs[i].life % 0.15f > 0.075f;
                    }
                    else if (orbs[i].life < 2.0f)
                    {
                        orbs[i].render = orbs[i].life % 0.2f > 0.1f;
                    }
                }
	        }
        }

        public void render()
        {
            // TODO: organize a RenderClass with the graphicsDevice and put predefined BlendStates
            BlendState bs = new BlendState();

            bs.AlphaBlendFunction = BlendFunction.Add;
            bs.ColorDestinationBlend = Blend.One;
            bs.AlphaDestinationBlend = Blend.One;
            BlendState backup = SB.graphicsDevice.BlendState;
            SB.graphicsDevice.BlendState = bs;

            for (int i = 0; i < orbs.Count; ++i)
            {
                if (orbs[i].render)
                {
                    Orb.texture.render(SB.getWorldMatrix(new Vector3(orbs[i].position, 0.0f), 0.0f, Orb.SIZE), orbs[i].color);
                }
            }
            SB.graphicsDevice.BlendState = backup;
        }

        public void clear()
        {
            orbs.Clear();
        }
    }
}
