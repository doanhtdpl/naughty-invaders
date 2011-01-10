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
    class GUI
    {
        public static Vector2 r = new Vector2(1, 1);

        public static void loadContent()
        {
        }

        public void update()
        {
        }

        public void render()
        {
            SB.spriteBatch.Begin();
            SB.spriteBatch.End();
        }

        #region Crono & time
        public void renderCrono(int time)
        {
            string strTime = getTimeString(time);
            float offset = SB.font.MeasureString(strTime).X / 2;
            string str = strTime.ToString() + " " + TextKey.Seconds.Translate();
            str.renderSC(Screen.getXYfromCenter(-offset, 0), 1.0f, Color.White, Color.Black, StringManager.tTextAlignment.Centered);
        }
        // Obtiene una string en el tipo de formato printable minutos y segundos "1:44"
        public static string getTimeString(int time)
        {
            // si es menos de un segundo devolvemos 0
            if (time < 1000)
                return "0";
            // si es menos que 10 segundos, devolvemos el primer número, que representa los segundos
            if (time < 10000)
                return time.ToString()[0].ToString();

            string t = time.ToString();
            // pasamos de milisegundos a segundos
            t = t.Remove(t.Length - 3, 3);
            time = int.Parse(t);
            if (time > 59)
            {
                int aux = time / 60;
                int aux2 = time % 60;
                // si quedan menos de 10 segundos para acabar el minuto actual, tenemos que pintar un cero para no confundir
                if (aux2 < 10)
                    return aux.ToString() + ":0" + aux2.ToString();
                else
                    return aux.ToString() + ":" + aux2.ToString();
            }
            else
            {
                return time.ToString();
            }
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