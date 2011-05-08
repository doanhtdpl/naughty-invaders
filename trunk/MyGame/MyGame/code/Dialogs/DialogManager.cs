using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class DialogEvent
    {
        bool skippable;
        float duration;
        string text;
        float textSpeed;
        // text style
        StringManager.tStyle textStyle;
        StringManager.tTextAlignment textAlignment;

        DialogManager.tDialogCharacter character;
 
        public void render()
        {
            
        }
    }

    class Dialog
    {
        public List<DialogEvent> events = new List<DialogEvent>();
        
    }

    class DialogManager
    {
        static DialogManager instance = null;
        DialogManager()
        {
        }
        public static DialogManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DialogManager();
                }
                return instance;
            }
        }

        List<Dialog> dialogs = new List<Dialog>();

        public enum tDialogCharacter { Wish, OnionElder, KingTomato }
        public string getCharacterName(tDialogCharacter character)
        {
            switch (character)
            {
                case tDialogCharacter.Wish: return "Wish";
                case tDialogCharacter.OnionElder: return "Onion Elder";
                case tDialogCharacter.KingTomato: return "King Tomato";
            }
            return "";
        }

        public void update()
        {

        }
        public void render()
        {

        }
    }
}
