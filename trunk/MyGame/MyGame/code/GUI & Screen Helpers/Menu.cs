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
    class MenuButton : RenderableEntity2D
    {
        MethodInfo function;

        public NetworkNode<MenuButton> upNode { set; get; }
        public NetworkNode<MenuButton> downNode { set; get; }

        public MenuButton(string texture, string text, string function, Vector2 position, Vector2 scale):base("GUI/menu", texture, position.toVector3(), 0.0f, Color.White)
        {
            this.delete();
            upNode = null;
            downNode = null;
        }

        public void execute()
        {
        }
    }

    class Menu
    {
        public NetworkNode<MenuButton> currentNode { set; get; }
        public Network<MenuButton> menuButtons { set; get; }

        public Menu()
        {
            menuButtons = new Network<MenuButton>();
        }

        public void update()
        {
            Vector2 stick = GamerManager.getMainControls().LS;

            NetworkNode<MenuButton> nextNode = currentNode.getNext(stick);
            if (nextNode != null)
            {
                currentNode = nextNode;
            }
            else // check for special connections (like the upper button is connected with the lower button if you press up)
            {
                if (stick.Y > 0.5f && currentNode.value.upNode != null)
                {
                    currentNode = currentNode.value.upNode;
                }
                else if (stick.Y < 0.5f && currentNode.value.downNode != null)
                {
                    currentNode = currentNode.value.downNode;
                }
            }

            if (GamerManager.getMainControls().A_firstPressed())
            {
                currentNode.value.execute();
            }
        }
    }
}