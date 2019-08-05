using System;
using Mt.Flow.Sharp.Parser.Common;

namespace Mt.Flow.Sharp.Parser.AST.Expressions
{
    public class UnaryExpression : IExpression
    {
        public readonly IExpression expr1;
        private readonly char operation;

        public UnaryExpression(char operation, IExpression expr1)
        {
            this.operation = operation;
            this.expr1 = expr1;
        }

        public IValue Eval()
        {
            var val = expr1.Eval();

            if (!(val is NumberValue))
                throw new Exception("Unary operation is only valid for numbers.");

            switch (operation)
            {
                case '-':
                    return -(val as NumberValue);
                case '+':
                default:
                    return val;
            }
        }

        public override string ToString()
        {
            return $"{operation}{expr1}";
        }
    }
}
