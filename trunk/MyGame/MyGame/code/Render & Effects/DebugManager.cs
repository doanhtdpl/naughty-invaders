using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class DebugManager
    {
        const int MAX_LINES = 500;

        BasicEffect basicEffect;

        VertexPositionColor[] lineList;
        short[] lineListIndices;
        int numberOfLines;


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
            lineList = new VertexPositionColor[MAX_LINES * 2];
            lineListIndices = new short[(MAX_LINES * 2)];          

            basicEffect = new BasicEffect(SB.graphicsDevice);
            basicEffect.DiffuseColor = new Vector3(1.0f, 1.0f, 1.0f);
            basicEffect.VertexColorEnabled = true;

            basicEffect.View = Camera2D.view;
            basicEffect.Projection = Camera2D.projection;

            short indexs = MAX_LINES * 2;
            for (short i = 0; i < indexs; i++)
            {
                lineListIndices[i] = i;
            }
        }

        public void addLine(Vector3 p1, Vector3 p2, Color color)
        {
            int index = numberOfLines * 2;
            lineList[index].Position = p1;
            lineList[index].Color = color;
            lineList[index + 1].Position = p2;
            lineList[index + 1].Color = color;
            ++numberOfLines;
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

        public void render()
        {
            basicEffect.Techniques[0].Passes[0].Apply();
            renderLines();
            endRender();
        }

        void endRender()
        {
            numberOfLines = 0;
        }
    }
}