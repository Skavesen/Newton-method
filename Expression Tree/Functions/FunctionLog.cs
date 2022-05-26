//using DerivationSimple.Drawer;
using VP_LW_4.Expression_Tree.Operations;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VP_LW_4.Expression_Tree.Functions
{
    class FunctionLog : IFunctionDual
    {
        public IExpressionNode FirstParameter { get; set; }
        public IExpressionNode SecondParameter { get; set; }

        public FunctionLog() { }
        public IExpressionNode Simplify()
        {           
            FirstParameter = FirstParameter.Simplify();
            SecondParameter = SecondParameter.Simplify();
            if (!FirstParameter.ContainsVariable() && !SecondParameter.ContainsVariable())
                return new Constant(this.Evaluate(null));
 
            return this;
        }
        public FunctionLog(IExpressionNode firstParam, IExpressionNode secondParam)
        {
            this.FirstParameter = firstParam;
            this.SecondParameter = secondParam;
        }
        public bool ContainsVariable()
        {
            return FirstParameter.ContainsVariable() || SecondParameter.ContainsVariable();
        }

        public IExpressionNode DeepCopy()
        {
            return new FunctionLog(FirstParameter.DeepCopy(), SecondParameter.DeepCopy());
        }

        public IExpressionNode Derivate()
        {
            return new OperationDivision(
                new FunctionLn(SecondParameter),
                new FunctionLn(FirstParameter)).Derivate();
        }

        public double Evaluate(Dictionary<string, double> input)
        {
            return Math.Log(SecondParameter.Evaluate(input)) / Math.Log(FirstParameter.Evaluate(input));
        }

        public string GetInFixNotation()
        {
            return $"log({FirstParameter.GetInFixNotation()}, {SecondParameter.GetInFixNotation()}) ";
        }

        public string GetPostFixNotation()
        {
            return $"{FirstParameter.GetInFixNotation()} {SecondParameter.GetInFixNotation()} log ";
        }

        public string GetPreFixNotation()
        {
            return $"log {FirstParameter.GetInFixNotation()} {SecondParameter.GetInFixNotation()} ";
        }        
    }
}
