using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Globalization;

namespace MyGame
{
    // this class provides lot of format converters between different types using extension methods
    // all of them use invariant culture to avoid culture problems
    // XML format is our own XML format, so is choosen according to our needs
    public static class FormatHelper
    {
        public static float toFloat(this string value)
        {
            return float.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
        }
        public static int toInt(this string value)
        {
            return int.Parse(value, CultureInfo.InvariantCulture.NumberFormat);
        }
        public static bool toBool(this string value)
        {
            return bool.Parse(value);
        }
        public static string toString(this float value)
        {
            return value.ToString(CultureInfo.InvariantCulture.NumberFormat);
        }
        public static byte toByte(this float value)
        {
            return (byte)(value * 255);
        }
        public static string toXML(this Vector2 v)
        {
            return v.X.toString() + " " + v.Y.toString();
        }
        public static Vector2 toVector2(this string str)
        {
            string[] values = str.Split(' ');
            return new Vector2(values[0].toFloat(), values[1].toFloat());
        }
        public static string toXML(this Vector3 v)
        {
            return v.X.toString() + " " + v.Y.toString() + " " + v.Z.toString();
        }
        public static Vector2 toVector2(this Vector3 v)
        {
            return new Vector2(v.X, v.Y);
        }
        public static Vector3 toVector3(this string str)
        {
            string[] values = str.Split(' ');
            return new Vector3(values[0].toFloat(), values[1].toFloat(), values[2].toFloat());
        }
        public static Vector3 toVector3(this Vector2 v)
        {
            return new Vector3(v.X, v.Y, 0.0f);
        }
        public static int toInt(this MyGame.MenuElement.tInputType type)
        {
            return (int)type;
        }
        // transforms an integer (number of seconds) to the format "1:44"
        public static string toTimeString(this int timeInSeconds)
        {
            // si es menos de un segundo devolvemos 0
            if (timeInSeconds < 1000)
                return "0";
            // si es menos que 10 segundos, devolvemos el primer número, que representa los segundos
            if (timeInSeconds < 10000)
                return timeInSeconds.ToString()[0].ToString();

            string t = timeInSeconds.ToString();
            // pasamos de milisegundos a segundos
            t = t.Remove(t.Length - 3, 3);
            timeInSeconds = int.Parse(t);
            if (timeInSeconds > 59)
            {
                int aux = timeInSeconds / 60;
                int aux2 = timeInSeconds % 60;
                // si quedan menos de 10 segundos para acabar el minuto actual, tenemos que pintar un cero para no confundir
                if (aux2 < 10)
                    return aux.ToString() + ":0" + aux2.ToString();
                else
                    return aux.ToString() + ":" + aux2.ToString();
            }
            else
            {
                return timeInSeconds.ToString();
            }
        }
        public static string toXML(this Color c)
        {
            return c.R.ToString() + " " + c.G.ToString() + " " + c.B.ToString() + " " + c.A.ToString();
        }
        public static Color toColor(this string str)
        {
            string[] values = str.Split(' ');
            return new Color(values[0].toInt(), values[1].toInt(), values[2].toInt(), values[3].toInt());
        }

        public static string toXML(this Rectangle r)
        {
            return r.X.ToString() + " " + r.Y.ToString() + " " + r.Width.ToString() + " " + r.Height.ToString();
        }
        public static Rectangle toRectangle(this string str)
        {
            string[] values = str.Split(' ');
            return new Rectangle(values[0].toInt(), values[1].toInt(), values[2].toInt(), values[3].toInt());
        }
        
        public static string toXML(this Matrix m)
        {
            string str =
                  m.M11.toString() + " " + m.M12.toString() + " " + m.M13.toString() + " " + m.M14.toString() + " "
                + m.M21.toString() + " " + m.M22.toString() + " " + m.M23.toString() + " " + m.M24.toString() + " "
                + m.M31.toString() + " " + m.M32.toString() + " " + m.M33.toString() + " " + m.M34.toString() + " "
                + m.M41.toString() + " " + m.M42.toString() + " " + m.M43.toString() + " " + m.M44.toString();
            return str;
        }
        public static Matrix toMatrix(this string str)
        {
            string[] values = str.Split(' ');
            Matrix m = Matrix.Identity;
            m.M11 = values[0].toFloat();  m.M12 = values[1].toFloat();  m.M13 = values[2].toFloat();  m.M14 = values[3].toFloat();
            m.M21 = values[4].toFloat();  m.M22 = values[5].toFloat();  m.M23 = values[6].toFloat();  m.M24 = values[7].toFloat();
            m.M31 = values[8].toFloat();  m.M32 = values[9].toFloat();  m.M33 = values[10].toFloat(); m.M34 = values[11].toFloat();
            m.M41 = values[12].toFloat(); m.M42 = values[13].toFloat(); m.M43 = values[14].toFloat(); m.M44 = values[15].toFloat();
            return m;
        }
    }
}
