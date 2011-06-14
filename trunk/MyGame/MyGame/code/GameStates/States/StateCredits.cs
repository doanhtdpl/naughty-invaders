using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    class StateCredits : StateGame
    {
        float time = 3;

        public StateCredits()
            : base("credits")
        {
        }

        public override void initialize()
        {
            base.initialize();

            GamerManager.getMainPlayer().mode = Player.tMode.SavingItems;

            SoundManager.Instance.playSong("Naughty_jingle", true);
        }

        public override void update()
        {
            GamerManager.updatePlayers();
            EnemyManager.Instance.update();
            LevelManager.Instance.update();
            ProjectileManager.Instance.update();
            ParticleManager.Instance.update();
            OrbManager.Instance.update();
            CameraManager.Instance.update();
            GUIManager.Instance.update();
            CinematicManager.Instance.update();
            TriggerManager.Instance.update();
            SoundManager.Instance.update();
            SB.cam.update();

            if (CameraManager.Instance.isIdle())
            {
                time -= SB.dt;
            }

            if ((GamerManager.getMainControls().B_firstPressed() || GamerManager.getMainControls().Start_firstPressed() || GamerManager.getMainControls().Back_firstPressed() || time < 0) && !TransitionManager.Instance.isFading())
            {
                TransitionManager.Instance.changeStateWithFade(StateManager.tGameState.Menu, 1, null, 0.5f, Color.Black);
            }
        }

        public override void render()
        {
            GraphicsManager.Instance.graphicsDevice.Clear(SB.BGColor);

            EntityManager.Instance.render();
            LevelManager.Instance.render();
            CinematicManager.Instance.render();
        }

    }
}