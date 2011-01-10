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
        public static string gameName = "MyGame";
        SpriteBatch spriteBatch;
        StateManager stateManager = new StateManager();

        public Game()
        {
            SB.graphics = new GraphicsDeviceManager(this);
            SB.graphics.GraphicsProfile = GraphicsProfile.Reach;
            SB.content = new ContentManager(Services, "Content");
            SB.random = new Random();
        }

        protected override void Initialize()
        {
            SB.graphics.PreferredBackBufferWidth = 1280;
            SB.graphics.PreferredBackBufferHeight = 720;
            SB.graphics.IsFullScreen = false;
            SB.graphics.ApplyChanges();
            Screen.screenCenterX = SB.graphics.PreferredBackBufferWidth / 2;
            Screen.screenCenterY = SB.graphics.PreferredBackBufferHeight / 2;
            SB.cam = new Camera2D();
            Screen.aspect = (float)SB.graphics.PreferredBackBufferWidth / (float)SB.graphics.PreferredBackBufferHeight;
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
            spriteBatch = new SpriteBatch(GraphicsDevice);
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
            GraphicsDevice.Clear(Color.CornflowerBlue);
            stateManager.render();
            base.Draw(gameTime);
        }
    }
}
