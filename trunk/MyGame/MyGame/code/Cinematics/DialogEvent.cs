using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace MyGame
{
    public enum tDialogCharacter { Wish = 0, DarkWish, OnionElder, KingTomato, Macedonia }
    class DialogEvent : CinematicEvent
    {
        public const int N_DIALOGS = 5;

        bool textComplete;
        public tDialogCharacter character { get; set; }
        public string text { get; set; }
        // text speed at characters per second
        public float textSpeed { get; set; }

        float nextAudio = 0.0f;
        int lastDialogPlayed = -1;

        public static string[,] characterAudios;
        public static Texture2D[] portraits;
        public static float[] dialogTimes;
        public static Texture2D dialogBackground;
        public static Rectangle backgroundRectangle;
        public static Rectangle portraitRectangle;

        int charactersToShow;

        public DialogEvent(tDialogCharacter character, string text, float activationTime = 0.3f, float textSpeed = 30.0f, bool skippable = true):base(activationTime)
        {
            this.character = character;
            this.text = text;
            this.textSpeed = textSpeed;

            this.textComplete = false;
            this.charactersToShow = 0;

            this.skippable = skippable;
        }

        public static void initialize()
        {
            Vector2 pos = Screen.getXYfromCenter(-400, -120);
            backgroundRectangle = new Rectangle((int)pos.X, (int)pos.Y, 1000, 200);
            pos = Screen.getXYfromCenter(-520, -125);
            portraitRectangle = new Rectangle((int)pos.X, (int)pos.Y, 150, 150);

            string[] characterNames = Enum.GetNames(Type.GetType("MyGame.tDialogCharacter"));
            int numberOfCharacters = characterNames.Length;

            DialogEvent.dialogBackground = TextureManager.Instance.getTexture("GUI/menu/dialogBackground");
            DialogEvent.portraits = new Texture2D[numberOfCharacters];
            DialogEvent.portraits[(int)tDialogCharacter.Wish] = TextureManager.Instance.getTexture("GUI/portraits/portraitWish");
            DialogEvent.portraits[(int)tDialogCharacter.DarkWish] = TextureManager.Instance.getTexture("GUI/portraits/mrblack-89");
            DialogEvent.portraits[(int)tDialogCharacter.OnionElder] = TextureManager.Instance.getTexture("GUI/portraits/portraitOnionElder");
            DialogEvent.portraits[(int)tDialogCharacter.KingTomato] = TextureManager.Instance.getTexture("GUI/portraits/portraitKingTomato");
            DialogEvent.portraits[(int)tDialogCharacter.Macedonia] = TextureManager.Instance.getTexture("GUI/portraits/portraitMacedonia");

            DialogEvent.characterAudios = new string[numberOfCharacters,N_DIALOGS];
            for (int i = 0; i < characterNames.Length; ++i)
            {
                for (int j = 0; j < N_DIALOGS; ++j)
                {
                    characterAudios[i,j] = "dialog" + characterNames[i] + (j+1).ToString();
                }
            }
            dialogTimes = new float[numberOfCharacters];
            dialogTimes[(int)tDialogCharacter.Wish] = 0.8f;
            dialogTimes[(int)tDialogCharacter.DarkWish] = 0.7f;
            dialogTimes[(int)tDialogCharacter.OnionElder] = 1.0f;
            dialogTimes[(int)tDialogCharacter.KingTomato] = 0.5f;
            dialogTimes[(int)tDialogCharacter.Macedonia] = 0.5f;
        }

        public override bool update(bool skip, bool forceSkip = false)
        {
            bool keepUpdating = true;

            if (skippable || forceSkip)
            {
                if (skip)
                {
                    if (!textComplete)
                    {
                        textSpeed += 30;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            
            timer += SB.dt;

            if (!textComplete)
            {
                float timeBuildingText = text.Length / textSpeed;
                charactersToShow = (int)(timer * textSpeed);
                if (charactersToShow > text.Length)
                {
                    charactersToShow = text.Length;
                    textComplete = true;
                }

                nextAudio -= SB.dt;
                if (nextAudio < 0.0f)
                {
                    int dialogToPlay;
                    do
                    {
                        dialogToPlay = Calc.randomNatural(0, N_DIALOGS-1);
                    }
                    while (dialogToPlay == lastDialogPlayed);

                    nextAudio = dialogTimes[(int)character];
                    SoundManager.Instance.playEffect(characterAudios[(int)character, dialogToPlay]);
                }
            }

            return keepUpdating;
        }

        public override void render()
        {
            GraphicsManager.Instance.spriteBatchBegin();

            GraphicsManager.Instance.spriteBatch.Draw(dialogBackground, backgroundRectangle, Color.White);
            GraphicsManager.Instance.spriteBatch.Draw(portraits[(int)character], portraitRectangle, Color.White);

            Vector2 position = Screen.getXYfromCenter(new Vector2(-300.0f, -185.0f));
            float scale = 0.86f;
            if (textComplete)
            {
                text.renderNIDialog(position, scale, Color.Green);
            }
            else
            {
                text.Substring(0, charactersToShow).renderNIDialog(position, scale, Color.Green);
            }

            if((SB.gameTime.TotalGameTime.TotalSeconds * 2) % 2 < 1)
                "::A".renderNI(Screen.getXYfromCenter(new Vector2(525, -225)), 1);
            else
                "::A".renderNI(Screen.getXYfromCenter(new Vector2(527, -223)), 1.2f);

            GraphicsManager.Instance.spriteBatchEnd();
        }
    }
}
