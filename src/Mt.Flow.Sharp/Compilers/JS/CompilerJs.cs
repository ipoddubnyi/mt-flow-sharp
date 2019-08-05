using System;
using System.Text;
using Mt.Flow.Sharp.Parser.AST.Statements;
using Mt.Flow.Sharp.Parser.Common;
using Mt.Flow.Sharp.Parser.Utils;

namespace Mt.Flow.Sharp.Compilers.JS
{
    class CompilerJs
    {
        private StringBuilder buffer;
        private int indentsize;

        public CompilerJs(int indentsize = 4)
        {
            this.indentsize = indentsize;
        }

        public string Compile(IStatement program)
        {
            buffer = new StringBuilder();

            var block = program as BlockStatement;
            foreach (var statement in block.Statements)
            {
                Run(statement, 0);
            }

            return buffer.ToString();
        }

        public void Run(IStatement statement, int currentindent = 0)
        {
            if (statement is BlockStatement)
            {
                RunBlockStatement(statement as BlockStatement, currentindent);
            }
            else if (statement is FunctionDefineStatement)
            {
                RunFunctionDefineStatement(statement as FunctionDefineStatement, currentindent);
            }
            else if (statement is AssignmentStatement)
            {
                RunAssignmentStatement(statement as AssignmentStatement, currentindent);
            }
            else if (statement is ArrayAssignmentStatement)
            {
                RunArrayAssignmentStatement(statement as ArrayAssignmentStatement, currentindent);
            }
            else if (statement is IfStatement)
            {
                RunIfStatement(statement as IfStatement, currentindent);
            }
            else if (statement is WhileStatement)
            {
                RunWhileStatement(statement as WhileStatement, currentindent);
            }
            else
            {
                buffer.Append(Indent(currentindent));
                buffer.AppendLine(statement.ToString());
            }
        }
        public void RunBlockStatement(BlockStatement block, int currentindent)
        {
            foreach (var statement in block.Statements)
            {
                Run(statement, currentindent + indentsize);
            }
        }

        public void RunFunctionDefineStatement(FunctionDefineStatement function, int currentindent)
        {
            buffer.Append(Indent(currentindent));
            buffer.AppendLine($"function {function.Name}({function.ArgumentNames.ToStringLine()}) {{");
            foreach (var arg in function.ArgumentNames)
            {
                DeclareVariable(arg);
            }
            Run(function.Body, currentindent);
            buffer.Append(Indent(currentindent));
            buffer.AppendLine("}");
        }

        public void RunAssignmentStatement(AssignmentStatement statement, int currentindent)
        {
            buffer.Append(Indent(currentindent));
            if (!IsVariableDeclared(statement.Variable))
            {
                buffer.Append("let ");
                DeclareVariable(statement.Variable);
            }

            buffer.AppendLine($"{statement.Variable} = {statement.Expression};");
        }

        public void RunArrayAssignmentStatement(ArrayAssignmentStatement statement, int currentindent)
        {
            buffer.Append(Indent(currentindent));
            buffer.AppendLine($"{statement.Array} = {statement.Expression};");
        }

        public void RunIfStatement(IfStatement ifstatement, int currentindent)
        {
            buffer.Append(Indent(currentindent));
            buffer.AppendLine($"if {ifstatement.expression} {{");
            Run(ifstatement.ifStatement, currentindent);
            if (null != ifstatement.elseStatement)
            {
                buffer.Append(Indent(currentindent));
                buffer.AppendLine("} else {{");
                Run(ifstatement.elseStatement, currentindent);
            }
            buffer.Append(Indent(currentindent));
            buffer.AppendLine("}");
        }

        public void RunWhileStatement(WhileStatement whilestatement, int currentindent)
        {
            buffer.Append(Indent(currentindent));
            buffer.AppendLine($"while {whilestatement.Condition} {{");
            Run(whilestatement.Statement, currentindent);
            buffer.Append(Indent(currentindent));
            buffer.AppendLine("}");
        }

        private string Indent(int size)
        {
            return new String(' ', size);
        }

        private bool IsVariableDeclared(string variable)
        {
            return Variables.Exists(variable);
        }

        private void DeclareVariable(string variable)
        {
            Variables.Set(variable, IntegerValue.ZERO);
        }
    }
}
