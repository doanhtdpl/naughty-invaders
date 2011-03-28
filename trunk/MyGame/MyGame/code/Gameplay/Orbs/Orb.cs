using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class Orb
    {
        public enum tOrb { XP, Life, Wish, Pet }

        public const float SIZE = 35.0f;
        public const float FRICTION = 10.0f;
        public static Texture texture;
        
	    public Vector2 position;
        public Vector2 velocity;
        public Color color;
        public float life;
        public bool toPlayer;

        public Orb(tOrb orbType, Vector2 position)
        {
            this.position = position;
            toPlayer = false;
            life = 5.0f;

            velocity = Calc.randomDirection() * Calc.randomScalar() * 20.0f;

            switch (orbType)
            {
                case tOrb.XP:
                    color = Color.Yellow;
                    break;
                case tOrb.Life:
                    color = Color.Green;
                    break;
                case tOrb.Wish:
                    color = Color.Red;
                    break;
                case tOrb.Pet:
                    color = Color.Blue;
                    break;
            }
        }
    };
}
