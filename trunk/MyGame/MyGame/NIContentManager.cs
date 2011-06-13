using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class NIContentManager : ContentManager
    {
        public NIContentManager(IServiceProvider serviceProvider, string path)
            : base(serviceProvider, path)
        { }


        Dictionary<string, object> loadedAssets = new Dictionary<string, object>();
        List<IDisposable> disposableAssets = new List<IDisposable>();

        Dictionary<string, object> loadedTextures = new Dictionary<string, object>();
        List<IDisposable> disposableTextures = new List<IDisposable>();


        public override T Load<T>(string assetName)
        {
            if (loadedAssets.ContainsKey(assetName))
                return (T)loadedAssets[assetName];

            if (loadedTextures.ContainsKey(assetName))
                return (T)loadedTextures[assetName];

            T asset = ReadAsset<T>(assetName, RecordDisposableAsset);

            if(asset is Texture2D || asset is SpriteFont)
                loadedTextures.Add(assetName, asset);
            else
                loadedAssets.Add(assetName, asset);

            return asset;
        }

        
        public override void Unload()
        {
            foreach (IDisposable disposable in disposableTextures)
            {
                disposable.Dispose();
            }

            loadedTextures.Clear();
            disposableTextures.Clear();
        }


        void RecordDisposableAsset(IDisposable disposable)
        {
            if (disposable is Texture2D || disposable is SpriteFont)
                disposableTextures.Add(disposable);
            else
                disposableAssets.Add(disposable);
        }
    }
}
