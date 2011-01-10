using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    class StateOptions : GameState
    {
        //public static TEX mainMenu = new TEX();
        Menu menu = new Menu(1.0f, Color.DarkGreen, Color.Green, Color.Black);

        public const int OPTIONS_X = -360;

        public override void initialize()
        {
            type = StateManager.tGS.Options;
            //menu.options.Add(new Option(TextKey.OptMusicVol.Translate(), TextKey.OptMusicVolDesc.Translate(), Screen.getXYfromCenter(OPTIONS_X, 200), Option.VALUE_REAL, GamerManager.getMainControls().saveData.musicLevel));
            //menu.options.Add(new Option(TextKey.OptSoundVol.Translate(), TextKey.OptSoundVolDesc.Translate(), Screen.getXYfromCenter(OPTIONS_X, 100), Option.VALUE_REAL, GamerManager.getMainControls().saveData.soundLevel));
            //menu.options[0].function = Option.tFunction.ChangeMusic;
            //menu.options[1].function = Option.tFunction.ChangeSound;
            //Option rumble = new Option(TextKey.OptRumble.Translate(), TextKey.OptRumbleDesc.Translate(), Screen.getXYfromCenter(OPTIONS_X, 0), Option.VALUE_NATURAL, GamerManager.getMainControls().saveData.rumble);
            //// queremos que YES sea la opción correspondiente al 1
            //rumble.options.Add(new Option(TextKey.OptNo.Translate(), TextKey.OptNoDesc.Translate(), Screen.getXYfromCenter(300, 0), Option.NONE));
            //rumble.options.Add(new Option(TextKey.OptYes.Translate(), TextKey.OptYesDesc.Translate(), Screen.getXYfromCenter(200, 0), Option.NONE));
            //menu.options.Add(rumble);
            //menu.options.Add(new Option(TextKey.OptBack.Translate(), TextKey.OptBackDesc.Translate(), Screen.getXYfromCenter(OPTIONS_X, -100), Option.TO_MAINMENU));
        }

        public override void loadContent()
        {
            Menu.loadContent();
            loaded = true;
        }

        public override void update()
        {
            base.update();
            menu.update();
            // hardcode muy específico para que haga rumble en caso de estar encima de la opción Rumble con Yes
            if (menu.selectedOption == 2 && menu.options[menu.selectedOption].selectedOption == 1)
            {
                GamerManager.getMainControls().rumble(100, 0.3f, 0.3f );
            }
        }

        public override void render()
        {
            SB.spriteBatch.Begin();
            menu.render();
            SB.spriteBatch.End();
        }

        public override void dispose()
        {
            //SaveGameManager.saveGame();
        }
    }
}