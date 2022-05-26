using VP_LW_4.Expression_Tree.Functions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VP_LW_4.Expression_Tree.Operations
{
    class OperationPower : IOperation
    {
        public IExpressionNode LeftOperand { get; set; }
        public IExpressionNode RightOperand { get; set; }

        public OperationPower(IExpressionNode leftOperand, IExpressionNode rightOperand)
        {
            this.LeftOperand = leftOperand;
            this.RightOperand = rightOperand;
        }
        public OperationPower() { }
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
                    return new Constant(1);
            }
            else if (LeftOperand.ContainsVariable() && !RightOperand.ContainsVariable())
            {
                if (RightOperand.Evaluate(null) == 0)
                    return new Constant(1);
                if (RightOperand.Evaluate(null) == 1)
                    return LeftOperand.DeepCopy();
            }

            return this;
        }
        public IExpressionNode DeepCopy() => new OperationPower(LeftOperand.DeepCopy(), RightOperand.DeepCopy());

        public IExpressionNode Derivate()
        {
            if (LeftOperand.ContainsVariable() && RightOperand.ContainsVariable())
            {
                return new OperationMultiplication(
                    this.DeepCopy(),
                    new OperationMultiplication(
                        new FunctionLn(LeftOperand.DeepCopy()),
                        RightOperand.DeepCopy()).Derivate());
            }
            else if(LeftOperand.ContainsVariable())
            {
                return new OperationMultiplication(
                    new OperationMultiplication(
                        RightOperand.DeepCopy(),
                        new OperationPower(
                            LeftOperand.DeepCopy(),
                            new OperationSubtraction(RightOperand.DeepCopy(), new Constant(1))
                            )
                        ),
                    LeftOperand.Derivate()
                    );
            }
            else if(RightOperand.ContainsVariable())
            {
                return new OperationMultiplication(
                    new OperationMultiplication(
                        this.DeepCopy(),
                        new FunctionLn(LeftOperand.DeepCopy())
                        ),
                    RightOperand.Derivate()
                    );
            }
            else
            {
                return new Constant(0);
            }
        }

        public double Evaluate(Dictionary<string, double> input)
        {
            var leftValue = LeftOperand.Evaluate(input);
            var rightValue = Math.Min(100, RightOperand.Evaluate(input));
            return Complex.Pow(leftValue, rightValue).Real;
        }

        public string GetInFixNotation() => $"({LeftOperand.GetInFixNotation()} ^ {RightOperand.GetInFixNotation()}) ";
        public string GetPostFixNotation() => $"{LeftOperand.GetInFixNotation()} {RightOperand.GetInFixNotation()} ^ ";
        public string GetPreFixNotation() => $"^ {LeftOperand.GetInFixNotation()} {RightOperand.GetInFixNotation()} ";
        public bool ContainsVariable() => LeftOperand.ContainsVariable() || RightOperand.ContainsVariable();        
    }
}
