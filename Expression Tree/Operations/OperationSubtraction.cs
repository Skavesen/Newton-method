//using DerivationSimple.Drawer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VP_LW_4.Expression_Tree
{
    public class OperationSubtraction : IOperation
    {
        public IExpressionNode LeftOperand { get; set; }
        public IExpressionNode RightOperand { get; set; }
        public OperationSubtraction(IExpressionNode leftOperand, IExpressionNode rightOperand)
        {
            this.LeftOperand = leftOperand;
            this.RightOperand = rightOperand;
        }
        public OperationSubtraction() { }
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
                    return RightOperand.DeepCopy();
            }
            else if (LeftOperand.ContainsVariable() && !RightOperand.ContainsVariable())
            {
                if (RightOperand.Evaluate(null) == 0)
                    return LeftOperand.DeepCopy();
            }

            return this;
        }
        public double Evaluate(Dictionary<string, double> input) => LeftOperand.Evaluate(input) - RightOperand.Evaluate(input);
        IExpressionNode IExpressionNode.Derivate() => new OperationSubtraction(LeftOperand.Derivate(), RightOperand.Derivate());
        public IExpressionNode DeepCopy() => new OperationSubtraction(LeftOperand.DeepCopy(), RightOperand.DeepCopy());
        public string GetPostFixNotation()=> $"{LeftOperand.GetPostFixNotation()} {RightOperand.GetPostFixNotation()} - ";
        public string GetPreFixNotation()=> $"- {LeftOperand.GetPreFixNotation()} {RightOperand.GetPreFixNotation()} ";
        public string GetInFixNotation()=> $"({LeftOperand.GetInFixNotation()} - {RightOperand.GetInFixNotation()}) ";
        public bool ContainsVariable() => LeftOperand.ContainsVariable() || RightOperand.ContainsVariable();

    }
}
