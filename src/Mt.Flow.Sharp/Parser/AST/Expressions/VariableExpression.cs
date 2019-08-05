using System;
using Mt.Flow.Sharp.Parser.Common;

namespace Mt.Flow.Sharp.Parser.AST.Expressions
{
    public class VariableExpression : IExpression
    {
        private string name;

        public VariableExpression(string name)
        {
            this.name = name;
        }

        public IValue Eval()
        {
            if (!Variables.Exists(name))
                throw new Exception("Constant does not exist.");

            return Variables.Get(name);
        }

        public override string ToString()
        {
            //return $"{name} [{Constants.Get(name)}]";
            return name;
        }
    }
}
