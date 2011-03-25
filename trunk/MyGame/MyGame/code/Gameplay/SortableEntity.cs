using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MyGame
{
    public class SortableEntity
    {
        public SortableEntity()
        {
            EntityManager.Instance.registerEntity(this);
        }
        public virtual float getZ() { return 0.0f; }
        public virtual void render() { }
        public virtual void reset() { }
    }
}
