//using DerivationSimple.Drawer;
using VP_LW_4.Expression_Tree.Operations;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace VP_LW_4.Expression_Tree.Functions
{
    class FunctionCtg : IFunctionSingular
    {
        public IExpressionNode Parameter { get; set; }
        public FunctionCtg(IExpressionNode parameter)
        {
            this.Parameter = parameter;
        }
        public FunctionCtg() { }
        public IExpressionNode Simplify()
        {
            Parameter = Parameter.Simplify();
            if (!Parameter.ContainsVariable())
                return new Constant(this.Evaluate(null));
            return this;
        }
        public IExpressionNode DeepCopy() => new FunctionCtg(Parameter.DeepCopy());

        public string GetPostFixNotation() => $"{Parameter.GetPostFixNotation()} ctg ";

        public string GetPreFixNotation() => $"ctg {Parameter.GetPreFixNotation()} ";

        public string GetInFixNotation() => $"ctg({Parameter.GetInFixNotation()}) ";

        public bool ContainsVariable() => Parameter.ContainsVariable();

        public IExpressionNode Derivate()
        {
            return new OperationMultiplication(
                new OperationMultiplication(
                    new Constant(-1), 
                    new OperationPower(
                        new OperationDivision(
                            new Constant(1),
                            new FunctionSin(Parameter.DeepCopy())),
                        new Constant(2))),
                Parameter.Derivate());
        }

        public double Evaluate(Dictionary<string, double> input)
        {
            return 1/Math.Tan(Parameter.Evaluate(input));
        }
    }
}
