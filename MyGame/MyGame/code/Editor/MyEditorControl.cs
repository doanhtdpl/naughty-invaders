using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Windows.Forms;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class MyEditorControl : GraphicsDeviceControl
    {

        StateManager stateManager = new StateManager();

        GameTime gameTime;
        Stopwatch elapsedTime = new Stopwatch();
        Stopwatch totalTime = new Stopwatch();

        /// <summary>
        /// Initializes the control.
        /// </summary>
        protected override void Initialize()
        {
            totalTime.Start();

            SB.graphicsDevice = GraphicsDevice;

            //Constructor
            SB.content = new ContentManager(Services, "Content");
            SB.random = new Random();

            //Initialize
            SB.width = 1280;
            SB.height = 720;
            Screen.screenCenterX = SB.width / 2;
            Screen.screenCenterY = SB.height / 2;
            SB.cam = new Camera2D();
            Screen.aspect = (float)SB.width / (float)SB.height;
            Camera2D.projection = Matrix.CreatePerspectiveFieldOfView(Microsoft.Xna.Framework.MathHelper.ToRadians(45), Screen.aspect, 1, 10000000);
            SB.cam.init(800);

            //Load Content
            SB.loadContent();
            LineManager.loadContent();
            TEX.loadContent();
            StringManager.loadContent();
            GUI.loadContent();

            // if we want to test gameplay, we will manually add a gamer
#if ONLY_GAMEPLAY
            GamerManager.createGamerEntity(PlayerIndex.One, true);
#endif

            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };
        }

        /// <summary>
        /// Draws the control.
        /// </summary>
        protected override void Draw()
        {
            UpdateGameTime();

            //Update
            SB.gameTime = gameTime;
            SB.dt = gameTime.ElapsedGameTime.Milliseconds * 0.001f;

            GamerManager.updateInputs();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                Application.Exit();
            }

            stateManager.update();

            //Render
            GraphicsDevice.Clear(Color.CornflowerBlue);
            stateManager.render();
        }

// Updates the GameTime object instead of relying on Game
        void UpdateGameTime()
        {
            // Recreate the GameTime with the current values
            gameTime = new GameTime(totalTime.Elapsed, elapsedTime.Elapsed);
 
            // Restart the elapsed timer that keeps track of time between frames
            elapsedTime.Reset();
            elapsedTime.Start();
        }

    }
}
