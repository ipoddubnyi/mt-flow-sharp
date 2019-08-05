using System;
using System.Collections.Generic;
using System.Globalization;

using Mt.Flow.Sharp.Parser.AST.Statements;
using Mt.Flow.Sharp.Parser.AST.Expressions;

namespace Mt.Flow.Sharp.Parser
{
    public class Parser
    {
        private static readonly Token EOF = new Token(TokenType.EOF, "");

        private readonly List<Token> tokens;
        private readonly int size;
        private int position;

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
            size = tokens.Count;
            position = 0;
        }

        public IStatement Parse()
        {
            var result = new BlockStatement();
            while (!Match(TokenType.EOF))
            {
                result.Add(Statement());
            }
            return result;
        }

        private IStatement Statement()
        {
            if (Match(TokenType.Lt))
            {
                return IfElseStatement();
            }
            if (Match(TokenType.Indent))
            {
                return Statement();
            }
            if (LookMatch(0, TokenType.LParen) && LookMatch(1, TokenType.Word))
            {
                return FunctionDefine();
            }
            if (LookMatch(0, TokenType.GotoForward) && LookMatch(1, TokenType.LParen))
            {
                return new FunctionStatement(FunctionCall());
            }
            if (Match(TokenType.NewLine))
            {
                return Statement();
            }
            return AssignmentStatement();
        }

        private IStatement FunctionDefine()
        {
            Consume(TokenType.LParen);
            var name = Consume(TokenType.Word).Text;
            var argNames = new List<string>();
            if (Match(TokenType.Colon))
            {
                while (!Match(TokenType.RParen))
                {
                    argNames.Add(Consume(TokenType.Word).Text);
                    Match(TokenType.Comma);
                }
            }
            else
            {
                // без аргументов
                Consume(TokenType.RParen);
            }
            Consume(TokenType.NewLine);
            var body = StatementOrBlock();
            return new FunctionDefineStatement(name, argNames, body);
        }

        private IStatement Block()
        {
            var token = Consume(TokenType.Indent);
            int indentvalue = (token as TokenIndent).Value;
            var block = new BlockStatement(indentvalue);
            while (!LookMatch(0, TokenType.Outdent))
            {
                block.Add(Statement());

                if (LookMatch(0, TokenType.NewLine))
                    Consume(TokenType.NewLine);
            }
            ConsumeOutdent(indentvalue);
            //Consume(TokenType.Outdent);
            return block;
        }

        private IStatement StatementOrBlock()
        {
            if (Get(0).Type == TokenType.Indent) return Block();
            return Statement();
        }

        private IStatement IfElseStatement()
        {
            var condition = Expression();
            Consume(TokenType.NewLine);
            if (Match(TokenType.Plus))
            {
                if (LookMatch(0, TokenType.NewLine))
                    Consume(TokenType.NewLine);
            }
            var ifStatement = StatementOrBlock();

            if (LookMatch(0, TokenType.GotoBack))
            {
                Consume(TokenType.GotoBack);
                return new WhileStatement(condition, ifStatement);
            }

            IStatement elseStatement = null;
            if (Match(TokenType.Minus))
            {
                if (LookMatch(0, TokenType.NewLine))
                    Consume(TokenType.NewLine);

                elseStatement = StatementOrBlock();
            }
            return new IfStatement(condition, ifStatement, elseStatement);
        }

        private IStatement AssignmentStatement()
        {
            // variable = ...
            if (LookMatch(0, TokenType.Word) && LookMatch(1, TokenType.Eq))
            {
                string variable = Consume(TokenType.Word).Text;
                Consume(TokenType.Eq); // пропускаем =
                return new AssignmentStatement(variable, Expression());
            }
            // array[...] = ...
            if (LookMatch(0, TokenType.Word) && LookMatch(1, TokenType.LBracket))
            {
                var array = ArrayElement();
                Consume(TokenType.Eq); // пропускаем =
                return new ArrayAssignmentStatement(array, Expression());
            }
            throw new Exception("Unknown statement.");
        }

        private IExpression Expression()
        {
            return LogicalOr();
        }

        private IExpression LogicalOr()
        {
            var result = LogicalAnd();

            while (true)
            {
                if (Match(TokenType.BarBar))
                {
                    result = new ConditionalExpression(ConditionalExpression.Operator.Or, result, LogicalAnd());
                    continue;
                }
                break;
            }

            return result;
        }

        private IExpression LogicalAnd()
        {
            var result = Equality();

            while (true)
            {
                if (Match(TokenType.AmpAmp))
                {
                    result = new ConditionalExpression(ConditionalExpression.Operator.And, result, Equality());
                    continue;
                }
                break;
            }

            return result;
        }

        private IExpression Equality()
        {
            // у операторов == и != приоритет ниже, чем у <, >, <=, >= и т.п.
            // поэтому вынесли в отдельный метод выше

            var result = Conditional();

            // TODO: почему здесь нет цикла?
            // Разобраться, зачем он вообще нужен.
            // Потому что операторы могут встречаться не один раз? Но без него, вроде, тоже работает

            if (Match(TokenType.EqEq))
            {
                return new ConditionalExpression(ConditionalExpression.Operator.Equals, result, Conditional());
            }
            if (Match(TokenType.ExclEq))
            {
                return new ConditionalExpression(ConditionalExpression.Operator.NotEquals, result, Conditional());
            }

            return result;
        }

