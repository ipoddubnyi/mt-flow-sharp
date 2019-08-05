using System;
using System.Collections.Generic;
using System.Text;

namespace Mt.Flow.Sharp.Parser.AST.Statements
{
    public class BlockStatement : IStatement
    {
        private int indent;
        private List<IStatement> statements;

        public BlockStatement(int indent = 0)
        {
            this.indent = indent;
            statements = new List<IStatement>();
        }

        public void Add(IStatement statement)
        {
            statements.Add(statement);
        }

        public void Execute()
        {
            foreach (var statement in statements)
            {
                statement.Execute();
            }
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append(Environment.NewLine).Append(' ', indent).Append("{").Append(Environment.NewLine);
            foreach (var statement in statements)
            {
                result.Append(' ', indent).Append("  ").Append(statement).Append(Environment.NewLine);
            }
            result.Append(' ', indent).Append("}").Append(Environment.NewLine);
            return result.ToString();
        }
    }
}
