using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    public class TEX
    {
        public static short[] index = { 2, 0, 1, 3, 2, 1 };

        public Texture2D texture;
        public VertexPositionColorTexture[] vertex;
        public Vector2 gameSize;

        private static Effect alphaEffect;
        private static EffectParameter WVP_param;
        private static EffectParameter fx_alpha;
        private static EffectParameter fx_texture;

        private static Effect basicEffect;
        private static EffectParameter bWVP_param;
        private static EffectParameter bfx_texture;

        public static void loadContent()
        {
            alphaEffect = SB.content.Load<Effect>("effects/alpha");
            WVP_param = alphaEffect.Parameters["WVP_Matrix"];
            fx_alpha = alphaEffect.Parameters["fx_Alpha"];
            fx_texture = alphaEffect.Parameters["fx_Texture"];

            basicEffect = SB.content.Load<Effect>("effects/basic");
            bWVP_param = basicEffect.Parameters["WVP_Matrix"];
            bfx_texture = basicEffect.Parameters["fx_Texture"];
        }

        public void initTEX(string path, Vector2 gameSize)
        {
            texture = SB.content.Load<Texture2D>(path);
            vertex = new VertexPositionColorTexture[4];
            TextureManager.Instance.mapTexture(gameSize.X, gameSize.Y, ref vertex);

            this.gameSize = gameSize;
        }
        public void initTEX(string path, float gameSizeX, float gameSizeY)
        {
            initTEX(path, new Vector2(gameSizeX, gameSizeY));
        }
        public void initTEX(Texture2D texture, float gameSizeX, float gameSizeY)
        {
            this.texture = texture;
            vertex = new VertexPositionColorTexture[4];
            TextureManager.Instance.mapTexture(gameSizeX, gameSizeY, ref vertex);
            gameSize.X = gameSizeX;
            gameSize.Y = gameSizeY;
        }

        #region All renders
        public void render(Vector2 position, float rotation)
        {
            render(position, rotation, 0);
        }
        public void render(Vector2 position, float rotation, float heightZ)
        {
            render(position, rotation, 1, heightZ);
        }
        public void render(Vector2 position, float rotation, float scale, float heightZ)
        {
            bWVP_param.SetValue(SB.getWVP(position.X, position.Y, heightZ, scale, rotation, gameSize.X / 2));
            bfx_texture.SetValue(texture);

            basicEffect.Techniques[0].Passes[0].Apply();

            SB.graphics.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColorTexture>(
                PrimitiveType.TriangleList, vertex, 0, 4, index, 0, 2);
        }

        public void renderFromDownLeft(Vector2 position, float rotation, float scale, float heightZ)
        {
            renderFromDownLeft(position, rotation, new Vector2(scale, scale), heightZ);
        }
        public void renderFromDownLeft(Vector2 position, float rotation, Vector2 scale, float heightZ)
        {
            bWVP_param.SetValue(SB.getWVP(position.X, position.Y, heightZ, scale, rotation, 0));
            bfx_texture.SetValue(texture);

            basicEffect.Techniques[0].Passes[0].Apply();

            SB.graphics.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColorTexture>(
                PrimitiveType.TriangleList, vertex, 0, 4, index, 0, 2);
        }

        /// <summary>
        /// Description for tex render with rotations.</summary>
        /// <param name="position"> The position in the world relative to the center of the texture.</param>
        /// <param name="rotationPoint"> The rotation point relative to the position parameter.</param>
        /// <param name="rotation"> The angle of rotation.</param>
        /// <remarks> Example, drawing a gun in front of the player. Position = (playerX + 30, playerY)
        /// To rotate from the middle of the player, rotationPoint = (-30, 0) </remarks>
        public void render(Vector2 position, Vector2 rotationPoint, float rotation)
        {
            bfx_texture.SetValue(texture);
            bWVP_param.SetValue(SB.getWVP(position, rotationPoint, rotation, gameSize));

            basicEffect.Techniques[0].Passes[0].Apply();

            SB.graphics.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColorTexture>(
                PrimitiveType.TriangleList, vertex, 0, 4, index, 0, 2);
        }
        public void render(Vector2 position, float height, Vector2 rotationPoint, float rotation)
        {
            bfx_texture.SetValue(texture);
            bWVP_param.SetValue(SB.getWVP(position, rotationPoint, rotation, gameSize, height));
            basicEffect.Techniques[0].Passes[0].Apply();
            SB.graphics.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColorTexture>(
                PrimitiveType.TriangleList, vertex, 0, 4, index, 0, 2);
        }
        public void alphaRender(Vector2 position, float rotation, float alpha, float Zrender)
        {
            alphaRender(position, rotation, alpha, Zrender, gameSize.X / 2);
        }
        public void alphaRender(Vector2 position, float rotation, float alpha, float Zrender, float radius)
        {
            WVP_param.SetValue(SB.getRotationWVP(position.X, position.Y, Zrender, rotation, radius));
            fx_texture.SetValue(texture);
            fx_alpha.SetValue(alpha);

            alphaEffect.Techniques[0].Passes[0].Apply();

            SB.graphics.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionColorTexture>(
                PrimitiveType.TriangleList, vertex, 0, 4, index, 0, 2);
        }
        #endregion
    }
}
