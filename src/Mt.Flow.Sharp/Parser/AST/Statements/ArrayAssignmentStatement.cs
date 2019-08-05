using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mt.Flow.Sharp.Parser.AST.Expressions;

namespace Mt.Flow.Sharp.Parser.AST.Statements
{
    public class ArrayAssignmentStatement : IStatement
    {
        public ArrayAccessExpression array;
        public IExpression expression;

        public ArrayAssignmentStatement(ArrayAccessExpression array, IExpression expression)
        {
            this.array = array;
            this.expression = expression;
        }

        public void Execute()
        {
            array.GetArray()[array.LastIndex()] = expression.Eval();
        }

        public override string ToString()
        {
            return $"{array} = {expression}";
        }
    }
}
