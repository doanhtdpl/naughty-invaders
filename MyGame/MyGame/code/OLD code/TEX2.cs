using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    public class TEX2
    {
        public static int[] index = { 2, 0, 1, 3, 2, 1 };

        public Texture2D texture;
        public Texture2D normalmap;
        public VertexPositionColorTexture[] vertex, vertexMirror;
        public Vector2 gameSize;
        public float Zrender = 0f;

        public static Effect lightEffect;
        private static EffectParameter WVP_param;
        private static EffectParameter W_param;
        private static EffectParameter fx_numlights;
        private static EffectParameter fx_lights;
        private static EffectParameter fx_rotation;
        private static EffectParameter fx_mirrored;

        public static void loadContent()
        {
            lightEffect = SB.content.Load<Effect>("Content/Shaders/light2d");
            WVP_param = lightEffect.Parameters["WVP_Matrix"];
            W_param = lightEffect.Parameters["W_Matrix"];
            fx_rotation = lightEffect.Parameters["fx_Rotation"];
            fx_mirrored = lightEffect.Parameters["fx_Mirrored"];
            fx_numlights = lightEffect.Parameters["fx_NumLights"];
            fx_lights = lightEffect.Parameters["lights"];
        }

        public void initTEX(string path, float gameSizeX, float gameSizeY, float Z)
        {
            texture = SB.content.Load<Texture2D>("Content"+path);
            normalmap = SB.content.Load<Texture2D>("Content" + path + "_map");
            vertex = new VertexPositionColorTexture[4];
            vertexMirror = new VertexPositionColorTexture[4];
            TextureManager.Instance.mapTexture(gameSizeX, gameSizeY, ref vertex);
            TextureManager.Instance.mapMirrorTexture(gameSizeX, gameSizeY, ref vertexMirror);
            Zrender = Z;
            gameSize = new Vector2(gameSizeX, gameSizeY);
        }
        public void initTEX(string path, float gameSizeX, float gameSizeY)
        {
            initTEX(path, gameSizeX, gameSizeY, 0);
        }
        // este init TEX es para los edificios, que se deben cargar con el tamaño de la textura (escalado)
        public void initScaledTEX(string path, float scaleFromOriginal, float Z)
        {
            texture = SB.content.Load<Texture2D>("Content" + path);
            normalmap = SB.content.Load<Texture2D>("Content" + path + "_map");
            vertex = new VertexPositionColorTexture[4];
            vertexMirror = new VertexPositionColorTexture[4];
            gameSize = new Vector2(texture.Width * scaleFromOriginal, texture.Height * scaleFromOriginal);
            TextureManager.Instance.mapTexture(gameSize.X, gameSize.Y, ref vertex);
            TextureManager.Instance.mapMirrorTexture(gameSize.X, gameSize.Y, ref vertexMirror);
            Zrender = Z;
        }

        public void render(bool mirrored, Vector2 position, Vector2 rotationPoint, float rotation)
        {
            // inicializamos texturas y variables
            GraphicsManager.Instance.graphicsDevice.Textures[0] = texture;
            GraphicsManager.Instance.graphicsDevice.Textures[1] = normalmap;
            W_param.SetValue(SB.getWorldMatrix(position.X, position.Y, Zrender, rotationPoint, rotation, gameSize));
            WVP_param.SetValue(SB.getRotationWVP(position.X, position.Y, Zrender, rotationPoint, rotation, gameSize));
            fx_rotation.SetValue(rotation);
            fx_mirrored.SetValue(mirrored);
            // preparamos el efecto y la técnica
            lightEffect.CurrentTechnique.Passes[0].Apply();
            if (mirrored)
                GraphicsManager.Instance.graphicsDevice.DrawUserIndexedPrimitives<VertexPositionColorTexture>(
                    PrimitiveType.TriangleList, vertexMirror, 0, 4, index, 0, 2);
            else
                GraphicsManager.Instance.graphicsDevice.DrawUserIndexedPrimitives<VertexPositionColorTexture>(
                    PrimitiveType.TriangleList, vertex, 0, 4, index, 0, 2);
        }
    }
}