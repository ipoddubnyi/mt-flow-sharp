
namespace Mt.Flow.Sharp.Parser
{
    public class TokenOutdent : Token
    {
        public int Value { get; private set; }

        public TokenOutdent(int value) :
            base (TokenType.Outdent, "")
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"{Type} {Value}";
        }

        public void Consume(int outdent)
        {
            Value -= outdent;
        }
    }
}
