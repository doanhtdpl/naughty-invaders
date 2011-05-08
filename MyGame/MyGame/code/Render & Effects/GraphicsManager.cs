using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyGame
{
    // all renderable entities must go here, to sort them properly and avoid blending problems
    class GraphicsManager
    {
        static GraphicsManager instance = null;
        GraphicsManager()
        {
        }
        public static GraphicsManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GraphicsManager();
                }
                return instance;
            }
        }

        public GraphicsDevice graphicsDevice { set; get; }
        public SpriteBatch spriteBatch { set; get; }

        public static Vector3[] vertexScreen;

        public void loadContent()
        {
            QuadRenderer.loadContent();
            initializeRender();
            spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public void spriteBatchBegin()
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
        }
        public void spriteBatchEnd()
        {
            spriteBatch.End();
        }

        public void initializeRender()
        {
            SamplerState ss = new SamplerState();
            // set clamp to address mode
            ss.AddressU = TextureAddressMode.Clamp;
            ss.AddressV = TextureAddressMode.Clamp;
            GraphicsManager.Instance.graphicsDevice.SamplerStates[0] = ss;

            vertexScreen = new Vector3[4];
            vertexScreen[0] = new Vector3(1000, 1000, 0);
            vertexScreen[1] = new Vector3(1000, -1000, 0);
            vertexScreen[2] = new Vector3(-1000, 1000, 0);
            vertexScreen[3] = new Vector3(-1000, -1000, 0);

            QuadRenderer.initialize();
        }
    }
}
