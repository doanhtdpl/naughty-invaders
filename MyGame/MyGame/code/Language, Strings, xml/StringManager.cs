using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    // This class renders a text with multiple options. The text can have images, if those images have the reference on
    // the string text like that: "Some text /imageName/ more text."
    public static class StringManager
    {
        // texts with more than MAX_TEXT_ELEMENTS words and images won't be fully rendered
        const int MAX_TEXT_ELEMENTS = 500;
        const string imagesPath = "textures/GUI/textImages/";
        // TextElement is used to save at the parsing step the elements to render after
        public struct TextElement
        {
            string str;
            Texture2D texture;
            public Vector2 position;
            float scale;

            public void initialize(string s, Vector2 pos)
            {
                str = s;
                texture = null;
                position = pos;
                scale = 1;
            }
            public void initialize(Texture2D tex, Vector2 pos, float sca)
            {
                str = null;
                texture = tex;
                position = pos;
                scale = sca;
            }

            public void render(Vector2 offset)
            {
                if (texture != null)
                {
                    GraphicsManager.Instance.spriteBatch.Draw(texture, position + offset, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                    //GraphicsManager.Instance.spriteBatch.Draw(texture, position + new Vector2(-17, -13), null, Color.White, 0, Vector2.Zero, scale * 2.5f, SpriteEffects.None, 0);
                }
                else
                {
                    switch (textStyle)
                    {
                        case tStyle.Normal:
                            GraphicsManager.Instance.spriteBatch.DrawString(textFont, str, position + offset, textColor * textAlpha, 0, Vector2.Zero, textScale, SpriteEffects.None, 0);
                            break;
                        case tStyle.Border:
                            drawSurroundedString(str, position + offset, textColor, textShadowColor, textScale, textAlpha);
                            break;
                        case tStyle.Shadowed:
                            drawShadowedString(str, position + offset, textColor, textShadowColor, textScale, textAlpha);
                            break;
                    }
                }
            }
            public void render()
            {
                render(Vector2.Zero);
            }
        }

        // enums to specify alignment and text style
        public enum tTextAlignment { NoAlignment, Left, Right, Centered };
        public enum tStyle { Normal, Shadowed, Border };

        // static array of TextElements yo reserve memory only once
        static TextElement[] textElements = new TextElement[MAX_TEXT_ELEMENTS];
        // variables initialized at the start of the whole render text
        static Vector2 textBorder;
        static SpriteFont textFont;
        static float textScale;
        static Color textColor;
        static Color textShadowColor;
        static float textAlpha;
        static tTextAlignment textAlignment;
        static tStyle textStyle;

        // the images able to be rendered are loaded in this dictionary, need to load the images at loadContent method
        static Dictionary<string, ScaledTexture2D> images = new Dictionary<string, ScaledTexture2D>();
        // load the images from another place and initialize the dictionary with them
        public static void setDictionary(Dictionary<string, ScaledTexture2D> newImages)
        {
            images = newImages;
        }
        // load manually the images
        public static void loadContent()
        {
            images.Add("A", new ScaledTexture2D(SB.content.Load<Texture2D>(imagesPath + "A"), 0.54f));
            images.Add("B", new ScaledTexture2D(SB.content.Load<Texture2D>(imagesPath + "B"), 0.54f));
            images.Add("X", new ScaledTexture2D(SB.content.Load<Texture2D>(imagesPath + "X"), 0.54f));
            images.Add("Y", new ScaledTexture2D(SB.content.Load<Texture2D>(imagesPath + "Y"), 0.54f));
            images.Add("DPAD", new ScaledTexture2D(SB.content.Load<Texture2D>(imagesPath + "DPAD"), 0.30f));
            images.Add("LS", new ScaledTexture2D(SB.content.Load<Texture2D>(imagesPath + "LS"), 0.37f));
            images.Add("RS", new ScaledTexture2D(SB.content.Load<Texture2D>(imagesPath + "RS"), 0.37f));
            images.Add("LB", new ScaledTexture2D(SB.content.Load<Texture2D>(imagesPath + "LB"), 0.45f));
            images.Add("RB", new ScaledTexture2D(SB.content.Load<Texture2D>(imagesPath + "RB"), 0.45f));
            images.Add("LT", new ScaledTexture2D(SB.content.Load<Texture2D>(imagesPath + "LT"), 0.35f));
            images.Add("RT", new ScaledTexture2D(SB.content.Load<Texture2D>(imagesPath + "RT"), 0.35f));
            images.Add("START", new ScaledTexture2D(SB.content.Load<Texture2D>(imagesPath + "START"), 0.65f));
            images.Add("BACK", new ScaledTexture2D(SB.content.Load<Texture2D>(imagesPath + "BACK"), 0.65f));
            images.Add("GUIDE", new ScaledTexture2D(SB.content.Load<Texture2D>(imagesPath + "GUIDE"), 0.42f));
        }

        public static void unloadContent()
        {
            images.Clear();
        }

        // render text functions with multiple headers
        // the text function renders the text with images represented by a string.
        // the string format is to have each word separated by space and each image like that ::ImageNameInDictionary
        // example: "Press ::START to begin."
        // the text can be rendered with a scale, space between lines and alignment
        // the alignment depends on the position parameter
        // the text can have color, shadow or bordered
        public static void render(this String str, Vector2 position, float scale)
        {
            render(str, position, scale, Color.White, tTextAlignment.Left, SB.font);
        }
        public static void render(this String str, Vector2 position, float scale, Color color)
        {
            render(str, position, scale, color, tTextAlignment.Left, SB.font);
        }
        public static void render(this String str, Vector2 position, float scale, Color color, tTextAlignment alignment)
        {
            render(str, position, scale, color, alignment, SB.font);
        }
        public static void render(this String str, Vector2 position, float scale, Color color, tTextAlignment alignment, SpriteFont font)
        {
            render(str, position, scale, color, alignment, SB.font, 550, 40 * scale, Color.Black, 1.0f, Vector2.Zero, tStyle.Normal);
        }
        public static void render(this String str, Vector2 position, float scale, Color color, tTextAlignment alignment, SpriteFont font, float maxWidth, float interLineY, Color shadowColor, float alpha, Vector2 border, tStyle style)
        {
            // initialize variables
            textFont = font;
            textColor = color;
            textScale = scale;
            textAlignment = alignment;
            textStyle = style;
            textShadowColor = shadowColor;
            textAlpha = alpha;
            textBorder = border;

            // to center the images in height according the text, get the aproximate height of the font
            float halfFontHeight = font.MeasureString("A").Y * scale * 0.5f;
            float spaceWidth = font.MeasureString(" ").X * scale;
            // current position that depends on how many characters and images have been rendered
            float sizeX = 0;
            // to backup the number of elements parsed
            int elementsParsed = 0;
            int firstElementInLine = 0;
            // separate the text and the images and start rendering
            string[] strToRender = str.Split(' ');
            // changeline used to tell if \n is parsed
            bool changeLine = false;
            bool nextChangeLine = false;
            for (int i = 0; i < strToRender.Length; i++)
            {
                // update de change line
                changeLine = nextChangeLine;
                nextChangeLine = false;

                // if the \n comes from a resource or from a code created string
                if (strToRender[i].EndsWith("\\n") || strToRender[i].EndsWith("\n"))
                {
                    nextChangeLine = true;
                    strToRender[i] = strToRender[i].TrimEnd('n');
                    strToRender[i] = strToRender[i].TrimEnd('\\');
                }
                if (strToRender[i].StartsWith("::")) // transform the format
                {
                    strToRender[i] = strToRender[i].TrimStart(':');
                }
                else // add a space at the end of each word
                {
                    strToRender[i] += " ";
                }
                // if the substring represents an image...
                if (images.ContainsKey(strToRender[i]))
                {
                    ScaledTexture2D st = images[strToRender[i]];
                    float sizeToAdd = st.getWidth() * scale;
                    if (sizeX + sizeToAdd > maxWidth || changeLine) // change line if it doesnt fit in current line
                    {
                        updateAlignment(alignment, (int)position.X, (int)sizeX, firstElementInLine, i - 1);
                        position.Y += interLineY;
                        sizeX = 0;
                        firstElementInLine = i;
                    }
                    //GraphicsManager.Instance.spriteBatch.Draw(st.texture, new Vector2(position.X + sizeX, position.Y - st.getHeight() * scale * 0.5f + halfFontHeight), null, Color.White, 0, Vector2.Zero, scale * st.scale, SpriteEffects.None, 0);
                    textElements[i].initialize(st.texture, new Vector2(position.X + sizeX, position.Y - st.getHeight() * scale * 0.5f + halfFontHeight), scale * st.scale);
                    sizeX += st.getWidth() * scale + spaceWidth;                    
                }
                else // ...if substring represents text
                {
                    float sizeToAdd = font.MeasureString(strToRender[i]).X * scale;
                    if (sizeX + sizeToAdd > maxWidth || changeLine) // change line if it doesnt fit in current line
                    {
                        updateAlignment(alignment, (int)position.X, (int)sizeX, firstElementInLine, i - 1);
                        position.Y += interLineY;
                        sizeX = 0;
                        firstElementInLine = i;
                    }      
                    //GraphicsManager.Instance.spriteBatch.DrawString(font, strToRender[i], new Vector2(position.X + sizeX, position.Y), color, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
                    textElements[i].initialize(strToRender[i], new Vector2(position.X + sizeX, position.Y));
                    sizeX += sizeToAdd;      
                }
                elementsParsed = i;
            }
            updateAlignment(alignment, (int)position.X, (int)sizeX, firstElementInLine, elementsParsed);

            // draw all the text elements
            for (int i = 0; i <= elementsParsed; i++)
            {
                textElements[i].render();
            }
        }
       
        public static void renderSC(this String str, Vector2 position, float scale, Color color, Color shadowColor, tTextAlignment alignment)
        {
            render(str, position, scale, color, alignment, SB.font, 1000, 30 * scale, shadowColor, 1.0f, new Vector2(1,1), tStyle.Border);
        }
        
        public static void renderNIDialog(this String str, Vector2 position, float scale, Color color)
        {
            render(str.ToUpper(), position, scale / 2.5f, color, tTextAlignment.Left, SB.font, 770, 60 * scale / 1.45f, Color.Black, 1.0f, new Vector2(1.5f, 1.5f), tStyle.Shadowed);
        }
        public static void renderNI(this String str, Vector2 position, float scale, tStyle style = tStyle.Normal)
        {
            render(str.ToUpper(), position, scale / 2.5f, Color.White, tTextAlignment.Centered, SB.font, 770, 60 * scale / 1.45f, Color.Black, 1.0f, new Vector2(1.5f, 1.5f), style);
        }
        public static void renderNI(this String str, Vector2 position, float scale, tStyle style, Color textColor, Color shadowColor)
        {
            render(str.ToUpper(), position, scale / 2.5f, textColor, tTextAlignment.Centered, SB.font, 770, 60 * scale / 1.45f, shadowColor, 1.0f, new Vector2(2.5f, 2.5f), style);
        }
        public static void renderNIDescription(this String str, Vector2 position, float scale)
        {
            render(str.ToUpper(), position, scale / 2.5f, Color.BlueViolet, tTextAlignment.Left, SB.font, 500, 60 * scale / 1.45f, Color.Black, 1.0f, new Vector2(1.5f, 1.5f), tStyle.Shadowed);
        }

        private static void updateAlignment(tTextAlignment alignment, int positionX, int sizeX, int first, int last)
        {
            int xOffset = 0;
            switch (alignment)
            {
                case tTextAlignment.NoAlignment:
                    return;
                case tTextAlignment.Centered:
                    xOffset -= sizeX / 2;
                    break;
                case tTextAlignment.Left:
                    break;
                case tTextAlignment.Right:
                    xOffset -= sizeX;
                    break;
            }

            for (int i = first; i <= last; i++)
            {
                textElements[i].position.X += xOffset;
            }
        }

        public static void drawSurroundedString(String text, Vector2 position, Color primal, Color shadow, float scale, float alpha)
        {
            GraphicsManager.Instance.spriteBatch.DrawString(SB.font, text, position - textBorder, shadow * alpha, 0, Vector2.Zero, scale, SpriteEffects.None, 1f);
            GraphicsManager.Instance.spriteBatch.DrawString(SB.font, text, position + textBorder, shadow * alpha, 0, Vector2.Zero, scale, SpriteEffects.None, 1f);
            GraphicsManager.Instance.spriteBatch.DrawString(SB.font, text, position + new Vector2(-textBorder.X, textBorder.Y), shadow * alpha, 0, Vector2.Zero, scale, SpriteEffects.None, 1f);
            GraphicsManager.Instance.spriteBatch.DrawString(SB.font, text, position + new Vector2(textBorder.X, -textBorder.Y), shadow * alpha, 0, Vector2.Zero, scale, SpriteEffects.None, 1f);
            GraphicsManager.Instance.spriteBatch.DrawString(SB.font, text, position, primal * alpha, 0, Vector2.Zero, scale, SpriteEffects.None, 1f);
        }
        public static void drawShadowedString(String text, Vector2 position, Color primal, Color shadow, float scale, float alpha)
        {
            GraphicsManager.Instance.spriteBatch.DrawString(SB.font, text, position + textBorder, shadow * alpha, 0, Vector2.Zero, scale, SpriteEffects.None, 1f);
            GraphicsManager.Instance.spriteBatch.DrawString(SB.font, text, position, primal * alpha, 0, Vector2.Zero, scale, SpriteEffects.None, 1f);
        }
    }

    public struct ScaledTexture2D
    {
        public Texture2D texture;
        public float scale;

        public ScaledTexture2D(Texture2D t, float s)
        {
            texture = t;
            scale = s;
        }

        public float getWidth()
        {
            return texture.Width * scale;
        }

        public float getHeight()
        {
            return texture.Height * scale;
        }
    };
}
