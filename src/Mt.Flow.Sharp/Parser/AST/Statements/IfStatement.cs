using System.Text;
using Mt.Flow.Sharp.Parser.AST.Expressions;

namespace Mt.Flow.Sharp.Parser.AST.Statements
{
    public class IfStatement : IStatement
    {
        public IExpression expression { get; private set; }
        public IStatement ifStatement { get; private set; }
        public IStatement elseStatement { get; private set; }

        public IfStatement(IExpression expression, IStatement ifStatement, IStatement elseStatement)
        {
            this.expression = expression;
            this.ifStatement = ifStatement;
            this.elseStatement = elseStatement;
        }

        public void Execute()
        {
            /*var result = expression.Eval().AsNumber();
            if (result != 0.0)
            {
                ifStatement.Execute();
            }
            else if (null != elseStatement)
            {
                elseStatement.Execute();
            }*/
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append("if ").Append(expression).Append(" ").Append(ifStatement);
            if (null != elseStatement)
            {
                result.Append("else ").Append(elseStatement);
            }
            return result.ToString();
        }
    }
}
