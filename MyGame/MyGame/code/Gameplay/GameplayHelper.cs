using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class GameplayHelper
    {
        GameplayHelper()
        {
        }

        static GameplayHelper instance = null;
        public static GameplayHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameplayHelper();
                }
                return instance;
            }
        }

        /*
        #region WALL COLLISION
        bool collided = false;
        for (int i = 0; i < StageManager.walls.Count; i++)
        {
            Vector2 v = StageManager.walls[i].vectorToPoint(position + speed);
            if (Vector2.Distance(position + speed, v) < radius + StageManager.WALL_SIZE)
            {
                collided = true;
                position -= Vector2.Normalize(v - position);
            }
        }
        if (!collided)
            position += speed;
        #endregion
        */

        public void updateEntityPosition(Entity2D entity, Vector2 newPosition, List<Line> levelLines, Line[] cameraLines = null)
        {
            bool collided = false;
            for (int i = 0; i < levelLines.Count; ++i)
            {
                Vector2 v = levelLines[i].vectorToPoint(newPosition);
                if (Vector2.Distance(newPosition, v) < entity.getRadius())
                {
                    collided = true;
                    entity.position2D -= Vector2.Normalize(v - entity.position2D);
                }
            }
            for (int i = 0; i < 4; ++i)
            {
                Vector2 v = cameraLines[i].vectorToPoint(newPosition);
                if (Vector2.Distance(newPosition, v) < entity.getRadius())
                {
                    collided = true;
                    entity.position2D -= Vector2.Normalize(v - entity.position2D);
                }
            }
            if (!collided)
                entity.position2D = newPosition;
        }
    }
}
