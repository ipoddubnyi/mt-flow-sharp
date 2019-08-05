using System;
using System.Text;

namespace Mt.Flow.Sharp.Parser.Common
{
    public class ArrayValue : IValue
    {
        private IValue[] elements;

        public IValue this[int index]
        {
            get => elements[index];

            set => elements[index] = value;
        }

        public ArrayValue(int size)
        {
            this.elements = new IValue[size];
        }

        public ArrayValue(IValue[] elements)
        {
            this.elements = new IValue[elements.Length];
            elements.CopyTo(this.elements, 0);
        }

        public ArrayValue(ArrayValue array) :
            this(array.elements)
        {
        }

        public override string ToString()
        {
            var buffer = new StringBuilder();
            foreach (var element in elements)
            {
                if (buffer.Length > 0) buffer.Append(", ");
                buffer.Append(element);
            }
            return "[" + buffer.ToString() + "]";
        }
    }
}
