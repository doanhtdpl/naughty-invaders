#region Using Statements
using System;
using System.Collections.Generic;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
#endregion

namespace MyGame
{
    class CoolizionManager
    {
        // CONSTANTES
        public const float GRAVITY = 0.15f;
        // tipos de líneas de colisión

        const short maxLineCol = 2;

        public static List<CoolizionLine> getCoolizionRectangle(Rectangle A)
        {
            List<CoolizionLine> coolizionRectangle = new List<CoolizionLine>();
            coolizionRectangle.Add(new CoolizionLine(new Vector2(A.X, A.Y), new Vector2(A.X + A.Width, A.Y), CoolizionLine.NO_TYPE));
            coolizionRectangle.Add(new CoolizionLine(new Vector2(A.X, A.Y), new Vector2(A.X, A.Y + A.Height), CoolizionLine.NO_TYPE));
            coolizionRectangle.Add(new CoolizionLine(new Vector2(A.X + A.Width, A.Y + A.Height), new Vector2(A.X + A.Width, A.Y), CoolizionLine.NO_TYPE));
            coolizionRectangle.Add(new CoolizionLine(new Vector2(A.X + A.Width, A.Y + A.Height), new Vector2(A.X, A.Y + A.Height), CoolizionLine.NO_TYPE));
            return coolizionRectangle;
        }
        public static bool lineVSlines(CoolizionLine A, List<CoolizionLine> lines, ref Vector2 result)
        {
            for (short i = 0; i < lines.Count; i++)
            {
                if (lineVSline(lines[i], A, ref result))
                    return true;
            }
            return false;
        }
        public static bool lineVSlinesUpper(CoolizionLine A, List<CoolizionLine> lines, ref Vector2 result)
        {
            float Ymin = float.NegativeInfinity;
            Vector2 aux = Vector2.Zero;
            bool gotResult = false;
            for (short i = 0; i < lines.Count; i++)
            {
                if (lineVSline(lines[i], A, ref result))
                    if (result.Y > Ymin)
                    {
                        Ymin = result.Y;
                        aux = result;
                        gotResult = true;
                    }
            }
            result = aux;
            return gotResult;
        }
        public static bool lineVSline(CoolizionLine A, CoolizionLine B, ref Vector2 result)
        {
            float uaN, ubN, uD, uA, uB;
            Vector2 A1 = A.p1;
            Vector2 A2 = A.p2;
            Vector2 B1 = B.p1;
            Vector2 B2 = B.p2;

            A1 += Vector2.Normalize(A1 - A2);
            A2 -= Vector2.Normalize(A1 - A2);
            B1 += Vector2.Normalize(B1 - B2);
            B2 -= Vector2.Normalize(B1 - B2);

            uaN = (A2.X - A1.X) * (B1.Y - A1.Y) - (A2.Y - A1.Y) * (B1.X - A1.X);
            ubN = (B2.X - B1.X) * (B1.Y - A1.Y) - (B2.Y - B1.Y) * (B1.X - A1.X);
            uD = (A2.Y - A1.Y) * (B2.X - B1.X) - (A2.X - A1.X) * (B2.Y - B1.Y);
            uA = uaN / uD;
            uB = ubN / uD;

            result.X = B1.X + uA * (B2.X - B1.X);
            result.Y = B1.Y + uA * (B2.Y - B1.Y);
            return (uA > 0 && uA < 1 && uB > 0 && uB < 1);
        }
        public static bool lineVSline(Vector2 A1, Vector2 A2, Vector2 B1, Vector2 B2, ref Vector2 result)
        {
            double uaN, ubN, uD, uA, uB;

            A1 += Vector2.Normalize(A1 - A2);
            A2 -= Vector2.Normalize(A1 - A2);
            B1 += Vector2.Normalize(B1 - B2);
            B2 -= Vector2.Normalize(B1 - B2);

            uaN = (A2.X - A1.X) * (B1.Y - A1.Y) - (A2.Y - A1.Y) * (B1.X - A1.X);
            ubN = (B2.X - B1.X) * (B1.Y - A1.Y) - (B2.Y - B1.Y) * (B1.X - A1.X);
            uD = (A2.Y - A1.Y) * (B2.X - B1.X) - (A2.X - A1.X) * (B2.Y - B1.Y);
            uA = uaN / uD;
            uB = ubN / uD;

            result.X = (float)(B1.X + uA * (B2.X - B1.X));
            result.Y = (float)(B1.Y + uA * (B2.Y - B1.Y));
            return (uA > 0 && uA < 1 && uB > 0 && uB < 1);
        }
        public static bool linesVSlines(List<CoolizionLine> listA, List<CoolizionLine> listB, ref List<CollisionData> CD)
        {
            Vector2 v = Vector2.Zero;
            bool collide = false;
            for (short i = 0; i < listA.Count; i++)
            {
                for(short j=0; j<listB.Count;j++)
                {
                    if (lineVSline(listA[i], listB[j], ref v))
                    {
                        CD.Add(new CollisionData(0, 0, i, j, v));
                        collide = true;
                    }
                }
            }
            return collide;
        }
        public static bool linesVSlines(List<CoolizionLine> listA, List<CoolizionLine> listB)
        {
            Vector2 v = Vector2.Zero;
            for (short i = 0; i < listA.Count; i++)
            {
                for (short j = 0; j < listB.Count; j++)
                {
                    if (lineVSline(listA[i], listB[j], ref v))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool pointVSrectangle(Vector2 p, Rectangle rectangle, int radius)
        {
            return ( p.X+radius>rectangle.X && p.X-radius < rectangle.X+rectangle.Width
                 && p.Y+radius > rectangle.Y && p.Y-radius < rectangle.Y+rectangle.Height );
        }
        public static bool pointVSquad(Vector2 v, Vector2 v1, Vector2 v2)
        {
            float r1 = Vector2.Dot(v,v1);
            float r2 = Vector2.Dot(v,v2);
            return (0 <= r1 && r1 <= Vector2.Dot(v1,v1) && 0<= r2 && r2 <= Vector2.Dot(v2, v2));
        }
        public static bool pointVSpolygon(Vector2 p, Vector2[] polygonVertex)
        {
            int intersections = 0;
            Vector2 pLeft = new Vector2(p.X - 100000, p.Y);
            Vector2 res = Vector2.Zero;
            for (int i = 0; i < polygonVertex.Length; i++)
            {
                if (lineVSline(pLeft, p, polygonVertex[i], polygonVertex[i + 1], ref res))
                    intersections++;
            }
            if (intersections % 2 == 0) return false;
            else return true;
        }
        public static bool pointVSpolygon(Vector2 p, VertexPositionTexture[] polygonVertex)
        {
            int intersections = 0;
            Vector2 pLeft = new Vector2(p.X - 100000, p.Y);
            Vector2 res = Vector2.Zero;
            for (int i = 0; i < polygonVertex.Length-1; i++)
            {
                if (lineVSline(
                    pLeft, 
                    p,
                    new Vector2(polygonVertex[i].Position.X, polygonVertex[i].Position.Y),
                    new Vector2(polygonVertex[i + 1].Position.X, polygonVertex[i + 1].Position.Y),
                    ref res))
                    intersections++;
            }
            // calculamos la última intersección
            if (lineVSline(
                pLeft,
                p,
                new Vector2(polygonVertex[polygonVertex.Length - 1].Position.X, polygonVertex[polygonVertex.Length - 1].Position.Y),
                new Vector2(polygonVertex[0].Position.X, polygonVertex[0].Position.Y),
                ref res))
                intersections++;

            if (intersections % 2 == 0) return false;
            else return true;
        }
        public static bool pointVScircle(Vector2 p, Vector2 circleOrigin, float radius)
        {
            return Vector2.Distance(p, circleOrigin) < radius;
        }
        public static bool pointVScone(Vector2 p, Vector2 origin, float orientation, float angle, float distance)
        {
            if (Vector2.Distance(p, origin) < distance)
            {
                Vector2 direction = p - origin;
                float a = (float)Math.Atan2(direction.Y, direction.X);
                return (Math.Abs(Calc.getDeltaOfAngles(a, orientation)) < angle / 2);
            }
            return false;
        }
        public static List<CoolizionLine> filterLines(List<CoolizionLine> lines, int coltype)
        {
            List<CoolizionLine> res = new List<CoolizionLine>();
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].type == coltype)
                {
                    res.Add(lines[i]);
                }
            }
            return res;
        }

