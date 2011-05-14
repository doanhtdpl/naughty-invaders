using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class StatePausedGame : GameState
    {
        Menu menu;

        void initializeMenu()
        {
            menu = new Menu();
            Vector2 scale = new Vector2(0.8f, 0.8f);
            MenuElement mbHeader = new MenuElement("smallHeader", new Vector2(0, 200), scale);
            MenuElement mb1 = new MenuElement("option1", new Vector2(0, 50), scale);
            mb1.setFunction("unpause", MenuElement.tInputType.A);
            MenuElement mb2 = new MenuElement("option2", new Vector2(0, -50), scale);
            mb2.setFunction("goToSkillsMenu", MenuElement.tInputType.A);
            MenuElement mb3 = new MenuElement("option3", new Vector2(0, -150), scale);
            mb3.setFunction("exitGame", MenuElement.tInputType.A);
            mb3.Scale = new Vector2(mb3.Scale.X, mb3.Scale.Y*0.8f);

            menu.menuTexts.Add(new MenuText("Paused game", new Vector2(0, 230), 1.2f));
            menu.menuTexts.Add(new MenuText("Continue game", new Vector2(20, 70), 1.0f));
            menu.menuTexts.Add(new MenuText("Skills", new Vector2(15, -25), 1.0f));
            menu.menuTexts.Add(new MenuText("Exit game", new Vector2(5, -130), 1.0f));
            menu.menuTexts.Add(new MenuText("Press ::B to go back", new Vector2(250, -230), 1.0f));

            mb1.upNode = mb3;
            mb1.downNode = mb2;
            mb2.upNode = mb1;
            mb2.downNode = mb3;
            mb3.upNode = mb2;
            mb3.downNode = mb1;
            menu.menuElements.Add(mbHeader);
            menu.menuElements.Add(mb1);
            menu.menuElements.Add(mb2);
            menu.menuElements.Add(mb3);
            menu.setCurrentNode(mb1);
        }

        public override void initialize()
        {
            transitionColor = new Color(104, 0, 4, 255);
            transitionColor *= 0.85f;
            type = StateManager.tGS.Pause;
            initializeMenu();
            renderAlways = false;
            //SoundManager.playSound("pause");
        }

        public override void loadContent()
        {
            loaded = true;
        }

        public override void update()
        {
            base.update();

            menu.update();

            // unpause with start or B button
            if (GamerManager.getMainControls().Start_firstPressed() || GamerManager.getMainControls().B_firstPressed())
            {
                StateManager.dequeueState(1);
                //SoundManager.playSound("pause");
            }
        }

        public override void render()
        {
            menu.render();
        }

        public override void dispose()
        {
            menu.clean();
        }
    }
}
