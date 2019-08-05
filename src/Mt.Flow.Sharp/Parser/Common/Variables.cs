using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mt.Flow.Sharp.Parser.Common
{
    public class Variables
    {
        private static Dictionary<string, IValue> variables = new Dictionary<string, IValue>();

        private static Stack<Dictionary<string, IValue>> stack = new Stack<Dictionary<string, IValue>>();

        public static void Push()
        {
            // кладём именно копия словаря, т.к. переменные в словаре будут меняться
            stack.Push(new Dictionary<string, IValue>(variables));
        }

        public static void Pop()
        {
            variables = stack.Pop();
        }

        public static bool Exists(string key)
        {
            return variables.ContainsKey(key);
        }

        public static IValue Get(string key)
        {
            if (!Exists(key))
                throw new Exception("Variable is unset.");

            return variables[key];
        }

        public static void Set(string key, IValue value)
        {
            variables[key] = value;
        }
    }
}
