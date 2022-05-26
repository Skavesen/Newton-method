using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VP_LW_4.Parser
{
    public static class ExpressionConverter
    {

        private static string PreProcessExpression(string expression)
        {
            expression = expression.ToLower();

            string newExpression = "";
            for(int i = 0; i < expression.Length - 1;i++)
            {
                if (expression[i] != ' ' && expression[i] != '.' 
                    && expression[i + 1] !='.' && expression[i+1] != ' ' &&
                    !(expression[i].IsDigit() && expression[i + 1].IsDigit())
                    && !(expression[i].IsLetter() && expression[i+1].IsLetter()))
                {
                    newExpression += $"{expression[i]} ";
                }
                else
                {
                    newExpression += expression[i];
                }
            }
            newExpression += expression[expression.Length - 1];
            return newExpression;
        }

        public static string ConvertToPostFixNotation(string expression)
        {
            string processedExpression = PreProcessExpression(expression);
            List<string> splitExpression = processedExpression.Split(' ').ToList();
            Queue<string> outputQueue = new Queue<string>();
            Stack<string> operatorStack = new Stack<string>();
            splitExpression.ForEach((token) => {
                if (token.IsNumber() || token.IsVariable())
                {
                    outputQueue.Enqueue(token);
                }
                else if (token.IsOperator())
                {
                    while (operatorStack.Count > 0
                    && operatorStack.Peek().IsOperator()
                    && (ParseHelper.tokenInformation[operatorStack.Peek()].precedence > ParseHelper.tokenInformation[token].precedence
                    || (ParseHelper.tokenInformation[operatorStack.Peek()].precedence == ParseHelper.tokenInformation[token].precedence)
                    && ParseHelper.tokenInformation[operatorStack.Peek()].isLeftAssociative)
                    && operatorStack.Peek() != "(")
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                    }
                    operatorStack.Push(token);

                }
                else if (token == "(")
                {
                    operatorStack.Push(token);
                }
                else if (token == ",")
                {
                    operatorStack.Push(token);
                }
                else if (token == ")")
                {
                    while(operatorStack.Count > 0
                    && operatorStack.Peek() != "(")
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                    }
                    if (operatorStack.Peek() == "(")
                    {
                        operatorStack.Pop();
                    }
                    if( operatorStack.Peek().IsFunctionCall())
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                    }
                }
                else if(token.IsFunctionCall())
                {
                    operatorStack.Push(token);
                }
                else
                {

                }
            });
            while (operatorStack.Count > 0)
                outputQueue.Enqueue(operatorStack.Pop());

            string newNotation = "";
            while(outputQueue.Count>0)
            {
                newNotation += outputQueue.Dequeue() + " ";
            }
            return newNotation;
        }
    }
}
