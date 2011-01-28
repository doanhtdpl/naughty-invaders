using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class DefaultState : EditorState
    {
        public override void leftClick(Vector2 _vPos)
        {
            Ray ray = EditorHelper.Instance.getMouseCursorRay(_vPos);
            selectedEntity = EditorHelper.Instance.rayVsEntities(ray, LevelManager.Instance.getStaticProps());

            if (selectedEntity != null)
            {
                updateEntityProperties();
            }
        }

        public override void update()
        {
        }

        public override void render()
        {
            if (selectedEntity != null)
            {
                EditorHelper.Instance.renderEntityQuad(selectedEntity);
            }
        }
    }
}
