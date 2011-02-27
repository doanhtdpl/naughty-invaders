using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using System.Globalization;

namespace MyGame
{
    public struct CollidablePart
    {
        public Vector2 centerOfMass;
        public float radius;

        public CollidablePart(Vector2 centerOfMass, float radius)
        {
            this.centerOfMass = centerOfMass;
            this.radius = radius;
        }
    }

    public class CollidableEntity2D : AnimatedEntity2D
    {
        protected float life;
        List<CollidablePart> parts = new List<CollidablePart>();

        public List<CollidablePart> getParts() { return parts; }

        public CollidableEntity2D(string entityFolder, string entityName, Vector3 position, float orientation, int id = -1)
            : base(entityFolder, entityName, position, orientation, id)
        {
        }

        public virtual void setCollisions()
        {
            // OVERRIDE to hardcode the collisions of each animatedentity
        }

        public void addCollision(Vector2 centerOfMass, float radius)
        {
            parts.Add(new CollidablePart(centerOfMass, radius));
        }

        // returns true if collides
        public bool collidesWith(Projectile p, ref bool entityAlive)
        {
            for (int i = 0; i < parts.Count; ++i)
            {
                List<CollidablePart> projectileParts = p.getParts();
                for (int j = 0; j < projectileParts.Count; ++j)
                {
                    Vector2 v = (position2D + parts[i].centerOfMass) - (p.position2D + projectileParts[j].centerOfMass);
                    if (v.Length() < parts[i].radius + projectileParts[j].radius)
                    {
                        entityAlive = gotHitAtPart(i, p.damage);
                        return true;
                    }
                }
            }
            return false;
        }

        // returns true if this collidable entity dies
        public virtual bool gotHitAtPart(int partIndex, float damage)
        {
            // OVERRIDE at each entity who wants specific behavior when hit
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
    }
}
