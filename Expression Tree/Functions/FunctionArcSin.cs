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
    class FunctionArcSin : IFunctionSingular
    {
        public IExpressionNode Parameter { get; set; }
        public FunctionArcSin(IExpressionNode parameter)
        {
            this.Parameter = parameter;
        }
        public FunctionArcSin() { }
        public IExpressionNode Simplify()
        {
            Parameter = Parameter.Simplify();
            if (!Parameter.ContainsVariable())
                return new Constant(this.Evaluate(null));
            return this;
        }
        public IExpressionNode DeepCopy() => new FunctionArcSin(Parameter.DeepCopy());

        public string GetPostFixNotation() => $"{Parameter.GetPostFixNotation()} arcsin ";

        public string GetPreFixNotation() => $"arcsin {Parameter.GetPreFixNotation()} ";

        public string GetInFixNotation() => $"arcsin({Parameter.GetInFixNotation()}) ";

        public bool ContainsVariable() => Parameter.ContainsVariable();

        public IExpressionNode Derivate()
        {
            return new OperationMultiplication(
                new OperationDivision(
                    new Constant(1),
                    new OperationPower(
                        new OperationSubtraction(
                            new Constant(1),
                            new OperationPower(
                                Parameter.DeepCopy(),
                                new Constant(2))),
                        new Constant(1d/2d))),
                Parameter.Derivate());
        }

        public double Evaluate(Dictionary<string, double> input)
        {
            return Math.Asin(Parameter.Evaluate(input));
        }
    }
}
