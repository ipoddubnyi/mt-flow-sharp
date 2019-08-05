using Mt.Flow.Sharp.Parser.Common;

namespace Mt.Flow.Sharp.Parser.AST.Expressions
{
    public class ValueExpression : IExpression
    {
        private readonly IValue value;

        public ValueExpression(int value)
        {
            this.value = new IntegerValue(value);
        }

        public ValueExpression(double value)
        {
            this.value = new DoubleValue(value);
        }

        public ValueExpression(string value)
        {
            this.value = new StringValue(value);
        }

        public IValue Eval()
        {
            return value;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }
}
