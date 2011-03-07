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
            graphics.GraphicsProfile = GraphicsProfile.Reach;
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
            SB.graphicsDevice = graphics.GraphicsDevice;
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


            // if we want to test gameplay, we will manually add a gamer
#if ONLY_GAMEPLAY
            GamerManager.createGamerEntity(PlayerIndex.One, true);
#endif

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // load and initialize stuff (pe: quad renderer)
            SB.loadContent();
            LineManager.loadContent();
            TEX.loadContent();
            StringManager.loadContent();
            GUI.loadContent();
        }

        protected override void UnloadContent()
        {
            TextureManager.Instance.dispose();
            EnemyManager.Instance.dispose();
            ProjectileManager.Instance.dispose();
        }

        protected override void Update(GameTime gameTime)
        {
            SB.gameTime = gameTime;
            SB.dt = gameTime.ElapsedGameTime.Milliseconds * 0.001f;

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
            //GraphicsDevice.Clear(new Color(79, 98, 37));
            stateManager.render();
            base.Draw(gameTime);
        }
    }
}
