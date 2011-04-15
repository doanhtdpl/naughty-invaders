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
        public enum tSpecial { BreaksGuard };
        protected float life;
        List<CollidablePart> parts = new List<CollidablePart>();

        public List<CollidablePart> getParts() { return parts; }
        public tSpecial special { get; set; }

        public float damage { set; get; }

        public CollidableEntity2D(string entityFolder, string entityName, Vector3 position, float orientation, Color color, float damage = 0.0f, int id = -1)
            : base(entityFolder, entityName, position, orientation, color, id)
        {
            this.damage = damage;
        }

        public virtual void setCollisions()
        {
            // OVERRIDE to hardcode the collisions of each animatedentity
        }

        public void addCollision(Vector2 centerOfMass, float radius)
        {
            parts.Add(new CollidablePart(centerOfMass, radius));
        }

        // returns true if collides with the projectile. This method calls gotHitAtPart child method to see if the entity dies
        public bool collidesWith(CollidableEntity2D ce, ref bool entityAlive)
        {
            for (int i = 0; i < parts.Count; ++i)
            {
                List<CollidablePart> ceParts = ce.getParts();
                for (int j = 0; j < ceParts.Count; ++j)
                {
                    Vector2 v = (position2D + parts[i].centerOfMass) - (ce.position2D + ceParts[j].centerOfMass);
                    if (v.Length() < parts[i].radius + ceParts[j].radius)
                    {
                        entityAlive = gotHitAtPart(ce, i);
                        return true;
                    }
                }
            }
            return false;
        }

        // returns true if this collidable entity dies
        public virtual bool gotHitAtPart(CollidableEntity2D ce, int partIndex)
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

#if DEBUG
        public void renderCollisionParts()
        {
            for (int i = 0; i < parts.Count; ++i)
            {
                DebugManager.Instance.addCircle(position2D + parts[i].centerOfMass, parts[i].radius, 15);
            }
        }
#endif
    }
}
