using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class Screen
    {
        public static int screenCenterX;
        public static int screenCenterY;
        public static float aspect;
        // funciones para obtener coordenadas respecto al centro (independientemente de la resolución)
        public static int getXfromCenter(float x)
        {
            return (int)(screenCenterX + x);
        }
        public static int getYfromCenter(float y)
        {
            return (int)(screenCenterY - y);
        }
        public static Vector2 getXYfromCenter(float x, float y)
        {
            return new Vector2((screenCenterX + x), (screenCenterY - y));
        }
        public static Vector2 getXYfromCenter(Vector2 v)
        {
            return getXYfromCenter(v.X, v.Y);
        }

        public static void drawSafeArea()
        {
            Rectangle safeArea = SB.graphics.GraphicsDevice.Viewport.TitleSafeArea;
            // TASK
        }
    }
}
