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
        public const int N_DIALOG_CHARACTERS = 5;

        bool textComplete;
        public tDialogCharacter character { get; set; }
        public string text { get; set; }
        // text speed at characters per second
        public float textSpeed { get; set; }

        public const float AUDIO_TIME = 0.8f;
        float nextAudio = 0.0f;

        public static string[,] characterAudios; 
        public static Texture2D[] portraits;
        public static Texture2D dialogBackground;
        public static Rectangle backgroundRectangle;
        public static Rectangle portraitRectangle;

        int charactersToShow;

        public DialogEvent(tDialogCharacter character, string text, float activationTime = 0.3f, float textSpeed = 60.0f, bool skippable = true):base(activationTime)
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

            DialogEvent.dialogBackground = TextureManager.Instance.getTexture("GUI/menu/dialogBackground");
            DialogEvent.portraits = new Texture2D[N_DIALOG_CHARACTERS];
            DialogEvent.portraits[(int)tDialogCharacter.Wish] = TextureManager.Instance.getTexture("GUI/portraits/portraitWish");
            DialogEvent.portraits[(int)tDialogCharacter.DarkWish] = TextureManager.Instance.getTexture("GUI/portraits/portraitMacedonia");
            DialogEvent.portraits[(int)tDialogCharacter.OnionElder] = TextureManager.Instance.getTexture("GUI/portraits/portraitOnionElder");
            DialogEvent.portraits[(int)tDialogCharacter.KingTomato] = TextureManager.Instance.getTexture("GUI/portraits/portraitKingTomato");
            DialogEvent.portraits[(int)tDialogCharacter.Macedonia] = TextureManager.Instance.getTexture("GUI/portraits/portraitMacedonia");

            DialogEvent.characterAudios = new string[N_DIALOG_CHARACTERS,3];
            //DialogEvent.characterAudios[(int)tDialogCharacter.Wish, 0] = "wish1";
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
                        textSpeed += 100;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            nextAudio -= SB.dt;
            if (nextAudio < 0.0f)
            {
                //SoundManager.Instance.playEffect(characterAudios[(int)character, Calc.randomNatural(0,2)]);
                nextAudio = AUDIO_TIME;
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
            }

            return keepUpdating;
        }

        public override void render()
        {
            GraphicsManager.Instance.spriteBatchBegin();

            GraphicsManager.Instance.spriteBatch.Draw(dialogBackground, backgroundRectangle, Color.White);
            GraphicsManager.Instance.spriteBatch.Draw(portraits[(int)character], portraitRectangle, Color.White);

            Vector2 position = Screen.getXYfromCenter(new Vector2(-300.0f, -190.0f));
            float scale = 0.86f;
            if (textComplete)
            {
                text.renderNIDialog(position, scale, Color.Green);
            }
            else
            {
                text.Substring(0, charactersToShow).renderNIDialog(position, scale, Color.Green);
            }
            GraphicsManager.Instance.spriteBatchEnd();
        }
    }
}
