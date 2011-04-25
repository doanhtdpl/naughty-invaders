using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;

namespace MyGame
{
    class ConditionFunctions
    {
        public static bool isPlayersLastLife()
        {
            return GamerManager.getSessionOwner().Player.lifes == 1;
        }
    }
}
