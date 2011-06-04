using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    class EnemySpawnZone
    {
        string enemyName;
        Rectangle zone;
        int totalSpawns;
        int currentSpawns;
        Vector2[] spawnPositions;

        public EnemySpawnZone(string enemyName, Rectangle zone, int totalSpawns)
        {
            this.enemyName = enemyName;
            this.zone = zone;
            this.totalSpawns = totalSpawns;
            spawnPositions = new Vector2[totalSpawns];
        }
        public EnemySpawnZone(Rectangle zone):this("grape", zone, 25) {}

        public void setEnemyName(string enemyName)
        {
            this.enemyName = enemyName;
        }
        public void setTotalSpawns(int totalSpawns)
        {
            this.totalSpawns = totalSpawns;
            spawnPositions = new Vector2[totalSpawns];
        }
        public string getEnemyName()
        {
            return enemyName;
        }
        public int getTotalSpawns()
        {
            return totalSpawns;
        }
        public Rectangle getZone()
        {
            return zone;
        }

        public void setZone(Rectangle rect)
        {
            this.zone = rect;
        }

        void setNewSpawnPosition(float cameraTopY, Enemy enemy)
        {
            Vector2 spawnPosition = Vector2.Zero;
            spawnPosition.Y = cameraTopY + enemy.getRadius();
            bool found = false;
            float allowedDistance = enemy.getRadius();
            float allowedDistanceSquared;
            int step = -1;
            do
            {
                ++step;
                allowedDistance += 5.0f;
                allowedDistanceSquared = allowedDistance * allowedDistance;
                spawnPosition.X = Calc.randomScalar(zone.Left, zone.Right);
                for (int i = 0; i < spawnPositions.Length; ++i)
                {
                    if (Vector2.DistanceSquared(spawnPosition, spawnPositions[i]) > allowedDistanceSquared)
                    {
                        found = true;
                        break;
                    }
                }

            }
            while (!found);

            enemy.position2D = spawnPosition;
        }

        public bool update(float cameraTopY)
        {
            float zoneHeight = zone.Bottom - zone.Top;
            float cameraInZone = 0.0f;
            if (cameraTopY > zone.Top)
            {
                cameraInZone = cameraTopY - zone.Top;
            }
            else
            {
                return true;
            }
            float percentageDone = cameraInZone / zoneHeight;
            if (percentageDone > 1.0f) percentageDone = 1.0f;
            int enemiesThatMustHaveSpawned = (int)(percentageDone * totalSpawns);
            
            if (enemiesThatMustHaveSpawned > currentSpawns)
            {
                // get an instance of the enemy type that we need, and add it to the manager
                Enemy enemy = (Enemy)EnemyManager.Instance.addEnemy(enemyName, Vector3.Zero);
                // now that we have the enemy we can add set the spawn position (we need to know enemy's radius)
                setNewSpawnPosition(cameraTopY, enemy);
                // add that position to the already spawned positions
                spawnPositions[enemiesThatMustHaveSpawned - 1] = enemy.position2D;
                ++currentSpawns;
            }

            return currentSpawns < totalSpawns;
        }
    }
}
