﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class Enemy : CollidableEntity2D
    {
        public int enemyLevel { get; set; }

        public Enemy(string entityName, Vector3 position, float orientation, int enemyLevel, int id = -1)
            : base("enemies", entityName, position, orientation, Color.White, id)
        {
            this.enemyLevel = enemyLevel;
        }

        public override void setCollisions()
        {
            addCollision(new Vector2(0, 0), scale.X * 0.45f);
        }

        public override bool gotHitAtPart(CollidableEntity2D ce, int partIndex)
        {
            ParticleManager.Instance.addParticles(entityName + "GotHit", this.position, Vector3.Zero, Color.White);
            return true;
        }

        public override void die()
        {
            ParticleManager.Instance.addParticles(entityName + "Dies", this.position, Vector3.Zero, Color.White);
            OrbManager.Instance.addRandomOrbs( enemyLevel, position2D);
            base.die();
        }

        public override void update()
        {
            base.update();
        }

        public override void render()
        {
            base.render();
        }

        public override void reset()
        {
            base.reset();
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
