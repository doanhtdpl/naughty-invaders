using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyGame
{
    // make the class static to benefit from the C# extension methods and make our Texture.render( )...
    public static class QuadRenderer
    {
        private static Effect quadEffect;
        private static EffectParameter WVP_param;
        private static EffectParameter fx_texture;

        public static int[] index = { 2, 0, 1, 3, 2, 1 };
        public static VertexPositionColorTexture[] vertex, vertexUVs;

        public static void initialize()
        {
            vertex = new VertexPositionColorTexture[4];
            vertex[0].Position = new Vector3(-0.5f,-0.5f, 0.0f);
            vertex[0].TextureCoordinate = new Vector2(0.0f, 1.0f);
            vertex[0].Color = new Color(1, 1, 1, 1);
            vertex[1].Position = new Vector3(-0.5f, 0.5f, 0.0f);
            vertex[1].TextureCoordinate = new Vector2(0.0f, 0.0f);
            vertex[1].Color = new Color(1, 1, 1, 1);
            vertex[2].Position = new Vector3(0.5f, -0.5f, 0.0f);
            vertex[2].TextureCoordinate = new Vector2(1.0f, 1.0f);
            vertex[2].Color = new Color(1, 1, 1, 1);
            vertex[3].Position = new Vector3(0.5f, 0.5f, 0.0f);
            vertex[3].TextureCoordinate = new Vector2(1.0f, 0.0f);
            vertex[3].Color = new Color(1, 1, 1, 1);

            vertexUVs = new VertexPositionColorTexture[4];
            vertexUVs[0].Position = new Vector3(-0.5f, -0.5f, 0.0f);
            vertexUVs[0].Color = new Color(1, 1, 1, 1);
            vertexUVs[1].Position = new Vector3(-0.5f, 0.5f, 0.0f);
            vertexUVs[1].Color = new Color(1, 1, 1, 1);
            vertexUVs[2].Position = new Vector3(0.5f, -0.5f, 0.0f);
            vertexUVs[2].Color = new Color(1, 1, 1, 1);
            vertexUVs[3].Position = new Vector3(0.5f, 0.5f, 0.0f);
            vertexUVs[3].Color = new Color(1, 1, 1, 1);
        }

        public static void loadContent()
        {
            quadEffect = SB.content.Load<Effect>("effects/Basic");
            WVP_param = quadEffect.Parameters["WVP_Matrix"];
            fx_texture = quadEffect.Parameters["fx_Texture"];
        }

        static Matrix getWVPMatrix(Vector2 position, float rotation, Vector2 scale)
        {
            return Matrix.CreateTranslation(new Vector3(0, 0, 0))
                * Matrix.CreateScale(scale.X, scale.Y, 1)
                * Matrix.CreateRotationZ(rotation)
                * Matrix.CreateTranslation(new Vector3(position.X, position.Y, 0))
                * Camera2D.view * Camera2D.projection;
        }

        public static void render(this Texture texture, Vector2 position, float rotation, Vector2 scale, bool customUVs = false)
        {
            WVP_param.SetValue(getWVPMatrix(position, rotation, scale));
            fx_texture.SetValue(texture);

            quadEffect.Techniques[0].Passes[0].Apply();

            if (customUVs)
            {
                SB.graphics.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColorTexture>(
                    PrimitiveType.TriangleList, vertexUVs, 0, 4, index, 0, 2);
            }
            else
            {
                SB.graphics.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColorTexture>(
                    PrimitiveType.TriangleList, vertex, 0, 4, index, 0, 2);
            }
        }

        public static void renderWithUVs(this Texture texture, Vector2 position, float rotation, Vector2 scale, Vector2 startingUV, Vector2 endingUV)
        {
            vertexUVs[0].TextureCoordinate = new Vector2(startingUV.X, endingUV.Y);
            vertexUVs[1].TextureCoordinate = startingUV;
            vertexUVs[2].TextureCoordinate = endingUV;
            vertexUVs[3].TextureCoordinate = new Vector2(endingUV.X, startingUV.Y);
            render(texture, position, rotation, scale, true);
        }
    }
}
