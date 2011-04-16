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
    public class RenderableEntity2D : Entity2D
    {
        Texture2D texture;
        public Color color;
        public bool flipHorizontal = false;
        public bool flipVertical = false;

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

            if(flipHorizontal || flipVertical)
            {

                Vector2 startUV = new Vector2(0.0f + (flipHorizontal ? 1.0f : 0.0f), 0.0f + (flipVertical ? 1.0f : 0.0f));
                Vector2 endUV = new Vector2(1.0f - (flipHorizontal ? 1.0f : 0.0f), 1.0f - (flipVertical ? 1.0f : 0.0f));

                texture.renderWithUVs(worldMatrix, startUV, endUV, color);
            }
            else
            {
                texture.render(worldMatrix, color);
            }
        }
    }
}
