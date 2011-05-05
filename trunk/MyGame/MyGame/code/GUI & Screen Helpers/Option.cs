using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace MyGame
{
    class Option
    {
        public String text;
        public String description;
        public Vector2 pos;
        public List<Option> options = new List<Option>();
        public tOption type;
        public int selectedOption;
        public float value;
        public Color color;
        public float scale = 1.0f;
        public bool useColor = false;
        public tFunction function = tFunction.None;
        public enum tFunction { None = 0, ChangeMusic = 1, ChangeSound = 2, };
        public Option(String pText, String pDescription, Vector2 pPos, tOption pType)
        {
            text = pText;
            description = pDescription;
            pos = pPos;
            type = pType;
        }
        public Option(String pText, String pDescription, Vector2 pPos, tOption pType, float pValue)
        {
            text = pText;
            description = pDescription;
            pos = pPos;
            type = pType;
            value = pValue;
        }
        public Option(String pText, String pDescription, Vector2 pPos, tOption pType, ref float pValue)
        {
            text = pText;
            description = pDescription;
            pos = pPos;
            type = pType;
            value = pValue;
        }
        public Option(String pText, String pDescription, Vector2 pPos, tOption pType, int pSelectedOption)
        {
            text = pText;
            description = pDescription;
            pos = pPos;
            type = pType;
            selectedOption = pSelectedOption;
        }
        public Option(String pText, String pDescription, Vector2 pPos, tOption pType, Color pColor, float pScale)
        {
            text = pText;
            description = pDescription;
            pos = pPos;
            type = pType;
            color = pColor;
            scale = pScale;
            useColor = true;
        }

        public enum tOption { None, ValueNatural, ValueReal, ToOptions, ToScores, ToCredits, ToMainmenu, ToGame,
            StartGame, SeeGamepadControls, DepauseGame };
        public void executeOption()
        {
            switch (type)
            {
                case tOption.ToOptions:
                    StateManager.enqueueState(StateManager.tGS.Options);
                    break;
                case tOption.ToScores:
                    StateManager.enqueueState(StateManager.tGS.Scores);
                    break;
                case tOption.ToCredits:
                    StateManager.enqueueState(StateManager.tGS.Credits);
                    break;
                case tOption.ToMainmenu:
                    // from options, only need to desenqueue
                    StateManager.dequeueState(1);
                    break;
                case tOption.ToGame:
                    StateManager.gameStates.Clear();
                    StateManager.enqueueState(StateManager.tGS.Game);
                    break;
                case tOption.SeeGamepadControls:
                    //StatePausedGame.showControls = true;
                    break;
                case tOption.DepauseGame:
                    StateManager.dequeueState(1);
                    break;
            }
        }

        public void nextOption()
        {
            if (selectedOption >= options.Count - 1)
                selectedOption = 0;
            else
                selectedOption++;
            SoundManager.playSound("menuChange");
            executeFunction();
        }
        public void lastOption()
        {
            if (selectedOption < 1)
                selectedOption = (int)(options.Count - 1);
            else
                selectedOption--;
            SoundManager.playSound("menuChange");
            executeFunction();
        }
        public void incrementValue()
        {
            value += 1;
            if (value > 100)
                value = 100;
            executeFunction();
        }
        public void decrementValue()
        {
            value -= 1;
            if (value < 0)
                value = 0;
            executeFunction();
        }

        public void executeFunction()
        {
            switch(function)
            {
                case tFunction.None:
                    break;
                case tFunction.ChangeMusic:
                    SoundManager.updateMusicVolume(value);
                    break;
                case tFunction.ChangeSound:
                    SoundManager.updateSoundVolume(value);
                    break;
            }
        }
    }
}
