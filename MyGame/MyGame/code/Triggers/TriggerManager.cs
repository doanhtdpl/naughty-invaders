using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;

namespace MyGame
{
    class TriggerManager
    {
        static TriggerManager instance = null;
        TriggerManager()
        {
        }
        public static TriggerManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new TriggerManager();
                }
                return instance;
            }
        }

        List<Trigger> triggers = new List<Trigger>();
        public void addTrigger(Trigger trigger)
        {
            triggers.Add(trigger);
        }

        // check all triggers
        public void update()
        {
            for(int i=0; i<triggers.Count; ++i)
            {
                if (triggers[i].isTriggered())
                {
                    if (triggers[i].execute())
                    {
                        triggers.RemoveAt(i);
                        --i;
                    }
                }
            }
        }
        public void clean()
        {
            triggers.Clear();
        }
        public void dispose()
        {
            clean();
        }
        public void render()
        {
            for (int i = 0; i < triggers.Count; ++i)
            {
            }
        }
    }
}
