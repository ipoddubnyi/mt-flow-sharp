
namespace Mt.Flow.Sharp.Parser
{
    public enum TokenType
    {
        Number,
        Word,
        Text,       // текст в кавычках

        Plus,       // +
        Minus,      // -
        Star,       // *
        Slash,      // /

        Excl,       // !
        Bar,        // |
        Amp,        // &

        Eq,         // =
        Lt,         // <
        Gt,         // >
        LtEq,       // <=
        GtEq,       // >=

        EqEq,       // ==
        ExclEq,     // !=
        BarBar,     // ||
        AmpAmp,     // &&

        LParen,     // (
        RParen,     // )
        LBracket,   // [
        RBracket,   // ]

        Comma,      // ,
        Colon,      // :

        GotoBack,       // <-
        GotoForward,    // ->

        NewLine,
        Indent,     // отступ вперёд
        Outdent,    // отступ назад

        EOF
    }
}
