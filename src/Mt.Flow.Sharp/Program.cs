using Mt.Flow.Sharp.Compilers.JS;
using Mt.Flow.Sharp.Parser;
using System;
using System.IO;
using System.Text;

namespace Mt.Flow.Sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
#if DEBUG
                if (0 == args.Length)
                {
                    args = new string[1]
                    {
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "sort.fsh")
                    };
                }
#endif

                foreach (var arg in args)
                {
                    var inputPath = arg;

                    string input = File.ReadAllText(inputPath, Encoding.UTF8);
                    var tokens = new Lexer(input).Tokenize();
                    foreach (var token in tokens)
                    {
                        Console.WriteLine(token);
                    }

                    Console.WriteLine("======================");
                    var program = new Parser.Parser(tokens).Parse();
                    Console.WriteLine(program);

                    //Console.WriteLine("======================");
                    //program.Execute();

                    Console.WriteLine("======================");

                    var compiler = new CompilerJs(4);
                    var jscode = compiler.Compile(program);

                    var dirPath = Path.GetDirectoryName(inputPath);
                    var outputName = Path.GetFileNameWithoutExtension(inputPath) + ".js";
                    var outputPath = Path.Combine(dirPath, outputName);
                    File.WriteAllText(outputPath, jscode, Encoding.UTF8);

                    Console.Write(jscode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.ReadKey();
        }
    }
}
