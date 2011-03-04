using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace MyGame
{
    class LineManager
    {
        public static BasicEffect lines_effect;
        // line effect
        private static Effect lineEffect;
        private static EffectParameter wvpMatrixParameter;
        private static EffectParameter timeParameter;
        private static EffectParameter lengthParameter;
        private static EffectParameter radiusParameter;
        private static EffectParameter lineColorParameter;
        private static VertexBuffer vb;
        private static IndexBuffer ib;
        private static VertexDeclaration vdecl;
        private static int numVertices;
        private static int numIndices;
        private static int numPrimitives;

        public static void loadContent()
        {
            // primitive lines
            lines_effect = new BasicEffect(SB.graphicsDevice);
            // line effect
            lineEffect = SB.content.Load<Effect>("effects/Line");
            wvpMatrixParameter = lineEffect.Parameters["worldViewProj"];
            timeParameter = lineEffect.Parameters["time"];
            lengthParameter = lineEffect.Parameters["length"];
            radiusParameter = lineEffect.Parameters["radius"];
            lineColorParameter = lineEffect.Parameters["lineColor"];
            createLineMesh();
        }

        public static void initRender()
        {
            lines_effect.View = Camera2D.view;
            lines_effect.Projection = Camera2D.projection;
        }

        public static void renderLines(VertexPositionTexture[] vertex, int primitiveCount, Color color)
        {
            int[] index = new int[vertex.Length * 2];
            for (int i = 0; i < vertex.Length; i++)
            {
                index[i * 2] = i;
                index[(i * 2) + 1] = i + 1;
            }

            lines_effect.DiffuseColor = color.ToVector3();
            lines_effect.World = Matrix.CreateTranslation(Vector3.Zero);
            foreach (EffectPass pass in lines_effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                SB.graphicsDevice.DrawUserIndexedPrimitives<VertexPositionTexture>(
                    PrimitiveType.LineList, vertex, 0, primitiveCount + 1, index, 0, primitiveCount);
            }
        }
        public static void renderNonContinuousLines(Vector3[] vertex, int primitiveCount, Color color)
        {
            //int[] index = new int[vertex.Length];
            //for (int i = 0; i < vertex.Length; i++)
            //{
            //    index[i] = i;
            //}

            //lines_effect.DiffuseColor = color.ToVector3();
            //lines_effect.World = Matrix.CreateTranslation(Vector3.Zero);

            //foreach (EffectPass pass in lines_effect.CurrentTechnique.Passes)
            //{
            //    pass.Apply();
            //    SB.graphics.GraphicsDevice.DrawUserIndexedPrimitives<Vector3>(PrimitiveType.LineList, vertex, 0, primitiveCount * 2, index, 0, primitiveCount);
            //}
        }

        public static void createLineMesh()
        {
            const int MAXRES = 15; // A higher MAXRES produces rounder endcaps at the cost of more vertices

            numVertices = 6 + (MAXRES + 2) + (MAXRES + 2);
            numPrimitives = 4 + MAXRES + MAXRES;
            numIndices = 3 * numPrimitives;
            short[] indices = new short[numIndices];

            VertexPositionNormalTexture[] tri = new VertexPositionNormalTexture[numVertices];

            // quad vertices
            int iVertexBase = 0;
            tri[0] = new VertexPositionNormalTexture(new Vector3(0.0f, -1.0f, 0), new Vector3(1, 0, 0), new Vector2(0, 0));
            tri[1] = new VertexPositionNormalTexture(new Vector3(0.0f, -1.0f, 0), new Vector3(1, 0, 0), new Vector2(0, 1));
            tri[2] = new VertexPositionNormalTexture(new Vector3(0.0f, 0.0f, 0), new Vector3(0, 0, 0), new Vector2(0, 1));
            tri[3] = new VertexPositionNormalTexture(new Vector3(0.0f, 0.0f, 0), new Vector3(0, 0, 0), new Vector2(0, 0));
            tri[4] = new VertexPositionNormalTexture(new Vector3(0.0f, 1.0f, 0), new Vector3(1, 0, 0), new Vector2(0, 1));
            tri[5] = new VertexPositionNormalTexture(new Vector3(0.0f, 1.0f, 0), new Vector3(1, 0, 0), new Vector2(0, 0));

            // quad indices
            int iIndexBase = 0;
            indices[0] = 0;
            indices[1] = 1;
            indices[2] = 2;
            indices[3] = 2;
            indices[4] = 3;
            indices[5] = 0;
            indices[6] = 2;
            indices[7] = 4;
            indices[8] = 3;
            indices[9] = 4;
            indices[10] = 5;
            indices[11] = 3;

            iVertexBase = 6;
            iIndexBase = 12;

            // left halfdisc vertices
            for (int i = 0; i < (MAXRES + 2); i++)
            {
                float x;
                float y;
                float theta;
                float distFromCenter;
                if (i == 0)
                {
                    x = 0;
                    y = 0;
                    theta = 0;
                    distFromCenter = 0;
                }
                else
                {
                    theta = (float)(i - 1) / (2 * MAXRES) * MathHelper.TwoPi + MathHelper.PiOver2;
                    x = (float)Math.Cos(theta);
                    y = (float)Math.Sin(theta);
                    distFromCenter = 1;
                }
                tri[iVertexBase + i] = new VertexPositionNormalTexture(new Vector3(x, y, 0), new Vector3(distFromCenter, 0, 0), new Vector2(1, 0));
            }

            // left halfdisc indices
            int iIndex = 0;
            for (int iPrim = 0; iPrim < MAXRES; iPrim++)
            {
                indices[iIndexBase + iIndex++] = (short)(iVertexBase + 0);
                indices[iIndexBase + iIndex++] = (short)(iVertexBase + iPrim + 1);
                indices[iIndexBase + iIndex++] = (short)(iVertexBase + iPrim + 2);
            }

            iVertexBase += (MAXRES + 2);
            iIndexBase += MAXRES * 3;

            // right halfdisc vertices
            for (int i = 0; i < (MAXRES + 2); i++)
            {
                float x;
                float y;
                float theta;
                float distFromCenter;
                if (i == 0)
                {
                    x = 0.0f;
                    y = 0;
                    theta = 0;
                    distFromCenter = 0;
                }
                else
                {
                    theta = (float)(i - 1) / (2 * MAXRES) * MathHelper.TwoPi - MathHelper.PiOver2;
                    x = (float)Math.Cos(theta);
                    y = (float)Math.Sin(theta);
                    distFromCenter = 1;
                }
                tri[iVertexBase + i] = new VertexPositionNormalTexture(new Vector3(x, y, 0), new Vector3(distFromCenter, 0, 0), new Vector2(1, 1));
            }

            // right halfdisc indices
            iIndex = 0;
            for (int iPrim = 0; iPrim < MAXRES; iPrim++)
            {
                indices[iIndexBase + iIndex++] = (short)(iVertexBase + 0);
                indices[iIndexBase + iIndex++] = (short)(iVertexBase + iPrim + 1);
                indices[iIndexBase + iIndex++] = (short)(iVertexBase + iPrim + 2);
            }

            GraphicsDevice device = SB.graphicsDevice;

            vb = new VertexBuffer(device, VertexPositionNormalTexture.VertexDeclaration, numVertices, BufferUsage.None);
            vb.SetData<VertexPositionNormalTexture>(tri);
            vdecl = VertexPositionNormalTexture.VertexDeclaration;

            ib = new IndexBuffer(device, IndexElementSize.SixteenBits, numIndices * 2, BufferUsage.None);
            ib.SetData<short>(indices);
        }
        public static void renderEffectLines(List<Line> lineList, float radius, Color color, String effect)
        {
            GraphicsDevice device = SB.graphicsDevice;

            // Glow es un tipo, Modern es otro
            lineEffect.CurrentTechnique = lineEffect.Techniques[effect];
            EffectPass pass = lineEffect.CurrentTechnique.Passes[0];

            device.Indices = ib;

            radiusParameter.SetValue(radius);
            for (int i = 0; i < lineList.Count; i++)
            {
                Matrix worldViewProjMatrix = lineList[i].WorldMatrix() * Camera2D.view * Camera2D.projection;
                wvpMatrixParameter.SetValue(worldViewProjMatrix);
                lengthParameter.SetValue(lineList[i].rho);
                lineColorParameter.SetValue(color.ToVector4());
                pass.Apply();
                device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, numVertices, 0, numPrimitives);
                if (lineList[i].opaque)
                {
                    worldViewProjMatrix = lineList[i].WorldMatrixTall() * Camera2D.view * Camera2D.projection;
                    wvpMatrixParameter.SetValue(worldViewProjMatrix);
                    lineColorParameter.SetValue(Color.White.ToVector4());
                    pass.Apply();
                    device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, numVertices, 0, numPrimitives);
                }
            }

            //SB.graphics.GraphicsDevice.RenderState.CullMode = CullMode.None;
            //SB.graphics.GraphicsDevice.RenderState.AlphaBlendEnable = false;
            //SB.graphics.GraphicsDevice.VertexDeclaration = SB.quad_vertex_declaration;
        }
    }
}