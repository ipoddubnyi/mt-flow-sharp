using Mt.Flow.Sharp.Parser.Common;
using Mt.Flow.Sharp.Parser.AST.Expressions;

namespace Mt.Flow.Sharp.Parser.AST.Statements
{
    public class AssignmentStatement : IStatement
    {
        private string variable;
        private IExpression expression;

        public AssignmentStatement(string variable, IExpression expression)
        {
            this.variable = variable;
            this.expression = expression;
        }

        public void Execute()
        {
            var result = expression.Eval();
            Variables.Set(variable, result);
        }

        public override string ToString()
        {
            return $"{variable} = {expression}";
        }
    }
}
