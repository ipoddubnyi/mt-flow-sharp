using Mt.Flow.Sharp.Parser.AST.Expressions;

namespace Mt.Flow.Sharp.Parser.AST.Statements
{
    public class ArrayAssignmentStatement : IStatement
    {
        public ArrayAccessExpression Array { get; private set; }
        public IExpression Expression { get; private set; }

        public ArrayAssignmentStatement(ArrayAccessExpression array, IExpression expression)
        {
            Array = array;
            Expression = expression;
        }

        public void Execute()
        {
            Array.GetArray()[Array.LastIndex()] = Expression.Eval();
        }

        public override string ToString()
        {
            return $"{Array} = {Expression}";
        }
    }
}
