using System.Collections.Generic;
using System.Text;
using Mt.Flow.Sharp.Parser.AST.Expressions;

namespace Mt.Flow.Sharp.Parser.Utils
{
    public static class Extensions
    {
        public static int IndexOf(this StringBuilder sb, char c)
        {
            for (int i = 0; i < sb.Length; ++i)
            {
                if (sb[i] == c)
                    return i;
            }

            return -1;
        }

        public static string GetName(this ConditionalExpression.Operator op)
        {
            switch (op)
            {
                case ConditionalExpression.Operator.Plus:
                    return "+";
                case ConditionalExpression.Operator.Minus:
                    return "-";
                case ConditionalExpression.Operator.Multiply:
                    return "*";
                case ConditionalExpression.Operator.Divide:
                    return "/";

                case ConditionalExpression.Operator.Equals:
                    return "==";
                case ConditionalExpression.Operator.NotEquals:
                    return "!=";

                case ConditionalExpression.Operator.Lt:
                    return "<";
                case ConditionalExpression.Operator.LtEq:
                    return "<=";
                case ConditionalExpression.Operator.Gt:
                    return ">";
                case ConditionalExpression.Operator.GtEq:
                    return ">=";

                case ConditionalExpression.Operator.And:
                    return "&&";
                case ConditionalExpression.Operator.Or:
                    return "||";
            }

            return op.ToString();
        }

        public static string ToStringLine(this List<string> strings)
        {
            var buffer = new StringBuilder();
            foreach (var str in strings)
            {
                if (buffer.Length > 1)
                    buffer.Append(", ");

                buffer.Append(str);
            }
            return buffer.ToString();
        }

        public static string ToStringLine(this List<IExpression> expressions)
        {
            var buffer = new StringBuilder();
            foreach (var expression in expressions)
            {
                if (buffer.Length > 1)
                    buffer.Append(", ");

                buffer.Append(expression);
            }
            return buffer.ToString();
        }
    }
}