        private IExpression Conditional()
        {
            var result = Additive();

            while (true)
            {
                if (Match(TokenType.Lt))
                {
                    result = new ConditionalExpression(ConditionalExpression.Operator.Lt, result, Additive());
                    continue;
                    //break;
                }
                if (Match(TokenType.LtEq))
                {
                    result = new ConditionalExpression(ConditionalExpression.Operator.LtEq, result, Additive());
                    continue;
                }
                // если это ">" и она не последняя в строке
                if (Match(TokenType.Gt) && !LookMatch(0, TokenType.NewLine))
                {
                    result = new ConditionalExpression(ConditionalExpression.Operator.Gt, result, Additive());
                    continue;
                    //break;
                }
                if (Match(TokenType.GtEq))
                {
                    result = new ConditionalExpression(ConditionalExpression.Operator.GtEq, result, Additive());
                    continue;
                }
                break;
            }

            return result;
        }

        private IExpression Additive()
        {
            var result = Multiplicative();

            while (true)
            {
                if (Match(TokenType.Plus))
                {
                    result = new BinaryExpression('+', result, Multiplicative());
                    continue;
                }
                if (Match(TokenType.Minus))
                {
                    result = new BinaryExpression('-', result, Multiplicative());
                    continue;
                }
                break;
            }

            return result;
        }

        private IExpression Multiplicative()
        {
            var result = Unary();

            while (true)
            {
                // 2 * 6 / 3
                if (Match(TokenType.Star))
                {
                    result = new BinaryExpression('*', result, Unary());
                    continue;
                }
                if (Match(TokenType.Slash))
                {
                    result = new BinaryExpression('/', result, Unary());
                    continue;
                }
                break;
            }

            return result;
        }

        private IExpression Unary()
        {
            if (Match(TokenType.Minus))
            {
                return new UnaryExpression('-', Primary());
            }
            // TODO: можно убрать лишнее условие
            if (Match(TokenType.Plus))
            {
                return Primary();
            }

            return Primary();
        }

        private IExpression Primary()
        {
            // TODO: берём current, затем в Match() повторяем то же самое
            var current = Get(0);
            if (Match(TokenType.Number))
            {
                //return new ValueExpression(double.Parse(current.Text, CultureInfo.InvariantCulture));
                return new ValueExpression(int.Parse(current.Text, CultureInfo.InvariantCulture));
            }
            if (LookMatch(0, TokenType.LParen) && LookMatch(1, TokenType.Word))
            {
                return FunctionCall();
            }
            if (LookMatch(0, TokenType.LBracket))
            {
                return ArrayInit();
            }
            if (LookMatch(0, TokenType.Word) && LookMatch(1, TokenType.LBracket))
            {
                return ArrayElement();
            }
            if (Match(TokenType.Word))
            {
                return new VariableExpression(current.Text);
            }
            if (Match(TokenType.Text))
            {
                return new ValueExpression(current.Text);
            }
            if (Match(TokenType.LParen))
            {
                var result = Expression(); // содержимое скобок
                Match(TokenType.RParen); // если ")" - значит, всё правильно
                return result;
            }

            throw new Exception("Unknown expression.");
        }

        private IExpression ArrayInit()
        {
            var elements = new List<IExpression>();
            Consume(TokenType.LBracket);
            while (!Match(TokenType.RBracket))
            {
                elements.Add(Expression());
                Match(TokenType.Comma);
            }
            return new ArrayExpression(elements); 
        }

        private ArrayAccessExpression ArrayElement()
        {
            var variable = Consume(TokenType.Word).Text;
            var indices = new List<IExpression>();
            do
            {
                Consume(TokenType.LBracket); // пропускаем [
                indices.Add(Expression());
                Consume(TokenType.RBracket); // пропускаем ]
            }
            while (LookMatch(0, TokenType.LBracket));
            return new ArrayAccessExpression(variable, indices);
        }

        private FunctionalExpression FunctionCall()
        {
            Consume(TokenType.GotoForward);
            Consume(TokenType.LParen);
            var name = Consume(TokenType.Word).Text;
            Consume(TokenType.RParen);
            // TODO: подумать, как передавать аргументы в функцию
            return new FunctionalExpression(name);
        }

        private Token Consume(TokenType type)
        {
            var current = Get(0);
            if (type != current.Type)
                throw new Exception($"Token {current} doesn't match {type}");

            position++;
            return current;
        }

        private Token ConsumeOutdent(int outdent)
        {
            var current = Get(0);
            if (TokenType.Outdent != current.Type)
                throw new Exception($"Token {current} doesn't match {TokenType.Outdent}");

            if ((current as TokenOutdent).Value > outdent)
            {
                (current as TokenOutdent).Consume(outdent);
                return current;
            }
            position++;
            return current;
        }

        // Проверяет правильный токен или нет
        // Если правильный - переходит на следующий
        private bool Match(TokenType type)
        {
            var current = Get(0);
            if (type != current.Type) return false;
            position++;
            return true;
        }

        private bool LookMatch(int relativePos, TokenType type)
        {
            return Get(relativePos).Type == type;
        }

        private Token Get(int relativePos)
        {
            int pos = position + relativePos;
            if (pos >= size) return EOF;
            return tokens[pos];
        }
    }
}
