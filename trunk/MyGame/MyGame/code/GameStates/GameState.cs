using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace MyGame
{
    public abstract class GameState
    {
        public bool renderAlways = true;
        public bool loaded = false;
        public bool loading = false;
        public bool gameState = false;
        public bool longLoad = false;
        public bool floating = false;
        public StateManager.tGS type;
        public float timeRunning = 0;
        public Color transitionColor = Color.Black;

        public abstract void initialize();
        public abstract void loadContent();
        public virtual void update()
        {
            timeRunning += SB.dt;
        }
        public abstract void render();
        public abstract void dispose();

        public virtual Vector2 getRespawnPosition(int playerNumber)
        {
            return Vector2.Zero;
        }
    }
}
