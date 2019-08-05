
namespace Mt.Flow.Sharp.Parser
{
    public class Token
    {
        public TokenType Type { get; private set; }
        public string Text { get; private set; }

        public Token(TokenType type, string text)
        {
            Type = type;
            Text = text;
        }

        public override string ToString()
        {
            return $"{Type} {Text}";
        }
    }
}
