using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;

namespace MyGame
{
    class TriggerFunction
    {
        MethodInfo method;
        public object[] parameters;

        public TriggerFunction(MethodInfo methodInfo, object[] parameters)
        {
            this.method = methodInfo;
            this.parameters = parameters;
        }

        public bool execute()
        {
            object result = method.Invoke(null, parameters);
            return (bool)result;
        }
    }
}
