using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Reflection;

namespace MyGame
{
    public class MenuText
    {
        public string text { get; set; }
        public float scale { get; set; }
        public Vector2 position { get; set; }

        public MenuText(string text, Vector2 position, float scale)
        {
            this.text = text;
            this.position = Screen.getXYfromCenter(position);
            this.scale = scale;
        }

        public void render()
        {
            text.renderNI(position, scale);
        }
    }

    public class MenuElement
    {
        Rectangle toDraw;
        Texture2D texture;
        Vector2 position;
        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                updateToDrawRectangle();
            }
        }
        Vector2 scale;
        public Vector2 Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
                updateToDrawRectangle();
            }
        }

        public enum tInputType { A = 0, Up, Down, Right, Left }
        const int INPUTS = 5;
        MethodInfo[] functions;

        public MenuElement upNode { set; get; }
        public MenuElement downNode { set; get; }

        public MenuElement(string textureName, Vector2 position, Vector2 scale)
        {
            this.texture = TextureManager.Instance.getTexture("GUI/menu", textureName);
            this.position = Screen.getXYfromCenter(position);
            this.scale = scale;
            updateToDrawRectangle();

            upNode = null;
            downNode = null;

            functions = new MethodInfo[INPUTS];
            for (int i = 0; i < INPUTS; ++i)
            {
                functions[i] = null;
            }
        }

        void updateToDrawRectangle()
        {
            float scaledWidth = texture.Width * scale.X;
            float scaledHeight = texture.Height * scale.Y;

            toDraw = new Rectangle(
                (int)(this.position.X - scaledWidth * 0.5f),
                (int)(this.position.Y - scaledHeight * 0.5f),
                (int)scaledWidth, (int)scaledHeight);
        }

        public void setFunction(string functionName, tInputType functionType)
        {
            Type type = Type.GetType("MyGame.MenuFunctions");
            functions[functionType.toInt()] = type.GetMethod(functionName);
        }

        public bool updateOptions()
        {
            ControlPad cp = GamerManager.getMainControls();
            if (cp.A_firstPressed() && (functions[tInputType.A.toInt()] != null))
            {
                functions[tInputType.A.toInt()].Invoke(null, null);
                return true;
            }
            if (cp.Left_firstPressed() && (functions[tInputType.Left.toInt()] != null))
            {
                functions[tInputType.Left.toInt()].Invoke(null, null);
                return true;
            }
            if (cp.Right_firstPressed() && (functions[tInputType.Right.toInt()] != null))
            {
                functions[tInputType.Right.toInt()].Invoke(null, null);
                return true;
            }
            if (cp.Up_firstPressed() && (functions[tInputType.Up.toInt()] != null))
            {
                functions[tInputType.Up.toInt()].Invoke(null, null);
                return true;
            }
            if (cp.Down_firstPressed() && (functions[tInputType.Down.toInt()] != null))
            {
                functions[tInputType.Down.toInt()].Invoke(null, null);
                return true;
            }

            return false;
        }

        public void render()
        {
            GraphicsManager.Instance.spriteBatch.Draw(texture, toDraw, Color.White);
        }
    }
}