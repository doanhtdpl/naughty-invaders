using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace MyGame
{
    class MenuFunctions
    {
        public static void startGame()
        {
            StateManager.dequeueState(1);
            StateManager.gameStates.Add(new StateWorldMap());
        }
        public static void goToOptions()
        {
            StateManager.gameStates.Add(new StateOptions());
        }
        public static void unpause()
        {
            StateManager.dequeueState(1);
        }
        public static void goToSkillsMenu()
        {
            StateManager.gameStates.Add(new StateSkillsMenu());
        }
        public static void exitGame()
        {
            StateManager.clearStates();
            StateManager.gameStates.Add(new StateWorldMap());
            //SoundManager.playSound("pause");
        }
        public static void exitToArcade()
        {
            StateManager.clearStates();
            Game.forceExit = true;
        }

        public static void buySkill(string skillName, MenuElement menuElement)
        {
            PlayerSkill ps = GamerManager.getSessionOwner().data.skills[skillName];
            int XP = GamerManager.getSessionOwner().data.XP;
            if (!ps.obtained && ps.cost <= XP)
            {
                ps.obtained = true;
                GamerManager.getSessionOwner().data.XP -= ps.cost;
                menuElement.drawLinkedElement = true;
                // playsound
            }
            else
            {
                // playsound
            }
        }
        public static void buySkillAddLife(string skillName, MenuElement menuElement, Player player)
        {
            PlayerSkill ps = GamerManager.getSessionOwner().data.skills[skillName];
            int XP = GamerManager.getSessionOwner().data.XP;
            if (!ps.obtained && ps.cost <= XP)
            {
                ps.obtained = true;
                GamerManager.getSessionOwner().data.XP -= ps.cost;
                menuElement.drawLinkedElement = true;
                player.addLifeToMax();
                // playsound
            }
            else
            {
                // playsound
            }
        }
    }
}