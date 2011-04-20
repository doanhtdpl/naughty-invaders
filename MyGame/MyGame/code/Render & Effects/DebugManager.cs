using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    public struct sDebugText
    {
        public Vector2 position;
        public string text;

        public sDebugText(Vector2 position, string text)
        {
            this.position = position;
            this.text = text;
        }
    }

    class DebugManager
    {
        const int MAX_LINES = 4000;
        const int MAX_TEXTS = 50;

        BasicEffect basicEffect;

        // debug lines
        VertexPositionColor[] lineList;
        short[] lineListIndices;
        int numberOfLines;

        // debug texts
        sDebugText[] texts;
        int numberOfTexts;

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
            basicEffect = new BasicEffect(GraphicsManager.Instance.graphicsDevice);
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

            texts = new sDebugText[MAX_TEXTS];
        }

        public void addCircle(Vector2 position, float radius, int segments, Color color)
        {
            float angleStep = Calc.TwoPi / (float)segments;
            float currentAngle = 0.0f;
            Vector2 p1, p2;
            p2 = new Vector2(position.X + radius, position.Y);
            for (int i = 0; i < segments; ++i)
            {
                currentAngle += angleStep;
                p1 = p2;
                p2 = new Vector2((float)(position.X + Math.Cos(currentAngle) * radius),
                                 (float)(position.Y + Math.Sin(currentAngle) * radius));
                addLine(p1, p2, color);
            }
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
        }
        public void addRectangle(Vector3 initialPoint, Vector3 endingPoint, Color color, float alpha = 0.3f)
        {
            Vector3 otherPoint1 = new Vector3(initialPoint.X, endingPoint.Y, initialPoint.Z);
            Vector3 otherPoint2 = new Vector3(endingPoint.X, initialPoint.Y, initialPoint.Z);
            addLine(initialPoint, otherPoint1, color);
            addLine(otherPoint1, endingPoint, color);
            addLine(endingPoint, otherPoint2, color);
            addLine(otherPoint2, initialPoint, color);
        }
        public void addRectangle(Rectangle rectangle, Color color, float alpha = 0.3f)
        {
            Vector2 initialPoint = new Vector2(rectangle.Left, rectangle.Top);
            Vector2 endingPoint = new Vector2(rectangle.Right, rectangle.Bottom);
            addRectangle(initialPoint, endingPoint, color, alpha);
        }

        void renderLines()
        {
            if (numberOfLines > 0)
            {
                GraphicsManager.Instance.graphicsDevice.DrawUserIndexedPrimitives<VertexPositionColor>(
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

        public void addText(Vector2 position, string text)
        {
            if (numberOfTexts > texts.Length) return;

            texts[numberOfTexts].position = position;
            texts[numberOfTexts].text = text;
            ++numberOfTexts;
        }

        void renderTexts()
        {
            GraphicsManager.Instance.spriteBatch.Begin();
            for (int i = 0; i < numberOfTexts; ++i)
            {
                texts[i].text.render(texts[i].position, 0.4f);
            }
            GraphicsManager.Instance.spriteBatch.End();
        }

        public void render()
        {
            basicEffect.View = Camera2D.view;
            basicEffect.Projection = Camera2D.projection;
            basicEffect.Techniques[0].Passes[0].Apply();
            renderLines();
            renderTexts();
            endRender();
        }

        void endRender()
        {
            numberOfLines = 0;
            numberOfTexts = 0;
        }
    }
}