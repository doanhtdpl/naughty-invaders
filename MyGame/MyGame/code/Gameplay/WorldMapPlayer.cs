using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyGame
{
    class WorldMapPlayer : AnimatedEntity2D
    {
        public WorldMapPlayer(Vector3 position)
            : base("characters", "player", position, 0.0f, Color.White)
        {
            entityState = tEntityState.Active;
        }

        public void update(ControlPad controls)
        {
            base.update();
        }

        public override void render()
        {
            base.render();
        }
    }
}
