using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class Enemy : CollidableEntity2D
    {
        public Enemy(string entityName, Vector3 position, float orientation)
            : base("enemies", entityName, position, orientation)
        {
            state = Entity2D.tEntityState.Waiting;
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(0, 0), scale.X * 0.45f);
        }

        public override bool gotHitAtPart(int partIndex, float damage)
        {
            return true;
        }

        public override void update()
        {
            base.update();
        }

        public override void render()
        {
            base.render();
        }

        public override void delete()
        {
            EnemyManager.Instance.removeEnemy(this);
            base.delete();
        }
        public override void requestDelete()
        {
            base.requestDelete();
            EnemyManager.Instance.requestDeleteOf(this);
        }
    }
}
