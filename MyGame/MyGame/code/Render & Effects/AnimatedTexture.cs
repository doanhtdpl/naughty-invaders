using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class AnimatedTexture
    {
        // each character can have multiple animated textures, so a number is needed to identify each texture
        public int id { get; set; }
        public string name { get; set; }
        public Texture2D texture { get; set; }
        public int columns { get; set; }
        public int rows { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
}
