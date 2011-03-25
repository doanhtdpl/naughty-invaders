using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyGame
{
    public class EntityComparer : IComparer<SortableEntity>
    {
        // sorts in descending order
        public int Compare(SortableEntity e1, SortableEntity e2)
        {
            if (e1.getZ() > e2.getZ())
                return 1;
            if (e1.getZ() < e2.getZ())
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
        List<SortableEntity> entities = new List<SortableEntity>();

        #region ENTITY MANAGEMENT
        public void registerEntity(SortableEntity entity)
        {
            if(!entities.Contains(entity))
                entities.Add(entity);
        }
        public void removeEntity(SortableEntity entity)
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

        public void render()
        {
            sortEntities();
            foreach (SortableEntity e in entities)
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
            foreach (SortableEntity ent in entities)
            {
                ent.reset();
            }
        }
    }
}
