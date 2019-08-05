using Mt.Flow.Sharp.Parser.AST.Expressions;

namespace Mt.Flow.Sharp.Parser.AST.Statements
{
    public class WhileStatement : IStatement
    {
        public IExpression Condition { get; private set; }
        public IStatement Statement { get; private set; }

        public WhileStatement(IExpression condition, IStatement statement)
        {
            Condition = condition;
            Statement = statement;
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
            return $"while {Condition} {Statement}";
        }
    }
}
