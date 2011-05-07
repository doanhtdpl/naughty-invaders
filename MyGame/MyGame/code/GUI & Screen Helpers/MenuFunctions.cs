using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace MyGame
{
    class MenuFunctions
    {
        public static void unpause()
        {
            StateManager.dequeueState(1);
        }
        public static void exitGame()
        {
            StateManager.clearStates();
            StateManager.enqueueState(StateManager.tGS.Menu);
            //SoundManager.playSound("pause");
        }
    }
}