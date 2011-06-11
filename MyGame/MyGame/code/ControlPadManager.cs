using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace MyGame
{
    class ControlPadManager
    {
        ControlPadManager()
        {
        }

        static ControlPadManager instance = null;
        public static ControlPadManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ControlPadManager();
                }
                return instance;
            }
        }

        const int NUMBER_OF_CONTROLLERS = 4;

        public ControlPad[] controlPads { get; set; }
        public bool[] controllersUsed { get; set; }

        public void initialize()
        {
            controlPads = new ControlPad[NUMBER_OF_CONTROLLERS];
            controllersUsed = new bool[NUMBER_OF_CONTROLLERS];
            for (int i = 0; i < NUMBER_OF_CONTROLLERS; ++i)
            {
                controlPads[i] = new ControlPad();
                controllersUsed[i] = false;
            }

            // TODO apaño
            controllersUsed[0] = true;
        }

        public void update()
        {
            for (int i = 0; i < 4; ++i)
            {
                if (controllersUsed[i])
                {
                    controlPads[i].update(PlayerIndex.One + i); // as is not possible to pass only a int as argument, pass PlayerIndex with the offset
                }
            }
        }
    }
}