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
    class FunctionArcCos : IFunctionSingular
    {
        public IExpressionNode Parameter { get; set; }
        public FunctionArcCos(IExpressionNode parameter)
        {
            this.Parameter = parameter;
        }
        public FunctionArcCos() { }
        public IExpressionNode Simplify()
        {
            Parameter = Parameter.Simplify();
            if (!Parameter.ContainsVariable())
                return new Constant(this.Evaluate(null));
            return this;
        }
        public IExpressionNode DeepCopy() => new FunctionArcCos(Parameter.DeepCopy());

        public string GetPostFixNotation() => $"{Parameter.GetPostFixNotation()} arccos ";

        public string GetPreFixNotation() => $"arccos {Parameter.GetPreFixNotation()} ";

        public string GetInFixNotation() => $"arccos({Parameter.GetInFixNotation()}) ";

        public bool ContainsVariable() => Parameter.ContainsVariable();

        public IExpressionNode Derivate()
        {
            return new OperationMultiplication(
                new OperationMultiplication(
                    new Constant(-1),
                    new OperationPower(
                        new OperationPower(
                            new OperationSubtraction(
                                new Constant(1),
                                new OperationPower(
                                    Parameter.DeepCopy(),
                                    new Constant(2))),
                            new Constant(1d / 2d)),
                        new Constant(-1))
                ),
                Parameter.Derivate());
        }

        public double Evaluate(Dictionary<string, double> input)
        {
            return Math.Acos(Parameter.Evaluate(input));
        }
    }
}
