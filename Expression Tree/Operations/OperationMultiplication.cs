//using DerivationSimple.Drawer;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace VP_LW_4.Expression_Tree.Operations
{
    class OperationMultiplication : IOperation
    {
        public IExpressionNode LeftOperand { get; set; }
        public IExpressionNode RightOperand { get; set; }

        public OperationMultiplication(IExpressionNode leftOperand, IExpressionNode rightOperand)
        {
            this.LeftOperand = leftOperand;
            this.RightOperand = rightOperand;
        }
        public OperationMultiplication() { }
        public IExpressionNode Simplify()
        {
            LeftOperand = LeftOperand.Simplify();
            RightOperand = RightOperand.Simplify();
            if (!LeftOperand.ContainsVariable() && !RightOperand.ContainsVariable())
            {
                return new Constant(this.Evaluate(null));
            }
            else if (!LeftOperand.ContainsVariable() && RightOperand.ContainsVariable())
            {
                if (LeftOperand.Evaluate(null) == 0)
                    return new Constant(0);
                if (LeftOperand.Evaluate(null) == 1)
                    return RightOperand.DeepCopy();
            }
            else if (LeftOperand.ContainsVariable() && !RightOperand.ContainsVariable())
            {
                if (RightOperand.Evaluate(null) == 0)
                    return new Constant(0);
                if (RightOperand.Evaluate(null) == 1)
                    return LeftOperand.DeepCopy();
            }

            return this;
        }
        public IExpressionNode Derivate()
        {
            var leftDerivative = LeftOperand.Derivate();
            var rightDerivative = RightOperand.Derivate();
            return new OperationAdd(
                new OperationMultiplication(
                    leftDerivative,
                    RightOperand.DeepCopy()),
                new OperationMultiplication(
                    LeftOperand.DeepCopy(),
                    rightDerivative));
        }

        public double Evaluate(Dictionary<string, double> input) => LeftOperand.Evaluate(input) * RightOperand.Evaluate(input);
        public IExpressionNode DeepCopy() => new OperationMultiplication(LeftOperand.DeepCopy(), RightOperand.DeepCopy());
        public string GetPostFixNotation()=> $"{LeftOperand.GetPostFixNotation()} {RightOperand.GetPostFixNotation()} * ";
        public string GetPreFixNotation()=> $"* {LeftOperand.GetPreFixNotation()} {RightOperand.GetPreFixNotation()} ";
        public string GetInFixNotation()=> $"({LeftOperand.GetInFixNotation()} * {RightOperand.GetInFixNotation()}) ";
        public bool ContainsVariable() => LeftOperand.ContainsVariable() || RightOperand.ContainsVariable();

    }
}
