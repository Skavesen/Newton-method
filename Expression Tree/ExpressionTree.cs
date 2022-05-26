using VP_LW_4.Expression_Tree.Functions;
using VP_LW_4.Parser;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace VP_LW_4.Expression_Tree
{
    public class ExpressionTree
    {
        public IExpressionNode root;
        public ExpressionTree(string expression)
        {
            string postfixExpression = ExpressionConverter.ConvertToPostFixNotation(expression);

            List<string> tokens = postfixExpression.Split(' ').ToList();
            Stack<IExpressionNode> tokenStack = new Stack<IExpressionNode>();
            for(int i = 0;i<tokens.Count;i++)
            {
                if(tokens[i].IsNumber())
                {
                    var newNode = new Constant(double.Parse(tokens[i]));
                    tokenStack.Push(newNode);
                }
                else if (tokens[i].IsVariable())
                {
                    var newNode = new Variable(tokens[i]);
                    tokenStack.Push(newNode);
                }
                else if(tokens[i].IsOperator())
                {
                    var op1 = tokenStack.Pop();
                    var op2 = tokenStack.Pop();
                    var temporary = (IOperation)Activator.CreateInstance(ParseHelper.tokenInformation[tokens[i]].nodeClass);
                    temporary.LeftOperand = op2;
                    temporary.RightOperand = op1;
                    tokenStack.Push(temporary);
                }
                else if(tokens[i].IsFunctionCall())
                {
                    if (ParseHelper.tokenInformation[tokens[i]].numberOfParameters == 1)
                    {
                        var op1 = tokenStack.Pop();
                        var temporary = (IFunctionSingular)Activator.CreateInstance(ParseHelper.tokenInformation[tokens[i]].nodeClass);
                        temporary.Parameter = op1;
                        tokenStack.Push(temporary);
                    }
                    else
                    {
                        var op1 = tokenStack.Pop();
                        var op2 = tokenStack.Pop();
                        var temporary = (IFunctionDual)Activator.CreateInstance(ParseHelper.tokenInformation[tokens[i]].nodeClass);
                        temporary.FirstParameter = op2;
                        temporary.SecondParameter = op1;
                        tokenStack.Push(temporary);
                    }
                }
            }
            this.root = tokenStack.Pop().Simplify();
        }

        public string GetPostFixNotation()
        {
            return root.GetPostFixNotation();
        }
        public string GetPreFixNotation()
        {
            return root.GetPreFixNotation();
        }
        public string GetInFixNotation()
        {
            return root.GetInFixNotation();
        }
        public IExpressionNode GetDerivative()
        {
            return root.Derivate().Simplify();
        }
        public double Evaluate(Dictionary<string, double> input)
        {
            return root.Evaluate(input);
        }
    }
}
