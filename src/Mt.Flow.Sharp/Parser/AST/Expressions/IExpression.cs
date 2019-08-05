using Mt.Flow.Sharp.Parser.Common;

namespace Mt.Flow.Sharp.Parser.AST.Expressions
{
    // AST - абстрактное синтаксическое дерево

    public interface IExpression
    {
        IValue Eval(); // Посчитать значение
    }
}
