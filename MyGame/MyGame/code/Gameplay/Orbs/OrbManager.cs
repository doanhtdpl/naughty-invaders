using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

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
            orbs = new List<Orb>();
        }
        public static OrbManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrbManager();
                    Orb.textures[(int)Orb.tOrb.XP] = TextureManager.Instance.getTexture("particles/OrbXP");
                    Orb.textures[(int)Orb.tOrb.Life] = TextureManager.Instance.getTexture("particles/OrbLife");
                }
                return instance;
            }
        }
        public List<Orb> orbs { get; set; }

        public void addRandomOrbs(int enemyLevel, Vector2 position)
        {
            int XP = Calc.randomNatural(0, enemyLevel);
            int life = 0;
            if (Calc.randomScalar() < 0.05f)
            {
                life = 1;
            }

            addOrbs(position, XP, life, 0, 0, false);
        }

        public void addOrbs(Vector2 position, int XP, int life, int wish, int pet, bool toPlayer = false)
        {
            for (int i = 0; i < XP; ++i)
            {
                addOrb(Orb.tOrb.XP, position, toPlayer);
            }
            for (int i = 0; i < life; ++i)
            {
                addOrb(Orb.tOrb.Life, position, toPlayer);
            }
        }
        public void addOrb(Orb.tOrb orbType, Vector2 position, bool toPlayer)
        {
            Orb orb = new Orb(orbType, position, toPlayer);
            orbs.Add(orb);
        }
	    public void update( )
        {
            Vector2 playerPosition = GamerManager.getMainPlayer().position2D;

	        // update each orb
	        for(int i=0; i<orbs.Count; ++i)
	        {
                // update time and delete the old orbs only if are not already attracted by the player
                if (!orbs[i].toPlayer)
                {
                    orbs[i].life -= SB.dt;
                    if (orbs[i].life < 0.0f)
                    {
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
                        GamerManager.getGamerEntities()[0].Player.addOrb(orbs[i].type);
                        orbs.RemoveAt(i);
                        --i;
                        continue;
                    }
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
            for (int i = 0; i < orbs.Count; ++i)
            {
                if (orbs[i].render)
                {
                    Orb.textures[(int)orbs[i].type].render(SB.getWorldMatrix(new Vector3(orbs[i].position, 0.0f), 0.0f, Orb.SIZE), Color.White);
                }
            }
        }

        public void clean()
        {
            orbs.Clear();
        }
    }
}
