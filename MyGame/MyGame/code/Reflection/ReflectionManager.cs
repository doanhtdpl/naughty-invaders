using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Reflection;

namespace MyGame
{
    class ReflectionManager
    {
        static ReflectionManager instance = null;
        ReflectionManager()
        {
        }
        public static ReflectionManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ReflectionManager();
                }
                return instance;
            }
        }

        // execute the specified static function of the specified class with optional parameters
        public object executeFunction(string functionName, string className, object[] parameters)
        {
            Type type = Type.GetType("MyGame." + className);
            MethodInfo method = type.GetMethod(functionName);
         
            object result = method.Invoke(null, parameters);
            return result;
        }
    }
}
