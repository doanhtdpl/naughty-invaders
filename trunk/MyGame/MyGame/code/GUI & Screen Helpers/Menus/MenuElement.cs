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

    public class XPCounter : MenuElement
    {
        Player player;

        public XPCounter(Player player, string textureName, Vector2 position, Vector2 scale):base(textureName, position, scale, false)
        {
            this.player = player;
        }

        public override void render(bool selected, bool flip = false)
        {
            base.render(selected, flip);
            Vector2 numbersPosition = Screen.getXYfromCenter(position + new Vector2(50, 20));
            StringManager.render(player.data.XP.ToString(), numbersPosition, 1.0f, Color.Gold, StringManager.tTextAlignment.Left, SB.font, 1000, 1000, Color.Brown, 1.0f, new Vector2(1.5f, 1.5f), StringManager.tStyle.Shadowed);
            StringManager.render(player.data.totalXP.ToString() + " xp", numbersPosition + new Vector2(20, 35), 0.7f, Color.Gold, StringManager.tTextAlignment.Left, SB.font, 1000, 1000, Color.Brown, 1.0f, new Vector2(1.5f, 1.5f), StringManager.tStyle.Shadowed);
        }
    }

    public class MenuElement
    {
        Texture2D texture;
        public Vector2 position { get; set; }
        public Vector2 scale { get; set; }
        public Color color { get; set; }
        public string description { get; set; }
        Vector2 descriptionPosition;
        public Vector2 DescriptionPosition
        {
            get
            {
                return descriptionPosition;
            }
            set
            {
                descriptionPosition = Screen.getXYfromCenter(value);
            }
        }

        public bool drawLinkedElement { get; set; }
        public MenuElement linkedElement { get; set; }

        public enum tInputType { A = 0, X, Y, B, Up, Down, Right, Left }
        const int INPUTS = 8;
        MethodInfo[] functions;
        object[][] functionParameters;

        public MenuElement upNode { set; get; }
        public MenuElement downNode { set; get; }
            
        public MenuElement(string textureName, Vector2 position, Vector2 scale, bool drawLinkedElement = false)
        {
            this.description = null;
            this.color = Color.White;
            this.texture = TextureManager.Instance.getTexture("GUI/menu", textureName);
            this.position = position;
            Vector2 finalScale = scale;
            finalScale.X *= this.texture.Width;
            finalScale.Y *= this.texture.Height;
            this.scale = finalScale;
            this.drawLinkedElement = drawLinkedElement;

            upNode = null;
            downNode = null;

            functions = new MethodInfo[INPUTS];
            for (int i = 0; i < INPUTS; ++i)
            {
                functions[i] = null;
            }

            functionParameters = new object[INPUTS][];
            for (int i = 0; i < INPUTS; ++i)
            {
                functionParameters[i] = null;
            }
        }

        public void setFunction(string functionName, tInputType functionType, object[] parameters = null)
        {
            Type type = Type.GetType("MyGame.MenuFunctions");
            functions[functionType.toInt()] = type.GetMethod(functionName);
            this.functionParameters[functionType.toInt()] = parameters;
        }

        public bool updateOptions()
        {
            ControlPad cp = GamerManager.getMainControls();
            if (cp.A_firstPressed() && (functions[tInputType.A.toInt()] != null))
            {
                functions[tInputType.A.toInt()].Invoke(null, functionParameters[tInputType.A.toInt()]);
                return true;
            }
            if (cp.A_firstPressed() && (functions[tInputType.X.toInt()] != null))
            {
                functions[tInputType.X.toInt()].Invoke(null, functionParameters[tInputType.X.toInt()]);
                return true;
            }
            if (cp.A_firstPressed() && (functions[tInputType.Y.toInt()] != null))
            {
                functions[tInputType.Y.toInt()].Invoke(null, functionParameters[tInputType.Y.toInt()]);
                return true;
            }
            if (cp.A_firstPressed() && (functions[tInputType.B.toInt()] != null))
            {
                functions[tInputType.B.toInt()].Invoke(null, functionParameters[tInputType.B.toInt()]);
                return true;
            }
            if (cp.Left_firstPressed() && (functions[tInputType.Left.toInt()] != null))
            {
                functions[tInputType.Left.toInt()].Invoke(null, functionParameters[tInputType.Left.toInt()]);
                return true;
            }
            if (cp.Right_firstPressed() && (functions[tInputType.Right.toInt()] != null))
            {
                functions[tInputType.Right.toInt()].Invoke(null, functionParameters[tInputType.Right.toInt()]);
                return true;
            }
            if (cp.Up_firstPressed() && (functions[tInputType.Up.toInt()] != null))
            {
                functions[tInputType.Up.toInt()].Invoke(null, functionParameters[tInputType.Up.toInt()]);
                return true;
            }
            if (cp.Down_firstPressed() && (functions[tInputType.Down.toInt()] != null))
            {
                functions[tInputType.Down.toInt()].Invoke(null, functionParameters[tInputType.Down.toInt()]);
                return true;
            }
            return false;
        }

        public virtual void render(bool selected, bool flip = false)
        {
            if (flip)
            {
                texture.render2D(position, scale, Color.White, 0.0f, SpriteEffects.FlipHorizontally);
            }
            else
            {
                texture.render2D(position, scale, Color.White);
            }
            if (drawLinkedElement)
            {
                linkedElement.render(true);
            }

            if (selected)
            {
                if (description != null)
                {
                    description.renderNIDescription(descriptionPosition, 0.8f);
                }
            }
        }
    }
}