﻿#if EDITOR
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
    class EditorState_MoveCameraNode : EditorState
    {
        NetworkNode<CameraData> cameraNode = null;

        public override void enter()
        {
            base.enter();
            MyEditor.Instance.staticPropertiesPanel.Hide();
            MyEditor.Instance.cameraNodePanel.Show();
            MyEditor.Instance.cameraNodePanel.Bounds = MyEditor.Instance.staticPropertiesPanel.Bounds;
        }

        public override void exit()
        {
            base.exit();
            MyEditor.Instance.staticPropertiesPanel.Show();
            MyEditor.Instance.cameraNodePanel.Hide();
        }

        public override void update()
        {
            base.update();

            if (selectCameraNode() && isPosInScreen(gameScreenPos))
            {
                if (keyState.IsKeyDown(Keys.Delete) && lastKeyState.IsKeyUp(Keys.Delete))
                {
                    CameraManager.Instance.getNodes().getNodes().Remove(cameraNode);
                    cameraNode = null;
                    return;
                }

                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Vector2 current = new Vector2(mouseState.X, mouseState.Y);
                    Vector2 last = new Vector2(lastMouseState.X, lastMouseState.Y);

                    Vector3 currentZ = EditorHelper.Instance.getMousePosInZ(current, 0);
                    Vector3 lastZ = EditorHelper.Instance.getMousePosInZ(last, 0);

                    cameraNode.value.position += (currentZ - lastZ);
                    cameraNode.value.target += (currentZ - lastZ);
                }
            }

            if (mouseState.LeftButton == ButtonState.Pressed || justReleasedLeftButton())
            {
                cameraToFields();
            }
        }

        public void cameraToFields()
        {
            if (cameraNode != null)
            {
                MyEditor.Instance.cameraPosX.Text = cameraNode.value.position.X.toString();
                MyEditor.Instance.cameraPosY.Text = cameraNode.value.position.Y.toString();
                MyEditor.Instance.cameraPosZ.Text = cameraNode.value.position.Z.toString();
                MyEditor.Instance.cameraNodeSpeed.Text = cameraNode.value.speed.toString();
                MyEditor.Instance.isFirstCheck.Checked = cameraNode.value.isFirst;
            }
            else
            {
                MyEditor.Instance.cameraPosX.Text = "";
                MyEditor.Instance.cameraPosY.Text = "";
                MyEditor.Instance.cameraPosZ.Text = "";
                MyEditor.Instance.cameraNodeSpeed.Text = "";
                MyEditor.Instance.isFirstCheck.Checked = false;
            }
        }

        public void fieldsToCamera()
        {
            cameraNode.value.position = new Vector3(MyEditor.Instance.cameraPosX.Text.toFloat(),
                MyEditor.Instance.cameraPosY.Text.toFloat(),
                MyEditor.Instance.cameraPosZ.Text.toFloat());
            cameraNode.value.target = cameraNode.value.position;
            cameraNode.value.target.Z = 0;
            cameraNode.value.speed = MyEditor.Instance.cameraNodeSpeed.Text.toFloat();
            cameraNode.value.isFirst = MyEditor.Instance.isFirstCheck.Checked;
        }

        public bool selectCameraNode()
        {
            if (!isPosInScreen(gameScreenPos))
            {
                return cameraNode != null;
            }

            if (justPressedLeftButton())
            {
                canSelect = true;
            }

            if (mouseState.LeftButton == ButtonState.Pressed && (Math.Abs(lastMouseState.X - mouseState.X) > 2 || Math.Abs(lastMouseState.Y - mouseState.Y) > 2))
            {
                canSelect = false;
            }

            if (justReleasedLeftButton() && canSelect)
            {
                cameraNode = null;
                foreach(NetworkNode<CameraData> node in CameraManager.Instance.getNodes().getNodes())
                {
                    Vector3 pos = node.value.target;
                    pos.Z = 0;
                    if ((node.value.target - mouseInSetaZero).Length() < 70.0f)
                    {
                        cameraNode = node;
                    }
                }
            }

            return cameraNode != null;
        }

        public override void render()
        {
            if (cameraNode != null)
            {
                Vector3 position = cameraNode.value.target;
                DebugManager.Instance.addLine(position - new Vector3(50, 50, 0), position + new Vector3(50, 50, 0), Color.Green);
                DebugManager.Instance.addRectangle(position - new Vector3(50, 50, 0), position + new Vector3(50, 50, 0), Color.Green);
            }
        }
    }
}
#endif