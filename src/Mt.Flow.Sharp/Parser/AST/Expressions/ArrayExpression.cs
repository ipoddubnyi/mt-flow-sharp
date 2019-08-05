using System.Collections.Generic;
using System.Text;
using Mt.Flow.Sharp.Parser.Common;
using Mt.Flow.Sharp.Parser.Utils;

namespace Mt.Flow.Sharp.Parser.AST.Expressions
{
    public class ArrayExpression : IExpression
    {
        public List<IExpression> elements;

        public ArrayExpression(List<IExpression> elements)
        {
            this.elements = elements;
        }

        public IValue Eval()
        {
            var size = elements.Count;
            var array = new ArrayValue(size);
            for (int i = 0; i < size; ++i)
            {
                array[i] = elements[i].Eval();
            }
            return array;
        }

        public override string ToString()
        {
            var buffer = new StringBuilder();
            buffer.Append('[');
            buffer.Append(elements.ToStringLine());
            buffer.Append(']');
            return buffer.ToString();
        }
    }
}
