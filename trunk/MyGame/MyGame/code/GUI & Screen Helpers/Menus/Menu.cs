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
    class MenuCursor
    {
        MenuElement leftCursor;
        MenuElement rightCursor;

        public MenuCursor(string textureName, Vector2 scale, bool drawLinkedElement = false)
        {
            leftCursor = new MenuElement(textureName, Vector2.Zero, scale);
            rightCursor = new MenuElement(textureName, Vector2.Zero, scale);
        }

        public void updatePositionAndScale(Vector2 position, Vector2 scale)
        {
            leftCursor.position = position + new Vector2(-240, 0);
            rightCursor.position = position + new Vector2(260, 0);

        }

        public void render()
        {
            leftCursor.render(false);
            rightCursor.render(false, true);
        }
    }

    class Menu
    {
        public MenuCursor selectionCursor { get; set; }
        public MenuElement currentNode { get; set; }
        public void setCurrentNode(MenuElement value)
        {
            currentNode = value;
            selectionCursor.updatePositionAndScale( currentNode.position, currentNode.scale );
        }
        public List<MenuElement> menuElements { get; set; }
        public List<MenuText> menuTexts { get; set; }

        public Menu()
        {
            menuElements = new List<MenuElement>();
            menuTexts = new List<MenuText>();
            selectionCursor = new MenuCursor("Selector", new Vector2(0.4f, 0.4f));
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
                menuElements[i].render(menuElements[i] == currentNode);
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