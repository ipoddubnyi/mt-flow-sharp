
namespace Mt.Flow.Sharp.Parser.Common
{
    public class DoubleValue : NumberValue
    {
        public static DoubleValue ZERO = new DoubleValue(0.0);

        private readonly double value;

        public DoubleValue(int value)
        {
            this.value = value;
        }

        public DoubleValue(double value)
        {
            this.value = value;
        }

        public DoubleValue(string value)
        {
            this.value = double.Parse(value);
        }

        public DoubleValue(bool value)
        {
            this.value = value ? 1 : 0;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        protected override NumberValue Negate(NumberValue v)
        {
            return new DoubleValue(-(v as DoubleValue).value);
        }

        protected override NumberValue Add(NumberValue v1, NumberValue v2)
        {
            return new DoubleValue((v1 as DoubleValue).value + (v2 as DoubleValue).value);
        }

        protected override NumberValue Sub(NumberValue v1, NumberValue v2)
        {
            return new DoubleValue((v1 as DoubleValue).value - (v2 as DoubleValue).value);
        }

        protected override NumberValue Mul(NumberValue v1, NumberValue v2)
        {
            return new DoubleValue((v1 as DoubleValue).value * (v2 as DoubleValue).value);
        }

        protected override NumberValue Div(NumberValue v1, NumberValue v2)
        {
            return new DoubleValue((v1 as DoubleValue).value / (v2 as DoubleValue).value);
        }

        public static explicit operator int(DoubleValue v) => (int)v.value;

        public static implicit operator double(DoubleValue v) => v.value;
    }
}

