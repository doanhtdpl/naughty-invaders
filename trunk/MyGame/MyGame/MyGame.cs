using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace MyGame
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        StateManager stateManager = new StateManager();

#if !EDITOR
        GraphicsDeviceManager graphics;
#endif

        public Game()
        {
#if !EDITOR
            graphics = new GraphicsDeviceManager(this);
#endif
            SB.content = new ContentManager(Services, "Content");
            SB.random = new Random();
        }

        protected override void Initialize()
        {
#if DEBUG
            this.IsMouseVisible = true;
#endif
            SB.width = 1280;
            SB.height = 720;
#if !EDITOR
            GraphicsManager.Instance.graphicsDevice = graphics.GraphicsDevice;
            graphics.PreferredBackBufferWidth = SB.width;
            graphics.PreferredBackBufferHeight = SB.height;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
#endif
            Screen.screenCenterX = SB.width / 2;
            Screen.screenCenterY = SB.height / 2;
            SB.cam = new Camera2D();
            Screen.aspect = (float)SB.width / (float)SB.height;
            Camera2D.projection = Matrix.CreatePerspectiveFieldOfView(Microsoft.Xna.Framework.MathHelper.ToRadians(45), Screen.aspect, 1, 10000000);
            SB.cam.init(800);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // load and initialize stuff (pe: quad renderer)
            SB.loadContent();
            GraphicsManager.Instance.loadContent();
            StringManager.loadContent();
            GUIManager.Instance.loadContent();

            //EditorHelper.Instance.loadNewLevel("final_Level01");
        }

        protected override void UnloadContent()
        {
            TextureManager.Instance.dispose();
            EnemyManager.Instance.dispose();
            ProjectileManager.Instance.dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            SB.updateGameTime(gameTime);

            GamerManager.updateInputs();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            stateManager.update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            stateManager.render();
            base.Draw(gameTime);
        }
    }
}
