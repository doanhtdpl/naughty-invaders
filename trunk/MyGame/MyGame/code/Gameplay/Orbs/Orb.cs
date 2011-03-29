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
        public const float LIFE_TIME = 8.0f;
        public static Texture texture;
        
	    public Vector2 position;
        public Vector2 velocity;
        public tOrb type;
        public Color color;
        public bool render;
        public float life;
        public bool toPlayer;

        public Orb(tOrb orbType, Vector2 position)
        {
            this.type = orbType;
            this.position = position;
            this.toPlayer = false;
            this.life = LIFE_TIME;
            this.render = true;

            this.velocity = Calc.randomDirection() * Calc.randomScalar() * 20.0f;

            switch (orbType)
            {
                case tOrb.XP:
                    this.color = Color.Yellow;
                    break;
                case tOrb.Life:
                    this.color = Color.Green;
                    break;
                case tOrb.Wish:
                    this.color = Color.Red;
                    break;
                case tOrb.Pet:
                    this.color = Color.Blue;
                    break;
            }
        }
    };
}
