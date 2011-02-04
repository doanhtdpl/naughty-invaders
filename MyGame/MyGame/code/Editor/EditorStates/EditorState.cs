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
                selectedEntity.position = new Vector3(float.Parse(MyEditor.Instance.textPosX.Text, CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(MyEditor.Instance.textPosY.Text, CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(MyEditor.Instance.textPosZ.Text, CultureInfo.InvariantCulture.NumberFormat));

                ////MyEditor.Instance.textRotX.Text = entity.orientation.X.ToString();
                ////MyEditor.Instance.textRotY.Text = entity.orientation.Y.ToString();
                selectedEntity.orientation = float.Parse(MyEditor.Instance.textRotZ.Text, CultureInfo.InvariantCulture.NumberFormat) / (float)(360 / (Math.PI * 2));
                selectedEntity.scale2D = new Vector2(float.Parse(MyEditor.Instance.textScaleX.Text, CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(MyEditor.Instance.textScaleY.Text, CultureInfo.InvariantCulture.NumberFormat));
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
