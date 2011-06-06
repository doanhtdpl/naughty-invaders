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
    public static class Calc
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
        public static int clamp(int value, int min, int max)
        {
            if (value < min)
            {
                return min;
            }
            else if (value > max)
            {
                return max;
            }
            return value;
        }

        #region Random
        private static Random random = new Random();
        public static int randomNatural(int min, int max)
        {
            return random.Next(min, max + 1);
        }
        public static float randomScalar()
        {
            return (float)random.NextDouble();
        }
        public static bool randomBool()
        {
            return random.NextDouble() < 0.5f;
        }
        public static float randomScalar(float min, float max)
        {
            return min + ((float)random.NextDouble() * (max - min));
        }
        public static Vector3 randomVector3(Vector3 varianceMin, Vector3 varianceMax)
        {
	        return new Vector3(
		        randomScalar(varianceMin.X, varianceMax.X),
                randomScalar(varianceMin.Y, varianceMax.Y),
                randomScalar(varianceMin.Z, varianceMax.Z));
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
            if (delta > PI)			// 179-(-175) = 354� = -6 (354-360)
                delta -= TwoPi;
            else if (delta < -PI)		// -179�-(175) = -354� = 6(-354�+360)
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
        public static float clampAngle(float value, float min, float max)
        {
            if (getDeltaOfAngles(value, min) > 0)
            {
                return min;
            }
            else if (getDeltaOfAngles(value, max) < 0)
            {
                return max;
            }
            return value;
        }
        public static Vector2 fromDirectionToDirectionAtSpeed(Vector2 from, Vector2 to, float speed)
        {
            return angleToDirection(fromAngleToAngleAtSpeed(directionToAngle(from), directionToAngle(to), speed));
        }
        public static float fromAngleToAngleAtSpeed(float from, float to, float speed)
        {
            float delta = getDeltaOfAngles(from, to);
            float increment = speed * SB.dt;
            if (delta < 0.0f)
            {
                if (-increment < delta)
                {
                    return to;
                }
                return from - increment;
            }
            else
            {
                if (increment > delta)
                {
                    return to;
                }
                return from + increment;
            }
        }
        #endregion
        #region Ray intersections
        public static bool intersectsTriangle(this Ray ray, Vector3 tri0, Vector3 tri1, Vector3 tri2)
        {
            float pickDistance, barycentricU, barycentricV;
            // Find vectors for two edges sharing vert0
            Vector3 edge1 = tri1 - tri0;
            Vector3 edge2 = tri2 - tri0;

            // Begin calculating determinant - also used to calculate barycentricU parameter
            Vector3 pvec = Vector3.Cross(ray.Direction, edge2);

            // If determinant is near zero, ray lies in plane of triangle
            float det = Vector3.Dot(edge1, pvec);
            if (det < 0.0001f)
                return false;

            // Calculate distance from vert0 to ray origin
            Vector3 tvec = ray.Position - tri0;

            // Calculate barycentricU parameter and test bounds
            barycentricU = Vector3.Dot(tvec, pvec);
            if (barycentricU < 0.0f || barycentricU > det)
                return false;

            // Prepare to test barycentricV parameter
            Vector3 qvec = Vector3.Cross(tvec, edge1);

            // Calculate barycentricV parameter and test bounds
            barycentricV = Vector3.Dot(ray.Direction, qvec);
            if (barycentricV < 0.0f || barycentricU + barycentricV > det)
                return false;

            // Calculate pickDistance, scale parameters, ray intersects triangle
            pickDistance = Vector3.Dot(edge2, qvec);
            float fInvDet = 1.0f / det;
            pickDistance *= fInvDet;
            barycentricU *= fInvDet;
            barycentricV *= fInvDet;

            return true;
        }
        #endregion
        #region Rectangles
        public static Rectangle getRectangle(Vector2 position, Vector2 size)
        {
            return new Rectangle((int)(position.X - size.X * 0.5), (int)(position.Y - size.Y * 0.5), (int)size.X, (int)size.Y);
        }
        #endregion
    }
}