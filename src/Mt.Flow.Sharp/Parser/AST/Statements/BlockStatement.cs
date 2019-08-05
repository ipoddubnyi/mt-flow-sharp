using System;
using System.Collections.Generic;
using System.Text;

namespace Mt.Flow.Sharp.Parser.AST.Statements
{
    public class BlockStatement : IStatement
    {
        public List<IStatement> Statements { get; private set; }

        public BlockStatement()
        {
            Statements = new List<IStatement>();
        }

        public void Add(IStatement statement)
        {
            Statements.Add(statement);
        }

        public void Execute()
        {
            foreach (var statement in Statements)
            {
                statement.Execute();
            }
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append("{").Append(Environment.NewLine);
            foreach (var statement in Statements)
            {
                result.Append(statement).Append(Environment.NewLine);
            }
            result.Append("}").Append(Environment.NewLine);
            return result.ToString();
        }
    }
}
