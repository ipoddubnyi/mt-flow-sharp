
namespace Mt.Flow.Sharp.Parser
{
    public class TokenIndent : Token
    {
        public int Value { get; private set; }

        public TokenIndent(int value) :
            base (TokenType.Indent, "")
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"{Type} {Value}";
        }
    }
}
