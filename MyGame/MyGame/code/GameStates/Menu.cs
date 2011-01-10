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
    class Menu
    {
        public List<Option> options = new List<Option>();
        public Vector2 cursorPosition;
        public int selectedOption;
        public static TEX cursor = new TEX();
        public float menuScale;
        public Color normalColor;
        public Color selectedColor;
        public Color effectColor;
        public Color descriptionColor;
        // bump vector for strings
        Vector2 r = new Vector2(2, 2);

        public const int DESCRIPTION_X = 150;
        public const int DESCRIPTION_Y = 625;
        public const int SUBDESCRIPTION_X = 675;
        public const int SUBDESCRIPTION_Y = 625;

        public Menu(float scale, Color normalColor, Color selectedColor, Color effectColor) : this(scale, normalColor, selectedColor, effectColor, normalColor) { }
        public Menu(float scale, Color normalColor, Color selectedColor, Color effectColor, Color descriptionColor)
        {
            this.menuScale = scale;
            this.normalColor = normalColor;
            this.selectedColor = selectedColor;
            this.effectColor = effectColor;
            this.descriptionColor = descriptionColor;
        }

        public static void loadContent()
        {
            cursor.initTEX("GUI/menu/cursor", 110, 60);
        }

        public void update()
        {
            if (options[selectedOption].type == Option.tOption.ValueNatural)
            {
                if (GamerManager.getMainControls().Left_firstPressed())
                    options[selectedOption].lastOption();
                else if (GamerManager.getMainControls().Right_firstPressed())
                    options[selectedOption].nextOption();
            }
            if (options[selectedOption].type == Option.tOption.ValueReal)
            {
                if (GamerManager.getMainControls().Left_pressed())
                    options[selectedOption].decrementValue();
                else if (GamerManager.getMainControls().Right_pressed())
                    options[selectedOption].incrementValue();
            }
            if (GamerManager.getMainControls().Down_firstPressed())
                nextOption();
            else if (GamerManager.getMainControls().Up_firstPressed())
                lastOption();
            cursorPosition.X = options[selectedOption].pos.X - 130;
            cursorPosition.Y = options[selectedOption].pos.Y;

            if (GamerManager.getMainControls().A_firstPressed())
            {
                options[selectedOption].executeOption();
                SoundManager.playSound("menuSelect");
            }
        }

        public void render()
        {
            Color selectedColorToUse;
            Color normalColorToUse;            
            for (int i = 0; i < options.Count; i++)
            {
                if (options[i].useColor)
                {
                    selectedColorToUse = options[i].color;
                    normalColorToUse = options[i].color;
                }
                else
                {
                    selectedColorToUse = selectedColor;
                    normalColorToUse = normalColor;
                }
                // comun para todas las opciones, si están seleccionadas se pintan más fuerte
                if (i == selectedOption)
                {
                    options[i].text.renderSC(options[i].pos, 1.1f * menuScale * options[i].scale, selectedColorToUse, effectColor, StringManager.tTextAlignment.Left);
                    //GUI.drawString(options[i].text, options[i].pos, selectedColorToUse, effectColor, 1.1f * menuScale * options[i].scale);
                }
                else
                {
                    options[i].text.renderSC(options[i].pos, menuScale * options[i].scale, normalColorToUse, effectColor, StringManager.tTextAlignment.Left);
                    //GUI.drawString(options[i].text, options[i].pos, normalColorToUse, effectColor, 1f * menuScale * options[i].scale);
                }
                if (i == selectedOption)
                {
                    options[i].description.renderSC(new Vector2(DESCRIPTION_X, DESCRIPTION_Y), 0.8f * menuScale * options[i].scale, descriptionColor, effectColor, StringManager.tTextAlignment.Left);
                    //GUI.drawString(options[i].description, new Vector2(DESCRIPTION_X, DESCRIPTION_Y), descriptionColor, effectColor, 0.8f * menuScale * options[i].scale);
                }
                // solo para opciones de varios valores (yes, no/ easy, medium, hard)
                if (options[i].type == Option.tOption.ValueNatural)
                {
                    for (int j = 0; j < options[i].options.Count; j++)
                    {
                        if (j == options[i].selectedOption)
                        {
                            options[i].options[j].text.renderSC(options[i].options[j].pos, menuScale * options[i].scale, selectedColorToUse, effectColor, StringManager.tTextAlignment.Left);
                            //GUI.drawString(options[i].options[j].text, options[i].options[j].pos, selectedColorToUse, effectColor, 1f * menuScale * options[i].scale);
                            if (i == selectedOption)
                            {
                                options[i].options[j].description.renderSC(new Vector2(SUBDESCRIPTION_X, SUBDESCRIPTION_Y), 0.8f * menuScale * options[i].scale, descriptionColor, effectColor, StringManager.tTextAlignment.Left);
                                //GUI.drawString(options[i].options[j].description, new Vector2(SUBDESCRIPTION_X, SUBDESCRIPTION_Y), descriptionColor, effectColor, 0.8f * menuScale * options[i].scale);
                            }
                        }
                        else
                        {
                            options[i].options[j].text.renderSC(options[i].options[j].pos, 0.8f * menuScale * options[i].scale, normalColorToUse, effectColor, StringManager.tTextAlignment.Left);
                            //GUI.drawString(options[i].options[j].text, options[i].options[j].pos, normalColorToUse, effectColor, 0.8f * menuScale * options[i].scale);
                        }
                    }
                }
                // opciones con valores tipo barra (volumen, correcciones visuales)
                else if (options[i].type == Option.tOption.ValueReal)
                {
                    options[i].value.ToString().renderSC(new Vector2(options[i].pos.X + 600, options[i].pos.Y), menuScale * options[i].scale, normalColorToUse, effectColor, StringManager.tTextAlignment.Left);
                    //GUI.drawString(options[i].value.ToString(), new Vector2(options[i].pos.X + 600, options[i].pos.Y), normalColorToUse, effectColor, 1f * menuScale * options[i].scale);
                }
            }
        }

        public void nextOption()
        {
            if (selectedOption >= options.Count - 1)
                selectedOption = 0;
            else
                selectedOption++;
            SoundManager.playSound("menuChange");
        }
        public void lastOption()
        {
            if (selectedOption < 1)
                selectedOption = (int)(options.Count - 1);
            else
                selectedOption--;
            SoundManager.playSound("menuChange");
        }
    }
}
