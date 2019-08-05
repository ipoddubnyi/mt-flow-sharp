using Mt.Flow.Sharp.Parser.AST.Expressions;

namespace Mt.Flow.Sharp.Parser.AST.Statements
{
    public class WhileStatement : IStatement
    {
        public IExpression condition;
        public IStatement statement;

        public WhileStatement(IExpression condition, IStatement statement)
        {
            this.condition = condition;
            this.statement = statement;
        }

        public void Execute()
        {
            /*while (condition.Eval().AsNumber() != 0)
            {
                try
                {
                    statement.Execute();
                }
                catch (BreakStatement)
                {
                    break;
                }
                catch (ContinueStatement)
                {
                    //continue;
                }
            }*/
        }

        public override string ToString()
        {
            return $"while {condition} {statement}";
        }
    }
}
