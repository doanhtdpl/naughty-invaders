using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    public class SB
    {
        #region fields
        //static public GraphicsDeviceManager graphics;
        static public GraphicsDevice graphicsDevice;
        static public int width, height;
        static public ContentManager content;
        static public SpriteBatch spriteBatch;
        static public Camera2D cam;
        static public Matrix worldZero;
        public static GameTime gameTime;   
        public static float dt;
        static public Random random;
        public const float SafeAreaPortion = 0.05f;
        public static BasicEffect quad_effect;
        public static VertexDeclaration quad_vertex_declaration;
        public static int[] index = { 2, 0, 1, 3, 2, 1 };
        public static SpriteFont font;
        public static Vector3[] vertexScreen;
        #endregion

        public static void loadContent()
        {
            content.RootDirectory = "Content";
            spriteBatch = new SpriteBatch(graphicsDevice);
            font = content.Load<SpriteFont>("fonts/font");
            worldZero = Matrix.CreateTranslation(new Vector3(0, 0, -5));

            QuadRenderer.loadContent();

            initializeRender();
        }

        public static void initializeRender()
        {
            quad_effect = new BasicEffect(graphicsDevice);
            quad_effect.TextureEnabled = true;
            quad_vertex_declaration = VertexPositionTexture.VertexDeclaration;
            vertexScreen = new Vector3[4];
            vertexScreen[0] = new Vector3(1000, 1000, 0);
            vertexScreen[1] = new Vector3(1000, -1000, 0);
            vertexScreen[2] = new Vector3(-1000, 1000, 0);
            vertexScreen[3] = new Vector3(-1000, -1000, 0);

            QuadRenderer.initialize();
        }
        public static void beginRender()
        {
            //SB.graphics.GraphicsDevice.RenderState.DepthBufferEnable = true;
            //graphics.GraphicsDevice.RenderState.AlphaFunction = CompareFunction.GreaterEqual;
            //graphics.GraphicsDevice.RenderState.ReferenceAlpha = 1;

            //graphics.GraphicsDevice.RenderState.AlphaBlendEnable = false;
            //graphics.GraphicsDevice.RenderState.AlphaTestEnable = true;
            //graphics.GraphicsDevice.RenderState.DepthBufferWriteEnable = true;

            quad_effect.View = Camera2D.view;
            quad_effect.Projection =Camera2D.projection;
        }
        public static void beginAlphaRender()
        {
            //graphics.GraphicsDevice.RenderState.AlphaBlendEnable = true;
            //graphics.GraphicsDevice.RenderState.AlphaBlendOperation = BlendFunction.Add;
            //graphics.GraphicsDevice.RenderState.SourceBlend = Blend.SourceAlpha;
            //graphics.GraphicsDevice.RenderState.DestinationBlend = Blend.InverseSourceAlpha;
            //graphics.GraphicsDevice.RenderState.SeparateAlphaBlendEnabled = false;
        }

        public static Matrix getWVP(Vector2 v)
        {
            Matrix world = Matrix.CreateTranslation(new Vector3(v, 0));
            return world * Camera2D.view *Camera2D.projection;
        }
        public static Matrix getWVP(float posX, float posY, float posZ, float scale, float rotation, float radius)
        {
            Matrix world =
               Matrix.CreateTranslation(new Vector3(-radius, -radius, 0))
               * Matrix.CreateScale(scale)
               * Matrix.CreateRotationZ(rotation)
               * Matrix.CreateTranslation(new Vector3(posX, posY, posZ));
            return world * Camera2D.view *Camera2D.projection;
        }
        public static Matrix getWVP(float posX, float posY, float posZ, Vector2 scale, float rotation, float radius)
        {
            Matrix world =
               Matrix.CreateTranslation(new Vector3(-radius, -radius, 0))
               * Matrix.CreateScale(new Vector3(scale, 0))
               * Matrix.CreateRotationZ(rotation)
               * Matrix.CreateTranslation(new Vector3(posX, posY, posZ));
            return world * Camera2D.view *Camera2D.projection;
        }
        public static Matrix getWVP(Vector2 position, Vector2 rotationPoint, float rotation, Vector2 gameSize)
        {
            return Matrix.CreateTranslation(new Vector3(-rotationPoint.X - gameSize.X / 2, -rotationPoint.Y - gameSize.Y / 2, 0))
                * Matrix.CreateRotationZ(rotation)
                * Matrix.CreateTranslation(new Vector3(position.X + rotationPoint.X, position.Y + rotationPoint.Y, 0))
                * Camera2D.view *Camera2D.projection;
        }
        public static Matrix getWVP(Vector3 position, float rotation, Vector2 gameSize)
        {
            return Matrix.CreateTranslation(new Vector3(- gameSize.X / 2, - gameSize.Y / 2, 0))
                * Matrix.CreateRotationZ(rotation)
                * Matrix.CreateTranslation(new Vector3(position.X, position.Y, position.Z))
                * Camera2D.view * Camera2D.projection;
        }
        public static Matrix getWVP(Vector2 position, Vector2 rotationPoint, float rotation, Vector2 gameSize, float height)
        {
            return Matrix.CreateTranslation(new Vector3(-rotationPoint.X - gameSize.X / 2, -rotationPoint.Y - gameSize.Y / 2, height))
                * Matrix.CreateRotationZ(rotation)
                * Matrix.CreateTranslation(new Vector3(position.X + rotationPoint.X, position.Y + rotationPoint.Y, 0))
                * Camera2D.view *Camera2D.projection;
        }

        public static Matrix getRotationWVP(float posX, float posY, float posZ, float rotation, float radius)
        {
            Matrix world = Matrix.CreateTranslation(new Vector3(-radius, -radius, 0))
                * Matrix.CreateRotationZ(rotation)
                * Matrix.CreateTranslation(new Vector3(posX, posY, posZ));
            return world * Camera2D.view *Camera2D.projection;
        }
        public static Matrix getRotationWVP(float posX, float posY, float posZ, Vector2 rotationPoint, float rotation, Vector2 rotationCorrection)
        {
            Matrix world = Matrix.CreateTranslation(new Vector3(-rotationPoint.X - rotationCorrection.X / 2, -rotationPoint.Y - rotationCorrection.Y / 2, 0))
                * Matrix.CreateRotationZ(rotation)
                * Matrix.CreateTranslation(new Vector3(posX + rotationPoint.X, posY + rotationPoint.Y, 0));
            return world * Camera2D.view *Camera2D.projection;
        }
        public static Matrix getWorldMatrix(float posX, float posY, float posZ, Vector2 rotationPoint, float rotation, Vector2 rotationCorrection)
        {
            Matrix world =
                 Matrix.CreateTranslation(new Vector3(-rotationPoint.X - rotationCorrection.X / 2, -rotationPoint.Y - rotationCorrection.Y / 2, 0))
                * Matrix.CreateRotationZ(rotation)
                * Matrix.CreateTranslation(new Vector3(posX + rotationPoint.X, posY + rotationPoint.Y, posZ));
            return world;
        }

        public static void renderPolygon(Texture2D texture, int[] index, VertexPositionTexture[] vertex, int primitiveCount)
        {
            quad_effect.Texture = texture;
            quad_effect.World = Matrix.CreateTranslation(Vector3.Zero);

            foreach (EffectPass pass in quad_effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.DrawUserIndexedPrimitives<VertexPositionTexture>(
                    PrimitiveType.TriangleList, vertex, 0, vertex.Length, index, 0, primitiveCount);
            }
        }
        public static void renderPolygonAlpha(Texture2D texture, int[] index, VertexPositionTexture[] vertex, int primitiveCount, float alpha)
        {
            //bool alphaState = graphics.GraphicsDevice.RenderState.AlphaBlendEnable;
            //graphics.GraphicsDevice.RenderState.AlphaBlendEnable = true;
            //graphics.GraphicsDevice.RenderState.SourceBlend = Blend.SourceAlpha;
            //graphics.GraphicsDevice.RenderState.DestinationBlend = Blend.InverseSourceAlpha;
            quad_effect.Alpha = alpha;
            quad_effect.Texture = texture;

            quad_effect.World = Matrix.CreateTranslation(Vector3.Zero);

            foreach (EffectPass pass in quad_effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.DrawUserIndexedPrimitives<VertexPositionTexture>(
                    PrimitiveType.TriangleList, vertex, 0, vertex.Length, index, 0, primitiveCount);
            }
            //graphics.GraphicsDevice.RenderState.AlphaBlendEnable = alphaState;
        }
        public static void renderTriangleAlpha(Texture2D texture, Vector2 position, float angle, float orientation, float alpha)
        {
            //bool alphaState = graphics.GraphicsDevice.RenderState.AlphaBlendEnable;
            //graphics.GraphicsDevice.RenderState.AlphaBlendEnable = true;
            //graphics.GraphicsDevice.RenderState.SourceBlend = Blend.SourceAlpha;
            //graphics.GraphicsDevice.RenderState.DestinationBlend = Blend.InverseSourceAlpha;
            quad_effect.Alpha = alpha;
            quad_effect.Texture = texture;

            quad_effect.World = Matrix.CreateTranslation(Vector3.Zero);

            VertexPositionTexture[] sight = new VertexPositionTexture[5];
            sight[0].Position = new Vector3(position, 0);
            float orientationStart = orientation - angle / 2;
            sight[1].Position.X = position.X + (float)(Math.Cos(orientationStart) * 500);
            sight[1].Position.Y = position.Y + (float)(Math.Sin(orientationStart) * 500);
            sight[1].Position.Z = 0;
            sight[2].Position.X = position.X + (float)(Math.Cos(orientationStart + angle) * 500);
            sight[2].Position.Y = position.Y + (float)(Math.Sin(orientationStart + angle) * 500);
            sight[2].Position.Z = 0;

            int[] newIndex = { 2, 0, 1 };

            foreach (EffectPass pass in quad_effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.DrawUserIndexedPrimitives<VertexPositionTexture>(
                    PrimitiveType.TriangleList, sight, 0, sight.Length, newIndex, 0, 1);
            }
            //graphics.GraphicsDevice.RenderState.AlphaBlendEnable = alphaState;
        }
    }
}