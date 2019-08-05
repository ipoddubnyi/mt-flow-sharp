using System.Collections.Generic;
using Mt.Flow.Sharp.Parser.Common;
using Mt.Flow.Sharp.Parser.Utils;

namespace Mt.Flow.Sharp.Parser.AST.Statements
{
    public class FunctionDefineStatement : IStatement
    {
        private string name;
        private List<string> argNames;
        public IStatement body;

        public FunctionDefineStatement(string name, List<string> argNames, IStatement body)
        {
            this.name = name;
            this.argNames = argNames;
            this.body = body;
        }

        public void Execute()
        {
            Functions.Set(name, new UserDefineFunction(argNames, body));
        }

        public override string ToString()
        {
            return $"{name}({argNames.ToStringLine()}) {body}";
        }
    }
}
