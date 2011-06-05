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

        MenuElement getBuySkillOption(string buttonTexture, string skill, string skillDescription, Vector2 position, Vector2 scale)
        {
            MenuElement me = new MenuElement(buttonTexture, position, scale);
            me.setFunction("buySkill", MenuElement.tInputType.X, new object[2] { skill, me});
            MenuElement meLinked = new MenuElement("botoncito", position + new Vector2(120, 0), scale);
            me.linkedElement = meLinked;
            me.drawLinkedElement = GamerManager.getSessionOwner().data.skills[skill].obtained;

            me.description = skillDescription;
            me.DescriptionPosition = new Vector2(140, 140);

            return me;
        }

        void initializeMenu()
        {
            menu = new Menu(250.0f);
            Vector2 scale = new Vector2(0.7f, 0.7f);

            MenuElement wish = new MenuElement("wishmenu", new Vector2(320, -50), scale * 0.8f);
            //wish.color = new Color(0.6f, 0.6f, 0.6f, 0.6f);

            MenuElement mbHeader = new MenuElement("smallHeader", new Vector2(0, 250), scale);
            MenuElement mb1 = getBuySkillOption("option1", "dash1",
                "Press ::A to make a quick dash",
                new Vector2(-350, 140), scale);
            MenuElement mb2 = getBuySkillOption("option2", "plasma",
                "Improve your main attack with plasma power",
                new Vector2(-350, 70), scale);
            MenuElement mb3 = getBuySkillOption("option3", "powerShot",
                "Press ::Y to shot a secondary powerful attack. Keep ::Y pressed to charge the attack", 
                new Vector2(-350, 15), new Vector2(scale.X, scale.Y * 0.8f));
            MenuElement mb4 = getBuySkillOption("option1", "life1",
                "Get an additional permanent life portion to your life bar",
                new Vector2(-350, -70), scale);
            mb4.setFunction("buySkillAddLife", MenuElement.tInputType.X, new object[3] { "life1", mb4, GamerManager.getMainPlayer() });
            MenuElement mbDescriptionHeader = new MenuElement("largeHeader", new Vector2(150, 80), new Vector2(1.3f, 0.4f));

            menu.menuTexts.Add(new MenuText("Skills", new Vector2(0, 280), 1.2f));
            menu.menuTexts.Add(new MenuText("Dash", new Vector2(-370, 160), 0.8f));
            menu.menuTexts.Add(new MenuText("Plasma", new Vector2(-335, 95), 0.8f));
            menu.menuTexts.Add(new MenuText("Power Shot", new Vector2(-345, 35), 0.8f));
            menu.menuTexts.Add(new MenuText("Life", new Vector2(-370, -30), 0.8f));
            menu.menuTexts.Add(new MenuText("Press ::B to go back", new Vector2(250, -230), 1.0f));

            mb1.upNode = mb4;
            mb1.downNode = mb2;
            mb2.upNode = mb1;
            mb2.downNode = mb3;
            mb3.upNode = mb2;
            mb3.downNode = mb4;
            mb4.upNode = mb3;
            mb4.downNode = mb1;
            menu.menuElements.Add(wish);
            menu.menuElements.Add(mbHeader);
            menu.menuElements.Add(mbDescriptionHeader);
            menu.menuElements.Add(mb1);
            menu.menuElements.Add(mb2);
            menu.menuElements.Add(mb3);
            menu.menuElements.Add(mb4);
            menu.setCurrentNode(mb1);

            // xp counter
            XPCounter xpCounter = new XPCounter(GamerManager.getSessionOwner(), "starXP", new Vector2(-470, -250), new Vector2(1.0f, 1.0f));
            menu.menuElements.Add(xpCounter);
        }

        public override void initialize()
        {
            transitionColor = new Color(10, 10, 10, 255);
            transitionColor *= 0.85f;
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
