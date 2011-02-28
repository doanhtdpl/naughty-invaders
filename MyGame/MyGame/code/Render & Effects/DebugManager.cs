using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    struct sDebugRectangle
    {
        public Vector2 initialPoint;
        public Vector2 endingPoint;
        public Color color;
    }

    class DebugManager
    {
        const int MAX_LINES = 500;
        const int MAX_RECTANGLES = 200;

        BasicEffect basicEffect;

        // debug lines
        VertexPositionColor[] lineList;
        short[] lineListIndices;
        int numberOfLines;
        // debug rectangles
        sDebugRectangle[] rectangles;
        VertexPositionColor[] rectanglePoints;
        short[] rectangleIndexes;
        int numberOfRectangles;


        static DebugManager instance = null;

        DebugManager()
        {
        }

        public static DebugManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DebugManager();
                }
                return instance;
            }
        }

        public void initialize()
        {
            basicEffect = new BasicEffect(SB.graphicsDevice);
            basicEffect.DiffuseColor = new Vector3(1.0f, 1.0f, 1.0f);
            basicEffect.VertexColorEnabled = true;

            lineList = new VertexPositionColor[MAX_LINES * 2];
            lineListIndices = new short[(MAX_LINES * 2)];
            numberOfLines = 0;
            short indexs = MAX_LINES * 2;
            for (short i = 0; i < indexs; i++)
            {
                lineListIndices[i] = i;
            }

            rectangles = new sDebugRectangle[MAX_RECTANGLES];
            rectangleIndexes = new short[4];
            numberOfRectangles = 0;
            for (short i = 0; i < 4; ++i)
            {
                rectangleIndexes[i] = i;
            }
            rectanglePoints = new VertexPositionColor[4];
        }

        public void addLine(Vector3 p1, Vector3 p2, Color color)
        {
            int index = numberOfLines * 2;

            if (index > lineList.Length) return;

            lineList[index].Position = p1;
            lineList[index].Color = color;
            lineList[index + 1].Position = p2;
            lineList[index + 1].Color = color;
            ++numberOfLines;
        }
        public void addLine(Vector2 p1, Vector2 p2, Color color)
        {
            addLine(new Vector3(p1.X, p1.Y, 0), new Vector3(p2.X, p2.Y, 0), color);
        }

        // draws colored borders of a rectangle taking as corners the two points. With alpha the rectangle can be empty or opaque
        public void addRectangle(Vector2 initialPoint, Vector2 endingPoint, Color color, float alpha = 0.3f)
        {
            Vector2 otherPoint1 = new Vector2(initialPoint.X, endingPoint.Y);
            Vector2 otherPoint2 = new Vector2(endingPoint.X, initialPoint.Y);
            addLine(initialPoint, otherPoint1, color);
            addLine(otherPoint1, endingPoint, color);
            addLine(endingPoint, otherPoint2, color);
            addLine(otherPoint2, initialPoint, color);
            
            // Ra a Marcos: Esto por qué estaba?
            //rectangles[numberOfRectangles].initialPoint = initialPoint;
            //rectangles[numberOfRectangles].endingPoint = endingPoint;
            //rectangles[numberOfRectangles].color = new Color(color.R, color.G, color.B, alpha);
            //++numberOfRectangles;
        }

        void renderLines()
        {
            if (numberOfLines > 0)
            {
                SB.graphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(
                    PrimitiveType.LineList,
                    lineList,
                    0,  // vertex buffer offset to add to each element of the index buffer
                    numberOfLines * 2,  // number of vertices in pointList
                    lineListIndices,  // the index buffer
                    0,  // first index element to read
                    numberOfLines   // number of primitives to draw
                );
            }
        }
        void renderRectangles()
        {
            for (int i = 0; i < numberOfRectangles; ++i)
            {
                Vector2 otherPoint1 = new Vector2(rectangles[i].initialPoint.X, rectangles[i].endingPoint.Y);
                Vector2 otherPoint2 = new Vector2(rectangles[i].endingPoint.X, rectangles[i].initialPoint.Y);

                rectanglePoints[0].Position = new Vector3(rectangles[i].initialPoint, 0);
                rectanglePoints[1].Position = new Vector3(otherPoint1, 0);
                rectanglePoints[2].Position = new Vector3(otherPoint2, 0);
                rectanglePoints[3].Position = new Vector3(rectangles[i].endingPoint, 0);
                for (int j = 0; j < rectanglePoints.Length; j++)
                {
                    rectanglePoints[j].Color = new Color(rectangles[i].color.R, rectangles[i].color.G, rectangles[i].color.B, rectangles[i].color.A);
                }

                SB.graphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(
                    PrimitiveType.TriangleStrip,
                    rectanglePoints,
                    0,  // vertex buffer offset to add to each element of the index buffer
                    4,  // number of vertices to draw
                    rectangleIndexes,
                    0,  // first index element to read
                    2   // number of primitives to draw
                );
            }
        }

        public void render()
        {
            basicEffect.View = Camera2D.view;
            basicEffect.Projection = Camera2D.projection;
            basicEffect.Techniques[0].Passes[0].Apply();
            renderRectangles();
            renderLines();
            endRender();
        }

        void endRender()
        {
            numberOfLines = 0;
            numberOfRectangles = 0;
        }
    }
}