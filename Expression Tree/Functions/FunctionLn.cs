//using DerivationSimple.Drawer;
using VP_LW_4.Expression_Tree.Operations;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace VP_LW_4.Expression_Tree.Functions
{
    class FunctionLn : IFunctionSingular
    {
        public IExpressionNode Parameter { get; set; }
        public FunctionLn(IExpressionNode param)
        {
            this.Parameter = param;
        }
        public FunctionLn() { }
        public IExpressionNode Simplify()
        {
            Parameter = Parameter.Simplify();
            if (!Parameter.ContainsVariable())
                return new Constant(this.Evaluate(null));
            return this;
        }
        public bool ContainsVariable()
        {
            return Parameter.ContainsVariable();
        }

        public IExpressionNode DeepCopy()
        {
            return new FunctionLn(Parameter.DeepCopy());
        }

        public IExpressionNode Derivate()
        {
            return new OperationDivision(
                Parameter.Derivate(),
                Parameter.DeepCopy());
        }

        public double Evaluate(Dictionary<string, double> input)
        {
            return Math.Log(Parameter.Evaluate(input));
        }

        public string GetInFixNotation()
        {
            return $"ln({Parameter.GetInFixNotation()}) ";
        }

        public string GetPostFixNotation()
        {
            return $"{Parameter.GetInFixNotation()} ln ";
        }

        public string GetPreFixNotation()
        {
            return $"ln {Parameter.GetInFixNotation()} ";
        }
    }
}
