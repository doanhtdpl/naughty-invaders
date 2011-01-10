#region Using Statements
using System;
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
    public class CoolizionLine
    {
        public short type;
        public bool opaque;
        public Vector2 p1;
        public Vector2 p2;

        public Vector2 rhoTheta; // Length and angle of the line
        public float rho { get { return rhoTheta.X; } }
        public float theta { get { return rhoTheta.Y; } }
        public bool isPlain = false;
        public float length;

        public const int NO_TYPE = 0;

        public CoolizionLine(Vector2 pp1, Vector2 pp2, short pType, bool pOpaque)
        {
            p1 = pp1;
            p2 = pp2;
            type = pType;
            opaque = pOpaque;
            recalc();
        }
        public CoolizionLine(Vector2 pp1, Vector2 pp2, short pType):this(pp1, pp2, pType, false){}
        public CoolizionLine(Vector2 pp1, Vector2 pp2) : this(pp1, pp2, NO_TYPE, false) { }

        public void inverseLine()
        {
            Vector2 aux;
            aux = p1;
            p1 = p2;
            p2 = aux;
            recalc();
        }

        public void recalc()
        {
            Vector2 delta = p2 - p1;
            length = delta.Length();
            float theta = (float)Math.Atan2(delta.Y, delta.X);
            if (theta < 0)
                theta += Microsoft.Xna.Framework.MathHelper.TwoPi;
            rhoTheta = new Vector2(length, theta);
            if (theta == Math.PI || theta == 0)
                isPlain = true;
        }

        public Matrix WorldMatrix()
        {
            Matrix rotate = Matrix.CreateRotationZ(theta);
            Matrix translate = Matrix.CreateTranslation(p1.X, p1.Y, 0);
            return rotate * translate;
        }
        public Matrix WorldMatrixTall()
        {
            Matrix rotate = Matrix.CreateRotationZ(theta);
            Matrix translate = Matrix.CreateTranslation(p1.X, p1.Y, 25);
            return rotate * translate;
        }
        public float distanceSquaredToPoint(Vector2 p)
        {
            Vector2 v = p2 - p1; // Vector from line's p1 to p2
            Vector2 w = p - p1; // Vector from line's p1 to p

            // See if p is closer to p1 than to the segment
            float c1 = Vector2.Dot(w, v);
            if (c1 <= 0)
                return Vector2.Distance(p, p1);

            // See if p is closer to p2 than to the segment
            float c2 = Vector2.Dot(v, v);
            if (c2 <= c1)
                return Vector2.Distance(p, p2);

            // p is closest to point pB, between p1 and p2
            float b = c1 / c2;
            Vector2 pB = p1 + b * v;
            return Vector2.Distance(p, pB);
        }
        public Vector2 vectorToPoint(Vector2 p)
        {
            Vector2 v = p2 - p1; // Vector from line's p1 to p2
            Vector2 w = p - p1; // Vector from line's p1 to p

            // See if p is closer to p1 than to the segment
            float c1 = Vector2.Dot(w, v);
            if (c1 <= 0)
                return p1;

            // See if p is closer to p2 than to the segment
            float c2 = Vector2.Dot(v, v);
            if (c2 <= c1)
                return p2;

            // p is closest to point pB, between p1 and p2
            float b = c1 / c2;
            Vector2 pB = p1 + b * v;
            return pB;
        }

        // result es al principio el punto del cuerpo que queremos saber si choca contra la línea, pero es
        // donde guardamos al fin el punto de la línea donde colisionará.
        public bool getCollisionPoint(Vector2 origin, Vector2 ray, ref Vector2 result)
        {
            float uaN = (ray.X - origin.X) * (p1.Y - origin.Y) - (ray.Y - origin.Y) * (p1.X - origin.X);
            float ubN = (p2.X-p1.X)*(p1.Y-origin.Y)-(p2.Y-p1.Y)*(p1.X-origin.X);
            float uD = (ray.Y - origin.Y) * (p2.X - p1.X) - (ray.X - origin.X) * (p2.Y - p1.Y);
            float uA = uaN / uD;
            float uB = ubN/uD;
            result.X = p1.X + uA * (p2.X - p1.X);
            result.Y = p1.Y + uA * (p2.Y - p1.Y);
            
            return (uA > 0 && uA < 1 && uB > 0 && uB < 1);
        }
        public Vector2 getNormal()
        {
            Vector3 vLine = new Vector3(p2.X-p1.X,p2.Y-p1.Y,0);
            Vector3 v = Vector3.Cross(vLine, -Vector3.UnitZ);
            return new Vector2(v.X, v.Y);
        }
        public bool isInSegment(Vector2 point)
        {
            Vector2 centerLine = (p1 + p2) / 2;
            float distanceSegment = Math.Abs(Vector2.Distance(point, centerLine));
            return (rho / 2) > distanceSegment;
        }
        public bool isWalkable()
        {
            if ((theta >= 0 && theta < Microsoft.Xna.Framework.MathHelper.PiOver4) ||
                (theta > Microsoft.Xna.Framework.MathHelper.PiOver4 * 3 && theta < Microsoft.Xna.Framework.MathHelper.PiOver4 * 5) ||
                (theta > Microsoft.Xna.Framework.MathHelper.PiOver4 * 7 && theta < Microsoft.Xna.Framework.MathHelper.TwoPi))
                return true; 
            else
                return false;
        }
        public bool getFallingAcceleration()
        {
            return (Math.Abs(Math.Cos(theta)) > Math.Cos(45));
        }
    }
}