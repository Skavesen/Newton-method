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
    class FunctionTg : IFunctionSingular
    {
        public IExpressionNode Parameter { get; set; }
        public FunctionTg(IExpressionNode parameter)
        {
            this.Parameter = parameter;
        }
        public FunctionTg() { }
        public IExpressionNode Simplify()
        {
            Parameter = Parameter.Simplify();
            if (!Parameter.ContainsVariable())
                return new Constant(this.Evaluate(null));
            return this;
        }
        public IExpressionNode DeepCopy() => new FunctionTg(Parameter.DeepCopy());

        public string GetPostFixNotation() => $"{Parameter.GetPostFixNotation()} tg ";

        public string GetPreFixNotation() => $"tg {Parameter.GetPreFixNotation()} ";

        public string GetInFixNotation() => $"tg({Parameter.GetInFixNotation()}) ";

        public bool ContainsVariable() => Parameter.ContainsVariable();

        public IExpressionNode Derivate()
        {
            return new OperationMultiplication(
                new OperationMultiplication(
                    new Constant(1),
                    new OperationDivision(
                        new Constant(1),
                        new FunctionCos(Parameter.DeepCopy()))),
                Parameter.Derivate());
        }

        public double Evaluate(Dictionary<string, double> input)
        {
            return Math.Tan(Parameter.Evaluate(input));
        }
    }
}
