using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using System.Globalization;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class RenderableEntity2D : MovingEntity2D
    {
        // animations
        protected string newActionState = "";
        Texture2D texture;

        public Texture2D Texture
        {
            get { return texture; }
        }

        public RenderableEntity2D(string textureName, Vector3 position, Vector2 scale, float orientation)
            : base(textureName, position, scale, orientation)
        {
            this.texture = TextureManager.Instance.getTexture(textureName);
        }

        public override void update()
        {
        }

        public override void render()
        {
            texture.render(worldMatrix);
        }
    }
}
