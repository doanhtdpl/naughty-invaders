using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class StateSkillsMenu : GameState
    {
        Menu menu;

        MenuElement getBuySkillOption(string buttonTexture, string skill, Vector2 position, Vector2 scale)
        {
            MenuElement me = new MenuElement(buttonTexture, position, scale);
            me.setFunction("buySkill", MenuElement.tInputType.X, new object[2] { skill, me });
            MenuElement meLinked = new MenuElement("A", position, scale);
            me.linkedElement = meLinked;
            me.drawLinkedElement = GamerManager.getSessionOwner().Player.data.skills[skill].obtained;

            return me;
        }

        void initializeMenu()
        {
            menu = new Menu();
            Vector2 scale = new Vector2(0.8f, 0.8f);
            MenuElement mbHeader = new MenuElement("smallHeader", new Vector2(0, 250), scale);
            MenuElement mb1 = getBuySkillOption("option1", "dash1", new Vector2(-350, 100), scale);
            MenuElement mb2 = getBuySkillOption("option2", "plasma", new Vector2(-350, 30), scale);
            MenuElement mb3 = getBuySkillOption("option3", "powerShot", new Vector2(-350, -25), new Vector2(scale.X, scale.Y * 0.8f));
            MenuElement mb4 = getBuySkillOption("option1", "life1", new Vector2(-350, -110), scale);

            MenuElement wish = new MenuElement("menu", new Vector2(250, 0), scale * 0.8f);

            menu.menuTexts.Add(new MenuText("Skills", new Vector2(0, 280), 1.2f));
            menu.menuTexts.Add(new MenuText("Dash", new Vector2(-370, 120), 0.8f));
            menu.menuTexts.Add(new MenuText("Plasma", new Vector2(-335, 55), 0.8f));
            menu.menuTexts.Add(new MenuText("Power Shot", new Vector2(-345, -5), 0.8f));
            menu.menuTexts.Add(new MenuText("Life", new Vector2(-370, -70), 0.8f));
            menu.menuTexts.Add(new MenuText("Press ::B to go back", new Vector2(250, -230), 1.0f));

            mb1.upNode = mb4;
            mb1.downNode = mb2;
            mb2.upNode = mb1;
            mb2.downNode = mb3;
            mb3.upNode = mb2;
            mb3.downNode = mb4;
            mb4.upNode = mb3;
            mb4.downNode = mb1;
            menu.menuElements.Add(mbHeader);
            menu.menuElements.Add(mb1);
            menu.menuElements.Add(mb2);
            menu.menuElements.Add(mb3);
            menu.menuElements.Add(mb4);
            menu.menuElements.Add(wish);
            menu.setCurrentNode(mb1);
        }

        public override void initialize()
        {
            transitionColor = new Color(100, 50, 200, 200);
            type = StateManager.tGS.SkillsMenu;
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
            if (GamerManager.getMainControls().Start_firstPressed())
            {
                StateManager.dequeueState(2);
            }
            if (GamerManager.getMainControls().B_firstPressed())
            {
                StateManager.dequeueState(1);
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
