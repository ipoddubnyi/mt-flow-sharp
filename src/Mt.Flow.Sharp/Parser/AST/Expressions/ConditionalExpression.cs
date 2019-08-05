using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mt.Flow.Sharp.Parser.Common;
using Mt.Flow.Sharp.Parser.Utils;

namespace Mt.Flow.Sharp.Parser.AST.Expressions
{
    public class ConditionalExpression : IExpression
    {
        public enum Operator
        {
            Plus,
            Minus,
            Multiply,
            Divide,

            Equals,
            NotEquals,

            Lt,
            LtEq,
            Gt,
            GtEq,

            And,
            Or
        }

        private readonly IExpression expr1, expr2;
        private readonly Operator operation;

        public ConditionalExpression(Operator operation, IExpression expr1, IExpression expr2)
        {
            this.operation = operation;
            this.expr1 = expr1;
            this.expr2 = expr2;
        }

        public IValue Eval()
        {
            /*var value1 = expr1.Eval();
            var value2 = expr2.Eval();

            double number1, number2;
            if (value1 is StringValue)
            {
                number1 = value1.AsString().CompareTo(value2.AsString());
                number2 = 0;
            }
            else
            {
                number1 = value1.AsNumber();
                number2 = value2.AsNumber();
            }

            bool result;
            switch (operation)
            {
                case Operator.Lt: result = number1 < number2; break;
                case Operator.LtEq: result = number1 <= number2; break;
                case Operator.Gt: result = number1 > number2; break;
                case Operator.GtEq: result = number1 >= number2; break;
                case Operator.NotEquals: result = number1 != number2; break;

                case Operator.And: result = (0 != number1) && (0 != number2); break;
                case Operator.Or: result = (0 != number1) || (0 != number2); break;

                case Operator.Equals:
                default:
                    result = number1 == number2;
                    break;
            }
            return new NumberValue(result);*/
            return new StringValue("");
        }

        public override string ToString()
        {
            return $"({expr1} {operation.GetName()} {expr2})";
        }
    }
}
