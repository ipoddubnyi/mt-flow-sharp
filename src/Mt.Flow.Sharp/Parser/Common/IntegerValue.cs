
namespace Mt.Flow.Sharp.Parser.Common
{
    public class IntegerValue : NumberValue
    {
        public static IntegerValue ZERO = new IntegerValue(0);

        private readonly int value;

        public IntegerValue(int value)
        {
            this.value = value;
        }

        public IntegerValue(double value)
        {
            this.value = (int)value;
        }

        public IntegerValue(string value)
        {
            this.value = int.Parse(value);
        }

        public IntegerValue(bool value)
        {
            this.value = value ? 1 : 0;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        protected override NumberValue Negate(NumberValue v)
        {
            return new IntegerValue(-(v as IntegerValue).value);
        }

        protected override NumberValue Add(NumberValue v1, NumberValue v2)
        {
            return new IntegerValue((v1 as IntegerValue).value + (v2 as IntegerValue).value);
        }

        protected override NumberValue Sub(NumberValue v1, NumberValue v2)
        {
            return new IntegerValue((v1 as IntegerValue).value - (v2 as IntegerValue).value);
        }

        protected override NumberValue Mul(NumberValue v1, NumberValue v2)
        {
            return new IntegerValue((v1 as IntegerValue).value * (v2 as IntegerValue).value);
        }

        protected override NumberValue Div(NumberValue v1, NumberValue v2)
        {
            return new IntegerValue((v1 as IntegerValue).value / (v2 as IntegerValue).value);
        }

        public static implicit operator int(IntegerValue v) => v.value;

        public static implicit operator double(IntegerValue v) => v.value;
    }
}
