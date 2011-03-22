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
    public class RenderableEntity2D : MovingEntity2D
    {
        // animations
        protected string newActionState = "";
        Texture2D texture;
        public Color color { get; set; }

        public enum tRenderState { Render, NoRender }
        public tRenderState renderState { get; set; }

        public Texture2D Texture
        {
            get { return texture; }
        }

        public RenderableEntity2D( string entityFolder, string textureName, Vector3 position, float orientation, Color color, int id = -1)
            : base(textureName, position, orientation, id)
        {
            // if its an animated entity, it doesnt have a texture
            if (entityFolder != "animated")
            {
                this.texture = TextureManager.Instance.getTexture(entityFolder, textureName);
                scale2D = new Vector2(this.texture.Width, this.texture.Height);
            }
            this.color = color;
        }

        public override void update()
        {
        }

        public override void render()
        {
            if (renderState == tRenderState.NoRender) return;

            texture.render(worldMatrix, color);
        }
    }
}