        public static List<CoolizionLine> getNearestLines(List<CoolizionLine> lines, Rectangle rectangle)
        {
            List<CoolizionLine> nearestLines = new List<CoolizionLine>();
            foreach (CoolizionLine line in lines)
            {
                Rectangle lineRectangle =
                    new Rectangle((int)Math.Min(line.p1.X, line.p2.X), (int)Math.Min(line.p1.Y, line.p2.Y),
                    (int)Math.Abs(line.p1.X - line.p2.X), (int)Math.Abs(line.p1.Y - line.p2.Y));

                if (lineRectangle.Intersects(rectangle))
                    nearestLines.Add(line);
            }

            return nearestLines;
        }
    }

    struct CollisionData
    {
        // A y B es para saber los objetos que chocan en la colisión
        // las lines especifican la línea exacta por la que choca cada objeto
        // point indica el punto de choque
        public short A;
        public short B;
        public short lineA;
        public short lineB;
        public Vector2 point;

        public CollisionData(short pA, short pB, short pLineA, short pLineB, Vector2 pPoint)
        {
            A = pA;
            B = pB;
            lineA = pLineA;
            lineB = pLineB;
            point = pPoint;
        }
        public void swapObjects()
        {
            short aux;
            aux = A;
            A = B;
            B = aux;
            aux = lineA;
            lineA = lineB;
            lineB = aux;
        }
    }
}
