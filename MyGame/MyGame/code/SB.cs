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
        static public int width, height;
        static public ContentManager content;
        static public Camera2D cam;
        public static GameTime gameTime;   
        public static float dt;
        static public Random random;
        public const float SafeAreaPortion = 0.05f;
        public static SpriteFont font;
        public static Color BGColor = Color.Black;
        #endregion

        public static void loadContent()
        {
            font = content.Load<SpriteFont>("fonts/font");
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
        public static Matrix getWorldMatrix(Vector3 position, float rotation, float scale)
        {
            Matrix world =
                Matrix.CreateScale(scale)
                * Matrix.CreateRotationZ(rotation)
                * Matrix.CreateTranslation(new Vector3(position.X, position.Y, position.Z));
            return world;
        }
        public static Matrix getWorldMatrix(float posX, float posY, float posZ, Vector2 rotationPoint, float rotation, Vector2 rotationCorrection)
        {
            Matrix world =
                 Matrix.CreateTranslation(new Vector3(-rotationPoint.X - rotationCorrection.X / 2, -rotationPoint.Y - rotationCorrection.Y / 2, 0))
                * Matrix.CreateRotationZ(rotation)
                * Matrix.CreateTranslation(new Vector3(posX + rotationPoint.X, posY + rotationPoint.Y, posZ));
            return world;
        }
    }
}