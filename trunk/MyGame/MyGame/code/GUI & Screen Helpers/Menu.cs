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
    public class MenuElement : RenderableEntity2D
    {
        public enum tInputType { A = 0, Up, Down, Right, Left }
        const int INPUTS = 5;
        MethodInfo[] functions;

        public string text { get; set; }

        public MenuElement upNode { set; get; }
        public MenuElement downNode { set; get; }

        // used to backup at the render method
        static Vector3 originalPosition;

        public MenuElement(string texture, string text, Vector2 position):base("GUI/menu", texture, position.toVector3(), 0.0f, Color.White, false)
        {
            this.delete();
            upNode = null;
            downNode = null;

            this.text = text;

            functions = new MethodInfo[INPUTS];
            for (int i = 0; i < INPUTS; ++i)
            {
                functions[i] = null;
            }
        }
        
        public void setFunction(string functionName, tInputType functionType)
        {
            Type type = Type.GetType("MyGame.MenuFunctions");
            functions[functionType.toInt()] = type.GetMethod(functionName);
        }

        public bool updateOptions()
        {
            ControlPad cp = GamerManager.getMainControls();
            if (cp.A_firstPressed() && (functions[tInputType.A.toInt()] != null))
            {
                functions[tInputType.A.toInt()].Invoke(null, null);
                return true;
            }
            if (cp.Left_firstPressed() && (functions[tInputType.Left.toInt()] != null))
            {
                functions[tInputType.Left.toInt()].Invoke(null, null);
                return true;
            }
            if (cp.Right_firstPressed() && (functions[tInputType.Right.toInt()] != null))
            {
                functions[tInputType.Right.toInt()].Invoke(null, null);
                return true;
            }
            if (cp.Up_firstPressed() && (functions[tInputType.Up.toInt()] != null))
            {
                functions[tInputType.Up.toInt()].Invoke(null, null);
                return true;
            }
            if (cp.Down_firstPressed() && (functions[tInputType.Down.toInt()] != null))
            {
                functions[tInputType.Down.toInt()].Invoke(null, null);
                return true;
            }

            return false;
        }

        public override void render()
        {
            originalPosition = position;
            position2D += CameraManager.Instance.getCameraPositionXY();
            base.render();
            position = originalPosition;
        }
    }

    class Menu
    {
        public MenuElement selectionCursor { get; set; }
        public MenuElement currentNode { get; set; }
        public void setCurrentNode(MenuElement value)
        {
            currentNode = value;
            selectionCursor.position2D = currentNode.position2D;
        }
        public List<MenuElement> menuElements { get; set; }

        public Menu()
        {
            menuElements = new List<MenuElement>();
            selectionCursor = new MenuElement("A", "", Vector2.Zero);
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
            for (int i = 0; i < menuElements.Count; ++i)
            {
                menuElements[i].render();
            }
            selectionCursor.render();
        }
    }
}