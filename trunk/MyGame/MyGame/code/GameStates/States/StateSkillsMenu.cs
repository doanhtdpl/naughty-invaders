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
        Texture2D bg;

        MenuElement getBuySkillOption(string buttonTexture, string skill, string skillDescription, Vector2 position, Vector2 scale)
        {
            MenuElement me = new MenuElement(buttonTexture, position, scale);
            me.setFunction("buySkill", MenuElement.tInputType.X, new object[2] { skill, me});
            MenuElement meLinked = new MenuElement("botoncito", position + new Vector2(120, 0), scale);
            me.linkedElement = meLinked;
            me.drawLinkedElement = GamerManager.getSessionOwner().data.skills[skill].obtained;

            me.description = skillDescription;
            me.DescriptionPosition = new Vector2(-35, 140);

            return me;
        }

        void initializeMenu()
        {
            menu = new Menu(250.0f);
            Vector2 scale = new Vector2(0.7f, 0.7f);

            MenuElement wish = new MenuElement("wishmenu", new Vector2(320, -50), scale * 0.8f);
            //wish.color = new Color(0.6f, 0.6f, 0.6f, 0.6f);

            MenuElement mbHeader = new MenuElement("smallHeader", new Vector2(0, 250), scale);
            MenuElement mbStar = new MenuElement("starXP", new Vector2(40, 125), scale * 0.8f);

            MenuElement mb1 = getBuySkillOption("option1", "dash1",
                "150      Press ::A to make a quick dash.",
                new Vector2(-350, 140), scale);
            MenuElement mb2 = getBuySkillOption("option2", "plasma",
                "300      Improve your main attack with plasma power.",
                new Vector2(-350, 70), scale);
            MenuElement mb3 = getBuySkillOption("option3", "powerShot",
                "250      Keep ::Y pressed to charge the attack. Release ::Y to shot a powerful attack.", 
                new Vector2(-350, 15), new Vector2(scale.X, scale.Y * 0.8f));
            MenuElement mb4 = getBuySkillOption("option1", "life1",
                "200      Get an additional permanent life portion in your life bar.",
                new Vector2(-350, -55), scale);
            mb4.setFunction("buySkillAddLife", MenuElement.tInputType.X, new object[3] { "life1", mb4, GamerManager.getMainPlayer() });
            MenuElement mbDescriptionHeader = new MenuElement("largeHeader", new Vector2(220, 80), new Vector2(1.3f, 0.4f));

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
            menu.menuElements.Add(mbStar);
            menu.menuElements.Add(mb1);
            menu.menuElements.Add(mb2);
            menu.menuElements.Add(mb3);
            menu.menuElements.Add(mb4);
            menu.setCurrentNode(mb1);

            // xp counter
            XPCounter xpCounter = new XPCounter(GamerManager.getSessionOwner(), "starXP", new Vector2(-470, -250), new Vector2(1.0f, 1.0f));
            menu.menuElements.Add(xpCounter);

            bg = TextureManager.Instance.getTexture("GUI/menu", "pausescreen-35");
        }

        public override void initialize()
        {
            transitionColor = new Color(0, 0, 0, 200);
            //transitionColor *= 0.85f;
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

            if (GamerManager.getMainControls().B_firstPressed() || GamerManager.getMainControls().Start_firstPressed() || GamerManager.getMainControls().Back_firstPressed())
            {
                StateManager.dequeueStates(1);
            }
        }

        public override void render()
        {
            GraphicsManager.Instance.spriteBatchBegin();
            Color color = Color.White;
            if (!(StateManager.gameStates[StateManager.gameStates.Count - 2] is StatePausedGame))
            {
                color.A = (byte)(255 * Math.Min(1, (timeRunning / 0.5f)));
            }
            QuadRenderer.render2D(bg, new Vector2(0, 0), new Vector2(1285, 740), color);
            GraphicsManager.Instance.spriteBatchEnd();

            menu.render();

            GraphicsManager.Instance.spriteBatchBegin();
            TextKey.skills.Translate().renderNI(Screen.getXYfromCenter(10, 290), 1.5f, StringManager.tStyle.Border);
            TextKey.skillDash.Translate().renderNI(Screen.getXYfromCenter(-350, 160), 0.8f, StringManager.tStyle.Shadowed);
            TextKey.skillPlasma.Translate().renderNI(Screen.getXYfromCenter(-350, 95), 0.8f, StringManager.tStyle.Shadowed);
            TextKey.skillPowerShot.Translate().renderNI(Screen.getXYfromCenter(-350, 30), 0.8f, StringManager.tStyle.Shadowed);
            TextKey.skillLife.Translate().renderNI(Screen.getXYfromCenter(-350, -32), 0.8f, StringManager.tStyle.Shadowed);
            TextKey.pressBBack.Translate().renderNI(Screen.getXYfromCenter(300, -230), 1.0f, StringManager.tStyle.Shadowed);
            GraphicsManager.Instance.spriteBatchEnd();
        }

        public override void dispose()
        {
            menu.clean();
        }
    }
}
