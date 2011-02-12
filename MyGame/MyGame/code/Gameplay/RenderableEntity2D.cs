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

        public RenderableEntity2D( string entityFolder, string textureName, Vector3 position, float orientation)
            : base(textureName, position, orientation)
        {
            this.texture = TextureManager.Instance.getTexture(entityFolder, textureName);
            scale2D = new Vector2(this.texture.Width, this.texture.Height);
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
