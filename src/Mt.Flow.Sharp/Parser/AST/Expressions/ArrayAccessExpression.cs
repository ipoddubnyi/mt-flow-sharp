using System;
using System.Collections.Generic;
using System.Text;
using Mt.Flow.Sharp.Parser.Common;
using Mt.Flow.Sharp.Parser.Utils;

namespace Mt.Flow.Sharp.Parser.AST.Expressions
{
    public class ArrayAccessExpression : IExpression
    {
        public string variable;
        public List<IExpression> indices;

        public ArrayAccessExpression(string variable, List<IExpression> indices)
        {
            this.variable = variable;
            this.indices = indices;
        }

        public IValue Eval()
        {
            // получаем значение из последнего массива
            // arr[4][6][1]
            // arr[4], arr[4][6] - массивы
            // arr[4][6][1] - значение
            return GetArray()[LastIndex()];
        }

        public ArrayValue GetArray()
        {
            // последовательно получаем все массивы по индексам
            var array = ConsumeArray(Variables.Get(variable));
            int last = indices.Count - 1; // последний индекс - не массив, а значение
            for (int i = 0; i < last; ++i)
            {
                array = ConsumeArray(array[Index(i)]);
            }
            return array;
        }

        public int LastIndex()
        {
            return Index(indices.Count - 1);
        }

        private int Index(int index)
        {
            return indices[index].Eval() as IntegerValue;
        }

        private ArrayValue ConsumeArray(IValue value)
        {
            if (value is ArrayValue)
            {
                return (ArrayValue)value;
            }
            else
            {
                throw new Exception("Array expected.");
            }
        }

        public override string ToString()
        {
            var buffer = new StringBuilder();
            buffer.Append(variable);
            buffer.Append('[');
            buffer.Append(indices.ToStringLine());
            buffer.Append(']');
            return buffer.ToString();
        }
    }
}
