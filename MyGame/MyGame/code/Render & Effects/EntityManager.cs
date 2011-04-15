using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    public class EntityComparer : IComparer<Entity2D>
    {
        // sorts in descending order
        public int Compare(Entity2D e1, Entity2D e2)
        {
            if (e1.position.Z > e2.position.Z)
                return 1;
            if (e1.position.Z < e2.position.Z)
                return -1;
            // equal
            return 0;
        }
    }

    // all renderable entities must go here, to sort them properly and avoid blending problems
    class EntityManager
    {
        static EntityManager instance = null;
        EntityManager()
        {
        }
        public static EntityManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EntityManager();
                }
                return instance;
            }
        }

        EntityComparer comparer = new EntityComparer();
        List<Entity2D> entities = new List<Entity2D>();

        #region ENTITY MANAGEMENT
        public void registerEntity(Entity2D entity)
        {
            if(!entities.Contains(entity))
                entities.Add(entity);
        }
        public void removeEntity(Entity2D entity)
        {
            entities.Remove(entity);
        }
        private void sortEntities()
        {
            entities.Sort(comparer);
        }
        public Entity2D getEntityByID(int id)
        {
            foreach (Entity2D ent in entities)
            {
                if (ent.id == id)
                {
                    return ent;
                }
            }
            return null;
        }
        #endregion

        void renderZ0Stuff()
        {
            ParticleManager.Instance.render();
            OrbManager.Instance.render();
        }

        public void render()
        {
            bool Z0rendered = false;
            sortEntities();
            foreach (Entity2D e in entities)
            {
                // render all things that must be rendered at Z = 0
                if (!Z0rendered)
                {
                    if (e.position.Z > 0.0f)
                    {
                        renderZ0Stuff();
                        Z0rendered = true;
                    }
                }
                e.render();
            }

            if (!Z0rendered)
            {
                renderZ0Stuff();
            }
        }

        public void clean()
        {
            entities.Clear();
        }

        public void reset()
        {
            foreach (Entity2D ent in entities)
            {
                ent.reset();
            }
        }

#if DEBUG
        public void renderCollisionParts()
        {
            foreach (Entity2D e in entities)
            {
                if (e is CollidableEntity2D)
                {
                    CollidableEntity2D ce = (CollidableEntity2D)e;
                    ce.renderCollisionParts();
                }
            }
        }
#endif
    }
}
