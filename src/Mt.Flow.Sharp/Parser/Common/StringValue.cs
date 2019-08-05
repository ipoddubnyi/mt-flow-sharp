
namespace Mt.Flow.Sharp.Parser.Common
{
    public class StringValue : IValue
    {
        public static StringValue Empty = new StringValue(string.Empty);

        private readonly string value;

        public StringValue(int value)
        {
            this.value = value.ToString();
        }

        public StringValue(double value)
        {
            this.value = value.ToString();
        }

        public StringValue(string value)
        {
            this.value = value;
        }

        public StringValue(bool value)
        {
            this.value = value.ToString();
        }

        public override string ToString()
        {
            return value;
        }

        public static explicit operator int(StringValue v) => int.Parse(v.value);

        public static explicit operator double(StringValue v) => double.Parse(v.value);

        public static StringValue operator +(StringValue v1, StringValue v2) => new StringValue(v1.value + v2.value);
    }
}
