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

        Vector2 position;
        List<Function> conditions = new List<Function>();
        List<Function> executions = new List<Function>();

        public void setPosition(Vector2 position)
        {
            this.position = position;
        }

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
            Function tf = new Function(functionName, functionContainer, parameters);

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
            foreach (Function condition in conditions)
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
            foreach (Function execution in executions)
            {
                execution.execute();
            }
            --executionTimes;
            return executionTimes > 0;
        }
    }
}
