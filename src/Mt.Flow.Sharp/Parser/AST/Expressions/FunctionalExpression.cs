using System;
using System.Collections.Generic;
using Mt.Flow.Sharp.Parser.Common;

namespace Mt.Flow.Sharp.Parser.AST.Expressions
{
    public class FunctionalExpression : IExpression
    {
        private string name;
        public List<IExpression> arguments;

        public FunctionalExpression(string name)
        {
            this.name = name;
            this.arguments = new List<IExpression>();
        }

        public FunctionalExpression(string name, List<IExpression> arguments)
        {
            this.name = name;
            this.arguments = arguments;
        }

        public void AddArgument(IExpression arg)
        {
            arguments.Add(arg);
        }

        public IValue Eval()
        {
            var size = arguments.Count;
            var values = new IValue[size];
            for (var i = 0; i < size; ++i)
            {
                values[i] = arguments[i].Eval();
            }

            var function = Functions.Get(name);
            if (function is UserDefineFunction)
            {
                var userFunction = (UserDefineFunction)function;
                if (size != userFunction.GetArgsCount())
                    throw new Exception("args count mismatch.");

                Variables.Push();
                for (int i = 0; i < size; ++i)
                {
                    Variables.Set(userFunction.GetArgName(i), values[i]);
                }
                var result = userFunction.Execute(values);
                Variables.Pop();
                return result;
            }

            return function.Execute(values);
        }

        public override string ToString()
        {
            return $"{name}({arguments})";
        }
    }
}
