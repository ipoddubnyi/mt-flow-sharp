using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mt.Flow.Sharp.Parser.Common;

namespace Mt.Flow.Sharp.Parser.AST.Expressions
{
    public class BinaryExpression : IExpression
    {
        private readonly IExpression expr1;
        private readonly IExpression expr2;
        private readonly char operation;

        public BinaryExpression(char operation, IExpression expr1, IExpression expr2)
        {
            this.operation = operation;
            this.expr1 = expr1;
            this.expr2 = expr2;
        }

        public IValue Eval()
        {
            var value1 = expr1.Eval() as IntegerValue;
            var value2 = expr2.Eval() as IntegerValue;
            /*if (value1 is StringValue || value1 is ArrayValue)
            {
                var string1 = value1.AsString();
                switch (operation)
                {
                    case '*':
                        // TODO: проверять, является ли второе значение числом

                        int iterations = (int)value2.AsNumber();
                        var buffer = new StringBuilder();
                        for (int i = 0; i < iterations; ++i)
                        {
                            buffer.Append(string1);
                        }
                        return new StringValue(buffer.ToString());
                    case '+':
                    default:
                        return new StringValue(string1 + value2.AsString());
                }
            }*/

            switch (operation)
            {
                case '-':
                    return value1 - value2;
                case '*':
                    return value1 * value2;
                case '/':
                    return value1 / value2; // пропускаем проверку деления на 0
                case '+':
                default:
                    return value1 + value2;
            }
        }

        public override string ToString()
        {
            return $"({expr1} {operation} {expr2})";
        }
    }
}
