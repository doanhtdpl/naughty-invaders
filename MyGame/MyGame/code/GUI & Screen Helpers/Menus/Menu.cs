using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.Reflection;

namespace MyGame
{
    class Menu
    {
        public MenuElement selectionCursor { get; set; }
        public MenuElement currentNode { get; set; }
        public void setCurrentNode(MenuElement value)
        {
            currentNode = value;
            selectionCursor.Position = currentNode.Position;
        }
        public List<MenuElement> menuElements { get; set; }
        public List<MenuText> menuTexts { get; set; }

        public Menu()
        {
            menuElements = new List<MenuElement>();
            menuTexts = new List<MenuText>();
            selectionCursor = new MenuElement("A", Vector2.Zero, Vector2.One);
        }

        public void update()
        {
            ControlPad cp = GamerManager.getMainControls();

            // if the button does something in his own update, return to skip other menu updates
            if (currentNode.updateOptions()) return;

            if (cp.Up_firstPressed() && currentNode.upNode != null)
            {
                setCurrentNode(currentNode.upNode);
            }
            else if (cp.Down_firstPressed() && currentNode.downNode != null)
            {
                setCurrentNode(currentNode.downNode);
            }
        }

        public void render()
        {
            GraphicsManager.Instance.spriteBatchBegin();
            for (int i = 0; i < menuElements.Count; ++i)
            {
                menuElements[i].render();
            }
            for (int i = 0; i < menuTexts.Count; ++i)
            {
                menuTexts[i].render();
            }
            selectionCursor.render();
            GraphicsManager.Instance.spriteBatchEnd();
        }

        public void clean()
        {
            menuElements.Clear();
            menuTexts.Clear();
        }
    }
}