using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Globalization;

namespace MyGame
{
    class EditorState
    {
        protected Entity2D selectedEntity = null;

        public virtual void update() { }
        public virtual void render() { }

        public virtual void propertiesChanged()
        {
            if (selectedEntity != null)
            {
                selectedEntity.position = new Vector3(MyEditor.Instance.textPosX.Text.toFloat(),
                    MyEditor.Instance.textPosY.Text.toFloat(),
                    MyEditor.Instance.textPosZ.Text.toFloat());

                selectedEntity.orientation = MyEditor.Instance.textRotZ.Text.toFloat() / (float)(360 / (Math.PI * 2));
                selectedEntity.scale2D = new Vector2(MyEditor.Instance.textScaleX.Text.toFloat(),
                    MyEditor.Instance.textScaleY.Text.toFloat());
            }
        }

        public void updateEntityProperties()
        {
            if (selectedEntity != null)
            {
                MyEditor.Instance.textPosX.Text = selectedEntity.position.X.ToString();
                MyEditor.Instance.textPosY.Text = selectedEntity.position.Y.ToString();
                MyEditor.Instance.textPosZ.Text = selectedEntity.position.Z.ToString();
                //MyEditor.Instance.textRotX.Text = entity.orientation.X.ToString();
                //MyEditor.Instance.textRotY.Text = entity.orientation.Y.ToString();
                MyEditor.Instance.textRotZ.Text = (selectedEntity.orientation * (float)(360 / (Math.PI * 2))).ToString();
                MyEditor.Instance.textScaleX.Text = selectedEntity.scale2D.X.ToString();
                MyEditor.Instance.textScaleY.Text = selectedEntity.scale2D.Y.ToString();
            }
        }
    }
}
