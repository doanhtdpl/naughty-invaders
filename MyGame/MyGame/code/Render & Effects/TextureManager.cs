using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class TextureManager
    {
        static TextureManager instance = null;
        TextureManager()
        {
        }
        public static TextureManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TextureManager();
                }
                return instance;
            }
        }

        Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        public Texture2D getTexture(string path)
        {
            if (textures.ContainsKey(path))
                if (textures.ContainsKey(path))
            {
                return textures[path];
            }

            Texture2D texture = SB.content.Load<Texture2D>("textures/" + path);
            textures[path] = texture;
            return texture;
        }
        public Texture2D getTexture(string textureFolder, string name)
        {
            return getTexture(textureFolder + "/" + name);
        }

        public Texture2D getColoredTexture(Color color)
        {
            Texture2D tex = new Texture2D(GraphicsManager.Instance.graphicsDevice, 1, 1);
            Color[] data = new Color[1];
            data[0] = color;
            tex.SetData<Color>(data);
            return tex;
        }

        public void getTextureData(Texture2D texture, ref Color[] textureData)
        {
            textureData = new Color[texture.Width * texture.Height];
            texture.GetData(textureData);
            textureData = convertTextureData(texture.Width, texture.Height, textureData);
        }

        public void mapTexture(float sizeX, float sizeY, ref VertexPositionColorTexture[] vertex)
        {
            vertex[0].Position = new Vector3(0, 0, 0.0f);
            vertex[0].TextureCoordinate = new Vector2(0.0f, 1.0f);
            vertex[0].Color = new Color(1, 1, 1, 1);
            vertex[1].Position = new Vector3(0, sizeY, 0.0f);
            vertex[1].TextureCoordinate = new Vector2(0.0f, 0.0f);
            vertex[1].Color = new Color(1, 1, 1, 1);
            vertex[2].Position = new Vector3(sizeX, 0, 0.0f);
            vertex[2].TextureCoordinate = new Vector2(1.0f, 1.0f);
            vertex[2].Color = new Color(1, 1, 1, 1);
            vertex[3].Position = new Vector3(sizeX, sizeY, 0.0f);
            vertex[3].TextureCoordinate = new Vector2(1.0f, 0.0f);
            vertex[3].Color = new Color(1, 1, 1, 1);
        }

        public void mapMirrorTexture(float sizeX, float sizeY, ref VertexPositionColorTexture[] vertex)
        {
            vertex[0].Position = new Vector3(0, 0, 0.0f);
            vertex[0].TextureCoordinate = new Vector2(1.0f, 1.0f);
            vertex[0].Color = new Color(1, 1, 1, 1);
            vertex[1].Position = new Vector3(0, sizeY, 0.0f);
            vertex[1].TextureCoordinate = new Vector2(1.0f, 0.0f);
            vertex[1].Color = new Color(1, 1, 1, 1);
            vertex[2].Position = new Vector3(sizeX, 0, 0.0f);
            vertex[2].TextureCoordinate = new Vector2(0.0f, 1.0f);
            vertex[2].Color = new Color(1, 1, 1, 1);
            vertex[3].Position = new Vector3(sizeX, sizeY, 0.0f);
            vertex[3].TextureCoordinate = new Vector2(0.0f, 0.0f);
            vertex[3].Color = new Color(1, 1, 1, 1);
        }

        public Color[] convertTextureData(int width, int height, Color[] textureData)
        {
            Color[] newTextureData = new Color[width * height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    newTextureData[y * width + x] = textureData[((height - 1) - y) * width + x];
                }
            }
            return newTextureData;
        }

        public void dispose()
        {
            textures.Clear();
        }
    }
}
