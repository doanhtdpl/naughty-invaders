using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;

namespace MyGame
{
    class Function
    {
        public string functionName;
        string className;
        public object[] parameters;

        public Function(string functionName, string className, object[] parameters)
        {
            this.functionName = functionName;
            this.className = className;
            this.parameters = parameters;
        }

        public bool execute()
        {
            return (bool)ReflectionManager.Instance.executeFunction(functionName, className, parameters);
        }
    }
}
