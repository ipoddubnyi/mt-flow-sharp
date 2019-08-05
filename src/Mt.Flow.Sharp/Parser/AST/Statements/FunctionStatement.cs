using Mt.Flow.Sharp.Parser.AST.Expressions;

namespace Mt.Flow.Sharp.Parser.AST.Statements
{
    public class FunctionStatement : IStatement
    {
        public FunctionalExpression Function { get; private set; }

        public FunctionStatement(FunctionalExpression function)
        {
            Function = function;
        }

        public void Execute()
        {
            Function.Eval();
        }

        public override string ToString()
        {
            return Function.ToString();
        }
    }
}
