using Mt.Flow.Sharp.Parser.AST.Expressions;

namespace Mt.Flow.Sharp.Parser.AST.Statements
{
    public class FunctionStatement : IStatement
    {
        public FunctionalExpression function;

        public FunctionStatement(FunctionalExpression function)
        {
            this.function = function;
        }

        public void Execute()
        {
            function.Eval();
        }

        public override string ToString()
        {
            return function.ToString();
        }
    }
}
