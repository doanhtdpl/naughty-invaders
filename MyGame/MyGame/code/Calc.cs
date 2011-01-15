#region Using Statements
using System;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
#endregion

namespace MyGame
{
    class Calc
    {
        #region PI constants
        // PI & co
        public const float PI = 3.14159265f;
        public const float TwoPi = 6.28318531f;
        public const float TwoPiOver3 = 2.09439510f;
        public const float ThreePiOver2 = 4.71238898f;
        public const float PiOver2 = 1.57079633f;
        public const float PiOver4 = 0.785398163f;
        public const float PiOver8 = 0.392699082f;
        // more PI variables that have a little margin added to prevent little errors when compared
        public const float PiOver2Plus = PiOver2 + 0.05f;
        public const float PiOver4Plus = PiOver4 + 0.05f;
        public const float PiOver2Minus = PiOver2 - 0.05f;
        public const float PiOver4Minus = PiOver4 - 0.05f;
        #endregion

        const float ZERO_TOLERANCE = 0.001f;

        public static float Vector2Cross(Vector2 a, Vector2 b)
        {
            return Vector3.Cross(new Vector3(a, 0), new Vector3(b, 0)).Z;
        }

        #region Random
        private static Random random = new Random();
        public static int randomNatural(int min, int max)
        {
            return random.Next(min, max);
        }
        public static float randomScalar()
        {
            return (float)random.NextDouble();
        }
        public static float randomScalar(float min, float max)
        {
            return min + ((float)random.NextDouble() * (max - min));
        }
        #endregion
        #region Angles
        public static float radiansToDegrees(float radians)
        {
            return (radians * 360) / Calc.TwoPi;
        }
        public static float degreesToRadians(int degrees)
        {
            return ((float)degrees * Calc.TwoPi) / 360;
        }
        public static float degreesToRadians(float degrees)
        {
            return (degrees * Calc.TwoPi) / 360;
        }

        public static float randomAngle()
        {
            return randomScalar(0, Calc.TwoPi);
        }
        public static float randomAngle(float alpha, float beta)
        {
            return randomScalar(alpha, beta);
        }
        public static Vector2 randomDirection()
        {
            float angle = randomScalar(0, Calc.TwoPi);
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }
        public static Vector2 randomDirection(float alpha, float beta)
        {
            float angle = randomScalar(alpha, beta);
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }
        public static float directionToAngle(Vector2 direction)
        {
            return (float)Math.Atan2(direction.Y, direction.X);
        }
        public static Vector2 angleToDirection(float angle)
        {
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }
        public static float getAngleOfDirections(Vector2 from, Vector2 target)
        {
            return setAngleBetweenPiAndMinusPi(getDeltaOfAngles(directionToAngle(from), directionToAngle(target)));
        }
        public static float getAngleOfDirectionsXY(Vector3 from, Vector3 target)
        {
            return setAngleBetweenPiAndMinusPi(getDeltaOfAngles(directionToAngle(new Vector2(from.X, from.Y)), directionToAngle(new Vector2(target.X, target.Y))));
        }
        public static float getDeltaOfAngles(float a, float b)
        {
            a = setAngleBetweenPiAndMinusPi(a);
            b = setAngleBetweenPiAndMinusPi(b);
            float delta = b - a;
            if (delta > PI)			// 179-(-175) = 354º = -6 (354-360)
                delta -= TwoPi;
            else if (delta < -PI)		// -179º-(175) = -354º = 6(-354º+360)
                delta += TwoPi;		// delta ES NEGATIVO

            return delta;
        }
        public static float setAngleBetweenPiAndMinusPi(float angle)
        {
            while (angle > PI)
            {
                angle -= TwoPi;
            }
            while (angle < -PI)
            {
                angle += TwoPi;
            }
            return angle;
        }
        public static float distanceBetweenAngles(float alpha, float beta, float value, bool right)
        {
            float dif = getDeltaOfAngles(alpha, beta);
            if (right)
            {
                return -dif * value;
            }
            else
            {
                return dif * value;
            }
        }
        public static float interpolateAngles(float alpha, float beta, float value, bool right)
        {
            float dif = Math.Abs(getDeltaOfAngles(alpha, beta));

            if (right)
            {
                dif = alpha - dif * value;
            }
            else
            {
                dif = alpha + dif * value;
            }
            if (dif < 0)
                dif += Calc.TwoPi;
            if (dif > Calc.TwoPi)
                dif -= Calc.TwoPi;
            return dif;
        }
        #endregion
    }
}