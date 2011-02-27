using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame
{
    public class Entity2DComparer : IComparer<Entity2D>
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

        Entity2DComparer comparer = new Entity2DComparer();
        List<Entity2D> entities = new List<Entity2D>();

        #region ENTITY MANAGEMENT
        public void registerEntity(Entity2D entity)
        {
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
        #endregion

        public void render()
        {
            sortEntities();
            foreach (Entity2D e in entities)
            {
                e.render();
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
    }
}
