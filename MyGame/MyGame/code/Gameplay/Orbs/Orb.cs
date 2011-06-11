﻿using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class Orb
    {
        public enum tOrb { XP = 0, Life }
        const int ORB_TYPES = 2;

        public const float FRICTION = 10.0f;
        public const float LIFE_TIME = 8.0f;
        public static Texture2D[] textures = new Texture2D[ORB_TYPES];
        public static float[] sizes = new float[ORB_TYPES];
        
	    public Vector2 position;
        public Vector2 velocity;
        public tOrb type;
        public bool render;
        public float life;
        public bool toPlayer;

        public Orb(tOrb orbType, Vector2 position, bool toPlayer, float speed)
        {
            this.type = orbType;
            this.position = position;
            this.toPlayer = toPlayer;
            this.life = LIFE_TIME;
            this.render = true;

            this.velocity = Calc.randomDirection() * Calc.randomScalar() * speed;
        }

        public static void initializeOrbSizes()
        {

        }
    };
}
