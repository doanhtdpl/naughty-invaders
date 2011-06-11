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
            TransitionManager.Instance.changeStateWithFade(StateManager.tGameState.WorldMap, 1, null, 0.5f, Color.Black);
        }
        public static void goToOptions()
        {
            TransitionManager.Instance.changeStateWithFade(StateManager.tGameState.Credits, 0, null, 0.5f, Color.Black);
        }
        public static void goToCredits()
        {
            StateManager.addState(StateManager.tGameState.Credits);
        }
        public static void unpause()
        {
            StateManager.dequeueStates(1);
        }
        public static void goToSkillsMenu()
        {
            StateManager.addState(StateManager.tGameState.SkillsMenu);
        }
        public static void exitGame()
        {
            StateManager.clearStates();
            StateManager.addState(StateManager.tGameState.WorldMap);
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
                player.addLifePortionsToMax();
                // playsound
            }
            else
            {
                // playsound
            }
        }
    }
}