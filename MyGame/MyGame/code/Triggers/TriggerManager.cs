using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyGame
{
    class TriggerFunction
    {
        public string functionName;
        public List<TriggerParameter> parameters = new List<TriggerParameter>();
    }
    class TriggerParameter
    {
        public enum tParameterType { Integer, Float, Double, String, Vector2, Vector3, Rectangle }
        string value;
    }

    class Trigger
    {
        public int executionTimes = 1;

        List<TriggerFunction> conditions = new List<TriggerFunction>();
        List<TriggerFunction> executions = new List<TriggerFunction>();

        public bool isTriggered()
        {
            bool isTriggered = false;
            foreach (TriggerFunction condition in conditions)
            {
                // call the function of the condition
                isTriggered = true;
            }
            return isTriggered;
        }
        public bool execute()
        {
            foreach (TriggerFunction condition in conditions)
            {
                // call the function to trigger

            }
            --executionTimes;
            return executionTimes > 0;
        }
    }

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

    }
}
