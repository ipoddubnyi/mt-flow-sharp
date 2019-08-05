using System.Collections.Generic;
using Mt.Flow.Sharp.Parser.Common;
using Mt.Flow.Sharp.Parser.Utils;

namespace Mt.Flow.Sharp.Parser.AST.Statements
{
    public class FunctionDefineStatement : IStatement
    {
        public string Name { get; private set; }
        public List<string> ArgumentNames { get; private set; }
        public IStatement Body { get; private set; }

        public FunctionDefineStatement(string name, List<string> argumentNames, IStatement body)
        {
            Name = name;
            ArgumentNames = argumentNames;
            Body = body;
        }

        public void Execute()
        {
            Functions.Set(Name, new UserDefineFunction(ArgumentNames, Body));
        }

        public override string ToString()
        {
            return $"{Name}({ArgumentNames.ToStringLine()}) {Body}";
        }
    }
}
