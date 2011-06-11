using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class Lifebar
    {
        Texture2D back;
        Vector2 backScale;
        Texture2D front;
        Vector2 frontScale;
        Entity2D reference;
        Vector2 offset;
        Color color;

        public float lifePercentage { get; set; }

        public Lifebar(string name, Entity2D reference, Vector2 scale, Vector2 offset, Color color)
        {
            back = TextureManager.Instance.getTexture("GUI/ingame/" + name + "Back");
            backScale.X = back.Width * scale.X;
            backScale.Y = back.Height * scale.Y;
            front = TextureManager.Instance.getTexture("GUI/ingame/" + name + "Front");
            frontScale.X = front.Width * scale.X;
            frontScale.Y = front.Height * scale.Y;

            this.color = color;

            this.reference = reference;
            this.offset = offset;
            lifePercentage = 1.0f;
            Viewport viewport = GraphicsManager.Instance.graphicsDevice.Viewport;
        }

        public void render(bool flipVertical = false)
        {
            Viewport viewport = GraphicsManager.Instance.graphicsDevice.Viewport;
            Vector3 projectedPosition = viewport.Project(reference.position + offset.toVector3(), Camera2D.projection, Camera2D.view, Matrix.Identity);
            SpriteEffects se = SpriteEffects.None;
            if (flipVertical)
            {
                se = SpriteEffects.FlipVertically;
            }
            back.render2D(projectedPosition.toVector2(), backScale, color, 0.0f, se, 1.0f, false);
            front.render2D(projectedPosition.toVector2(), frontScale, color, 0.0f, se, lifePercentage, false);
        }
    }
}
