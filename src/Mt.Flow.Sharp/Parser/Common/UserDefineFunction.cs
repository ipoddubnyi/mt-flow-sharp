using System.Collections.Generic;
using Mt.Flow.Sharp.Parser.AST.Statements;

namespace Mt.Flow.Sharp.Parser.Common
{
    public class UserDefineFunction : IFunction
    {
        private List<string> argNames;
        private IStatement body;

        public UserDefineFunction(List<string> argNames, IStatement body)
        {
            this.argNames = argNames;
            this.body = body;
        }

        public IValue Execute(params IValue[] args)
        {
            body.Execute();
            return IntegerValue.ZERO;

            /*try
            {
                body.Execute();
                return NumberValue.ZERO;
            }
            catch (ReturnStatement rt)
            {
                return rt.GetResult();
            }*/
        }

        public int GetArgsCount()
        {
            return argNames.Count;
        }

        public string GetArgName(int index)
        {
            if (index < 0 || index >= GetArgsCount()) return "";
            return argNames[index];
        }
    }
}
