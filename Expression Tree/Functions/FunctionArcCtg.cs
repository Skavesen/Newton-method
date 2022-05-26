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
    class FunctionArcCtg : IFunctionSingular
    {
        public IExpressionNode Parameter { get; set; }
        public FunctionArcCtg(IExpressionNode parameter)
        {
            this.Parameter = parameter;
        }
        public FunctionArcCtg() { }
        public IExpressionNode Simplify()
        {
            Parameter = Parameter.Simplify();
            if (!Parameter.ContainsVariable())
                return new Constant(this.Evaluate(null));
            return this;
        }
        public IExpressionNode DeepCopy() => new FunctionArcCtg(Parameter.DeepCopy());

        public string GetPostFixNotation() => $"{Parameter.GetPostFixNotation()} arcctg ";

        public string GetPreFixNotation() => $"arcctg {Parameter.GetPreFixNotation()} ";

        public string GetInFixNotation() => $"arcctg({Parameter.GetInFixNotation()}) ";

        public bool ContainsVariable() => Parameter.ContainsVariable();

        public IExpressionNode Derivate()
        {
            return new OperationMultiplication(
                    new OperationMultiplication(
                        new Constant(-1),
                        new OperationDivision(
                            new Constant(1),
                            new OperationAdd(
                                new OperationPower(
                                    Parameter.DeepCopy(),
                                    new Constant(2)),
                                new Constant(1)))),
                    Parameter.Derivate());
        }

        public double Evaluate(Dictionary<string, double> input)
        {
            return Math.Atan2(1, Parameter.Evaluate(input));
        }
    }
}
