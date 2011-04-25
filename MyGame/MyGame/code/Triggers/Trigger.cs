using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;

namespace MyGame
{
    class Trigger
    {
        public int executionTimes = 1;

        List<TriggerFunction> conditions = new List<TriggerFunction>();
        List<TriggerFunction> executions = new List<TriggerFunction>();

        public void addFunction(bool isCondition, string functionName, params object[] parameters)
        {
            string functionContainer = "";
            if (isCondition)
            {
                functionContainer = "ConditionFunctions";
            }
            else
            {
                functionContainer = "ConsequenceFunctions";
            }
            Type type = Type.GetType("MyGame." + functionContainer);
            MethodInfo method = type.GetMethod(functionName);

            TriggerFunction tf = new TriggerFunction(method, parameters);

            if (isCondition)
            {
                conditions.Add(tf);
            }
            else
            {
                executions.Add(tf);
            }
        }

        public void initTrigger()
        {

        }

        public bool isTriggered()
        {
            foreach (TriggerFunction condition in conditions)
            {
                if (!condition.execute())
                {
                    return false;
                }
            }
            return true;
        }
        public bool execute()
        {
            foreach (TriggerFunction execution in executions)
            {
                execution.execute();
            }
            --executionTimes;
            return executionTimes > 0;
        }
    }
}
