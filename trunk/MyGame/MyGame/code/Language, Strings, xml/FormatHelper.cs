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
        public static string toString(this float value)
        {
            return value.ToString(CultureInfo.InvariantCulture.NumberFormat);
        }
        public static Vector3 toVector3(this string str)
        {
            string[] values = str.Split(' ');
            return new Vector3(values[0].toFloat(), values[1].toFloat(), values[2].toFloat());
        }
        public static Color toColor(this string str)
        {
            string[] values = str.Split(' ');
            return new Color(values[0].toFloat(), values[1].toFloat(), values[2].toFloat(), values[3].toFloat());
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
            Matrix m;
            m.M11 = values[0].toFloat();  m.M12 = values[1].toFloat();  m.M13 = values[2].toFloat();  m.M14 = values[3].toFloat();
            m.M21 = values[4].toFloat();  m.M22 = values[5].toFloat();  m.M23 = values[6].toFloat();  m.M24 = values[7].toFloat();
            m.M31 = values[8].toFloat();  m.M32 = values[9].toFloat();  m.M33 = values[10].toFloat(); m.M34 = values[11].toFloat();
            m.M41 = values[12].toFloat(); m.M42 = values[13].toFloat(); m.M43 = values[14].toFloat(); m.M44 = values[15].toFloat();
            return m;
        }
    }
}
