using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    class MinigameEpilepticMacedonia : StateGame
    {
        public MinigameEpilepticMacedonia(string level)
            : base(level)
        {
            Cinematic cinematic = new Cinematic();
            ActorEvent ae1 = new ActorEvent(GamerManager.getMainPlayer(), false);
            ae1.setAt(new Vector3(0.0f, -700.0f, 0.0f));
            ae1.moveTo(new Vector3(0.0f, 0.0f, 0.0f), 200.0f);

            DialogEvent de1 = new DialogEvent(tDialogCharacter.Wish, "Woo macedonia! Yo te ayudaré!", 70.0f);

            cinematic.events.Add((CinematicEvent)ae1);
            cinematic.events.Add((CinematicEvent)de1);
            CinematicManager.Instance.addCinematic("epilepticMacedoniaIntro", cinematic);
        }
        
        public override void update()
        {
            base.update();

            Vector3 position;
            position.X = Calc.randomScalar(Camera2D.screenWithMargins.Left, Camera2D.screenWithMargins.Right);
            position.Y = Camera2D.screenWithMargins.Bottom;
            position.Z = 0.0f;
            EnemyManager.Instance.addEnemy("Tomato", position);
        }
        
        public override void dispose()
        {
        }
    }
}