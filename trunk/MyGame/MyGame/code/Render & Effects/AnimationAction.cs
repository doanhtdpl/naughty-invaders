using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame
{
    class AnimationAction
    {
        public string name { get; set; }
        public int initialFrame { get; set; }
        public int endFrame { get; set; }
        public float speed { get; set; }
        public bool loops { get; set; }
        public int textureId { get; set; }
    }
}
