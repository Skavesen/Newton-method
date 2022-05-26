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
    class FunctionArcTg : IFunctionSingular
    {
        public IExpressionNode Parameter { get; set; }
        public FunctionArcTg(IExpressionNode parameter)
        {
            this.Parameter = parameter;
        }
        public FunctionArcTg() { }
        public IExpressionNode Simplify()
        {
            Parameter = Parameter.Simplify();
            if (!Parameter.ContainsVariable())
                return new Constant(this.Evaluate(null));
            return this;
        }
        public IExpressionNode DeepCopy() => new FunctionArcTg(Parameter.DeepCopy());

        public string GetPostFixNotation() => $"{Parameter.GetPostFixNotation()} arctg ";

        public string GetPreFixNotation() => $"arctg {Parameter.GetPreFixNotation()} ";

        public string GetInFixNotation() => $"arctg({Parameter.GetInFixNotation()}) ";

        public bool ContainsVariable() => Parameter.ContainsVariable();

        public IExpressionNode Derivate()
        {
            return new OperationMultiplication(
                    new OperationDivision(
                        new Constant(1),
                        new OperationAdd(
                            new OperationPower(
                                Parameter.DeepCopy(),
                                new Constant(2)),
                            new Constant(1))),
                    Parameter.Derivate());
        }

        public double Evaluate(Dictionary<string, double> input)
        {
            return Math.Atan(Parameter.Evaluate(input));
        }
    }
}
