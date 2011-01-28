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
        Texture texture;

        RenderableEntity2D(Texture texture, Vector3 position, Vector2 scale, float orientation):base(position, scale, orientation)
        {
            this.texture = texture;
        }

        public virtual void update()
        {
        }

        public virtual void render()
        {
            texture.render(worldMatrix);
        }
    }
}
