using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame
{
    class AnimationAction
    {
        // from XML
        public string name { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public int initialFrame { get; set; }
        public int endFrame { get; set; }
        public float FPS { get; set; }
        public bool loops { get; set; }
        public int textureId { get; set; }
        public string playAtEnd { get; set; }

        public string playRandom { get; set; }  
        public float playRandomMin { get; set; }
        public float playRandomMax { get; set; }

        // calculated from XML data
        public int totalFrames { get; set; }

        public void initialize()
        {
            totalFrames = endFrame - initialFrame + 1;
        }
    }
}
