#if EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class EditorState_AddCameraNode : EditorState
    {
        public EditorState_AddCameraNode()
            : base()
        {
        }

        public override void enter()
        {
            base.enter();
            MyEditor.Instance.myEditorControl.Focus();
        }

        public override void update()
        {
            base.update();

            if (justPressedLeftButton() && isPosInScreen(gameScreenPos))
            {
                Vector3 position = new Vector3(mouseInSetaZero.X, mouseInSetaZero.Y, 800);
                Vector3 target = new Vector3(mouseInSetaZero.X, mouseInSetaZero.Y, 0);
                CameraManager.Instance.addNode(new NetworkNode<CameraData>(new CameraData(target),position));

                MyEditor.Instance.changeState(new EditorState_AddCameraNode());
            }
        }

        public override void exit()
        {
            base.exit();
        }

        public override void render()
        {
            Vector2 position = new Vector2(mouseInSetaZero.X, mouseInSetaZero.Y);
            DebugManager.Instance.addRectangle(position - new Vector2(50, 50), position + new Vector2(50, 50), Color.Yellow, 1.0f);

            foreach (NetworkNode<CameraData> node in CameraManager.Instance.getNodes().getNodes())
            {
                DebugManager.Instance.addRectangle(node.value.target - new Vector3(50, 50, 0), node.value.target + new Vector3(50, 50, 0), Color.Blue, 1.0f);
            }
        }
    }
}
#endif