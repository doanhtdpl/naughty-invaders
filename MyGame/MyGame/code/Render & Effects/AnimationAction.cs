using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame
{
    public class RandomAction
    {
        public string name { get; set; }
        public float probability { get; set; }
    }

    public class AnimationAction
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

        public bool playRandom { get; set; }
        public float playRandomMin { get; set; }
        public float playRandomMax { get; set; }
        public List<RandomAction> randomActions { get; set; }

        // calculated from XML data
        public int totalFrames { get; set; }

        public void initialize()
        {
            totalFrames = endFrame - initialFrame + 1;
        }

        public float getDuration()
        {
            return (endFrame - initialFrame) * (1 / FPS);
        }
    }
}
