using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace MyGame
{
    class GUIManager
    {
        static GUIManager instance = null;
        GUIManager()
        {
        }
        public static GUIManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GUIManager();
                }
                return instance;
            }
        }

        public Texture2D largeHeader, smallHeader, option1, option2, option3;
        public static Vector2 r = new Vector2(1, 1);

        public void loadContent()
        {
            largeHeader = TextureManager.Instance.getTexture("GUI/menu/largeHeader");
            smallHeader = TextureManager.Instance.getTexture("GUI/menu/smallHeader");
            option1 = TextureManager.Instance.getTexture("GUI/menu/option1");
            option2 = TextureManager.Instance.getTexture("GUI/menu/option2");
            option3 = TextureManager.Instance.getTexture("GUI/menu/option3");
        }

        public void update()
        {
        }

        public void render()
        {
            GraphicsManager.Instance.spriteBatch.Begin();
            GraphicsManager.Instance.spriteBatch.End();
        }

        #region Crono & time
        public void renderCrono(int time)
        {
            string strTime = time.toTimeString();
            float offset = SB.font.MeasureString(strTime).X / 2;
            string str = strTime.ToString() + " " + TextKey.Seconds.Translate();
            str.renderSC(Screen.getXYfromCenter(-offset, 0), 1.0f, Color.White, Color.Black, StringManager.tTextAlignment.Centered);
        }
        public static string drawTime(int time)
        {
            String t = time.ToString();
            switch (t.Length)
            {
                case 1:
                    return t;
                case 2:
                    return t;
                case 3:
                    return t;
                case 4:
                    return t[0].ToString() + "s " + t[1].ToString() + t[2].ToString();
                case 5:
                    return t[0].ToString() + t[1].ToString() + "s " + t[2] + t[3];
                case 6:
                    return t[0].ToString() + t[1].ToString() + t[2].ToString() + "s " + t[3].ToString() + t[4].ToString();
                default:
                    return t;
            }
        }
        #endregion    
    }
}