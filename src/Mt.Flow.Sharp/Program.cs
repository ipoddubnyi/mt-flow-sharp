using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Mt.Flow.Sharp.Parser;

namespace Mt.Flow.Sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string input = File.ReadAllText("sort.fsh", Encoding.UTF8);
                var tokens = new Lexer(input).Tokenize();
                foreach (var token in tokens)
                {
                    Console.WriteLine(token);
                }

                /*Console.WriteLine("======================");
                var expressions = new Parser.Parser(tokens).Parse();
                foreach (var expression in expressions)
                {
                    Console.WriteLine(expression);
                }*/

                Console.WriteLine("======================");
                var program = new Parser.Parser(tokens).Parse();
                Console.WriteLine(program);

                /*Console.WriteLine("======================");
                program.Accept(new FunctionAdder());
                program.Accept(new VariablePrinter());
                program.Accept(new AssignValidator());
                program.Execute();*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadKey();
        }
    }
}
