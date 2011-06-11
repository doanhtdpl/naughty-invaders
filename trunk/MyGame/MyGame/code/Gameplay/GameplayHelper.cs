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

        // returns true if arrives to its destiny
        public bool fromToAtSpeed(ref Vector3 position, Vector3 to, float speed)
        {
            Vector3 direction = Vector3.Normalize(to - position);
            position += direction * speed * SB.dt;
            // see if the new position passed over the destiny
            if ((direction + Vector3.Normalize(to - position)).LengthSquared() < 0.5f )
            {
                position = to;
                return true;
            }
            return false;
        }
        // updates the position of an entity within the level lines passed as parameter and in the playable zone
        public void updateEntityPosition(Entity2D entity, Vector2 newPosition, List<Line> levelLines, bool keepInPlayableZone = false, bool arcadeLimitation = false)
        {
            float distanceToLine = 0.0f;

            // set the new position
            entity.position2D = newPosition;

            // update the position controlling the collisions with all the scene and camera lines
            for (int i = 0; i < levelLines.Count; ++i)
            {
                Vector2 v = levelLines[i].vectorToPoint(newPosition);
                distanceToLine = Vector2.Distance(newPosition, v) - entity.getRadius();
                if (distanceToLine < 0)
                {
                    entity.position2D -= Vector2.Normalize(entity.position2D - v) * distanceToLine;
                }
            }
            if (keepInPlayableZone)
            {
                Line[] cameraLines = Camera2D.playableZoneCollisions;
                if (arcadeLimitation)
                {
                    float newTop = Camera2D.position.Y + (Camera2D.screen.Height * 0.2f);
                    cameraLines[0].p1.Y = newTop;
                    cameraLines[0].p2.Y = newTop;
                }

                for (int i = 0; i < 4; ++i)
                {
                    Vector2 v = cameraLines[i].vectorToPoint(newPosition);
                    distanceToLine = Vector2.Distance(newPosition, v) - entity.getRadius();
                    if (distanceToLine < 0)
                    {
                        entity.position2D -= Vector2.Normalize(entity.position2D - v) * distanceToLine;
                    }
                }
            }
        }
    }
}
