using System;
using System.Collections.Generic;
using System.Text;

namespace Mt.Flow.Sharp.Parser
{
    public class Lexer
    {
        private static readonly string OPERATOR_CHARS = @"+-*/()[]{}=<>!&|,:";
        private static readonly Dictionary<string, TokenType> OPERATORS = new Dictionary<string, TokenType>()
        {
            { "+", TokenType.Plus },
            { "-", TokenType.Minus },
            { "*", TokenType.Star },
            { "/", TokenType.Slash },

            { "!", TokenType.Excl },
            { "|", TokenType.Bar },
            { "&", TokenType.Amp },

            { "=", TokenType.Eq },
            { "<", TokenType.Lt },
            { ">", TokenType.Gt },
            { "<=", TokenType.LtEq },
            { ">=", TokenType.GtEq },

            { "==", TokenType.EqEq },
            { "!=", TokenType.ExclEq },
            { "||", TokenType.BarBar },
            { "&&", TokenType.AmpAmp },

            { "(", TokenType.LParen },
            { ")", TokenType.RParen },
            { "[", TokenType.LBracket },
            { "]", TokenType.RBracket },

            { ",", TokenType.Comma },
            { ":", TokenType.Colon }
        };

        private readonly string input;
        private readonly int length;
        private readonly List<Token> tokens;
        private int position;
        private int indent;

        public Lexer(string input)
        {
            this.input = input;
            length = input.Length;
            tokens = new List<Token>();
            position = 0;
            indent = 0;
        }

        public List<Token> Tokenize()
        {
            while (position < length)
            {
                char current = Peek(0);
                if (char.IsDigit(current))
                {
                    TokenizeNumber();
                }
                else if (char.IsLetter(current))
                {
                    TokenizeWord();
                }
                else if ('"' == current)
                {
                    TokenizeText();
                }
                else if (-1 != OPERATOR_CHARS.IndexOf(current))
                {
                    TokenizeOperator();
                }
                else if ('\n' == current || '\r' == current)
                {
                    TokenizeNewLine();
                    TokenizeIndent();
                }
                else
                {
                    // пробельные символы
                    Next();
                }
            }
            return tokens;
        }

        private void TokenizeNumber()
        {
            var buffer = new StringBuilder();

            char current = Peek(0);
            while (true)
            {
                /*if (current == '.')
                {
                    if (-1 != buffer.IndexOf('.'))
                        throw new Exception("Invalid float number.");
                }
                else*/ if (!char.IsDigit(current))
                {
                    break;
                }
                buffer.Append(current);
                current = Next();
            }

            AddToken(TokenType.Number, buffer.ToString());
        }

        private void TokenizeWord()
        {
            var buffer = new StringBuilder();

            char current = Peek(0);
            while (true)
            {
                if (!char.IsLetterOrDigit(current) && current != '_')
                    break;

                buffer.Append(current);
                current = Next();
            }

            AddToken(TokenType.Word, buffer.ToString());
        }

        private void TokenizeText()
        {
            Next(); // skip "
            var buffer = new StringBuilder();

            char current = Peek(0);
            while (true)
            {
                // экранирование
                if ('\\' == current)
                {
                    current = Next();
                    switch (current)
                    {
                        case '"':
                            buffer.Append('"');
                            break;
                        case 'r':
                            buffer.Append('\r');
                            break;
                        case 'n':
                            buffer.Append('\n');
                            break;
                        case 't':
                            buffer.Append('\t');
                            break;
                        case '\\':
                            buffer.Append('\\');
                            break;
                        default:
                            throw new Exception($"Unkonwn special symbol {current}.");
                    }
                    current = Next();
                    continue;
                }

                if ('"' == current) break;
                buffer.Append(current);
                current = Next();
            }
            Next(); // skip closing "

            AddToken(TokenType.Text, buffer.ToString());
        }

        private void TokenizeNewLine()
        {
            var current = Peek(0);
            while (-1 != "\r\n".IndexOf(current))
                current = Next();

            // не учитываем несколько новых строк подряд
            if (!MatchLastToken(TokenType.NewLine))
                AddToken(TokenType.NewLine);
        }

        private void TokenizeIndent()
        {
            var buffer = new StringBuilder();

            int count = 0;
            var current = Peek(0);
            while (' ' == current)
            {
                count += 1;
                current = Next();
            }

            if (count > indent)
            {
                AddTokenIndent(count - indent);
                indent = count;
            }
            else if (count < indent)
            {
                AddTokenOutdent(indent - count);
                indent = count;
            }

            //AddTokenIndent(count);
        }

        private void TokenizeIf()
        {
            // <-
            // <condition>
            // a < b

            var current = Peek(0);
            if (current == '<')
            {
                char next = Peek(1);
                /*if (next == '-') // <-
                {
                    //Next();
                    //Next();
                    //TokenizeGoToBack();
                    return;
                }*/
                if (char.IsDigit(next))
                {
                    TokenizeNumber();
                }
                else if (char.IsLetter(next))
                {
                    TokenizeWord();
                }
            }
        }

        private void TokenizeOperator()
        {
            var current = Peek(0);
            if (current == '/')
            {
                if (Peek(1) == '/')
                {
                    Next();
                    Next();
                    TokenizeComment();
                    return;
                }
                else if (Peek(1) == '*')
                {
                    Next();
                    Next();
                    TokenizeMultilineComment();
                    return;
                }
            }

            if (current == '-')
            {
                if (Peek(1) == '>')
                {
                    Next();
                    Next();
                    AddToken(TokenType.GotoForward);
                    return;
                }
            }

            if (current == '<')
            {
                if (Peek(1) == '-')
                {
                    Next();
                    Next();
                    AddToken(TokenType.GotoBack);
                    return;
                }
            }

            var buffer = new StringBuilder();
            while (true)
            {
                var text = buffer.ToString();
                if (!OPERATORS.ContainsKey(text + current) && !string.IsNullOrEmpty(text))
                {
                    AddToken(OPERATORS[text]);
                    return;
                }
                buffer.Append(current);
                current = Next();
            }
        }

        private void TokenizeComment()
        {
            var current = Peek(0);
            while (-1 == "\r\n\0".IndexOf(current))
            {
                current = Next();
            }
        }

        private void TokenizeMultilineComment()
        {
            var current = Peek(0);
            while (true)
            {
                if (current == '\0') throw new Exception("Missing close tag.");
                if (current == '*' && Peek(1) == '/') break;
                current = Next();
            }
            Next(); // *
            Next(); // /
        }

        private char Next()
        {
            position++;
            return Peek(0); // текущий символ
        }

        private char Peek(int relativePos)
        {
            // 0 - текущий символ
            // 1 - следующий за текущим символ
            int pos = position + relativePos;
            if (pos >= length) return '\0';
            return input[pos];
        }

        private void AddToken(TokenType type, string text = "")
        {
            tokens.Add(new Token(type, text));
        }

        private void AddTokenIndent(int value)
        {
            tokens.Add(new TokenIndent(value));
        }

        private void AddTokenOutdent(int value)
        {
            tokens.Add(new TokenOutdent(value));
        }

        private bool MatchLastToken(TokenType type)
        {
            if (0 == tokens.Count) return false;
            var token = tokens[tokens.Count - 1];
            return type == token.Type;
        }
    }
}
