using Mt.Flow.Sharp.Parser.Common;
using Mt.Flow.Sharp.Parser.AST.Expressions;

namespace Mt.Flow.Sharp.Parser.AST.Statements
{
    public class AssignmentStatement : IStatement
    {
        public string Variable { get; private set; }
        public IExpression Expression { get; private set; }

        public AssignmentStatement(string variable, IExpression expression)
        {
            Variable = variable;
            Expression = expression;
        }

        public void Execute()
        {
            var result = Expression.Eval();
            Variables.Set(Variable, result);
        }

        public override string ToString()
        {
            return $"{Variable} = {Expression}";
        }
    }
}
