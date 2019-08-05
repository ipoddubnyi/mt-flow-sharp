
namespace Mt.Flow.Sharp.Parser.Common
{
    public abstract class NumberValue : IValue
    {
        protected abstract NumberValue Negate(NumberValue v);

        protected abstract NumberValue Add(NumberValue v1, NumberValue v2);

        protected abstract NumberValue Sub(NumberValue v1, NumberValue v2);

        protected abstract NumberValue Mul(NumberValue v1, NumberValue v2);

        protected abstract NumberValue Div(NumberValue v1, NumberValue v2);

        public static NumberValue operator +(NumberValue v) => v;

        public static NumberValue operator -(NumberValue v) => -v;

        public static NumberValue operator +(NumberValue v1, NumberValue v2) => v1 + v2;

        public static NumberValue operator -(NumberValue v1, NumberValue v2) => v1 - v2;

        public static NumberValue operator *(NumberValue v1, NumberValue v2) => v1 * v2;

        public static NumberValue operator /(NumberValue v1, NumberValue v2) => v1 / v2;
    }
}
