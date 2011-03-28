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
        const float ORB_SPEED = 500.0f;

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
                orbs[i].life -= SB.dt;
                orbs[i].velocity -= orbs[i].velocity * Orb.FRICTION * SB.dt;
                orbs[i].position += orbs[i].velocity;

                if (orbs[i].toPlayer)
                {
                    orbs[i].position += Vector2.Normalize(playerPosition - orbs[i].position) * ORB_SPEED * SB.dt;
                    if (Vector2.DistanceSquared(playerPosition, orbs[i].position) < PICK_DISTANCE)
                    {
                        // delete de orb
                        orbs.RemoveAt(i);
                        --i;
                    }
                }
                else if (Vector2.DistanceSquared(playerPosition, orbs[i].position) < PLAYER_DISTANCE)
                {
                    orbs[i].toPlayer = true;
                }
	        }
        }

        public void render()
        {
            for (int i = 0; i < orbs.Count; ++i)
            {
                Orb.texture.render(SB.getWorldMatrix(new Vector3(orbs[i].position,0.0f), 0.0f, Orb.SIZE), orbs[i].color);
                Orb.texture.render(SB.getWorldMatrix(new Vector3(orbs[i].position, 0.0f), 0.0f, Orb.SIZE * 0.5f), Color.White);
            }
        }

        public void clear()
        {
            orbs.Clear();
        }
    }
}
