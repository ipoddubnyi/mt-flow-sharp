using System;
using System.Collections.Generic;

namespace Mt.Flow.Sharp.Parser.Common
{
    public class Functions
    {
        private static Dictionary<string, IFunction> functions = new Dictionary<string, IFunction>();

        public static bool Exists(string key)
        {
            return functions.ContainsKey(key);
        }

        public static IFunction Get(string key)
        {
            if (!Exists(key))
                throw new Exception($"Unknown function {key}.");

            return functions[key];
        }

        public static void Set(string key, IFunction function)
        {
            functions[key] = function;
        }
    }
}
