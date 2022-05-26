//using DerivationSimple.Drawer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VP_LW_4.Expression_Tree
{
    public class OperationAdd : IOperation
    {
        public IExpressionNode LeftOperand { get; set; }
        public IExpressionNode RightOperand { get; set; }
        public OperationAdd(IExpressionNode leftOperand, IExpressionNode rightOperand)
        {
            this.LeftOperand = leftOperand.Simplify();
            this.RightOperand = rightOperand.Simplify();
        }
        public OperationAdd() { }

        public double Evaluate(Dictionary<string, double> input)
        {
            return LeftOperand.Evaluate(input) + RightOperand.Evaluate(input);
        }
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
        IExpressionNode IExpressionNode.Derivate()
        {
            return new OperationAdd(LeftOperand.Derivate(), RightOperand.Derivate());
        }
        public string GetPostFixNotation() => $"{LeftOperand.GetPostFixNotation()} {RightOperand.GetPostFixNotation()} + ";

        public string GetPreFixNotation()=> $"+ {LeftOperand.GetPreFixNotation()} {RightOperand.GetPreFixNotation()} ";

        public string GetInFixNotation() => $"({LeftOperand.GetInFixNotation()} + {RightOperand.GetInFixNotation()}) ";

        public IExpressionNode DeepCopy() => new OperationAdd(LeftOperand.DeepCopy(), RightOperand.DeepCopy());
        public bool ContainsVariable() => LeftOperand.ContainsVariable() || RightOperand.ContainsVariable();

    }
}
