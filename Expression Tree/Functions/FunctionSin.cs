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
    class FunctionSin : IFunctionSingular
    {
        public IExpressionNode Parameter { get; set; }
        public FunctionSin(IExpressionNode parameter)
        {
            this.Parameter = parameter;
        }
        public FunctionSin() { }
        public IExpressionNode Simplify()
        {
            Parameter = Parameter.Simplify();
            if (!Parameter.ContainsVariable())
                return new Constant(this.Evaluate(null));
            return this;
        }
        public IExpressionNode DeepCopy() => new FunctionSin(Parameter.DeepCopy());

        public string GetPostFixNotation() => $"{Parameter.GetPostFixNotation()} sin ";

        public string GetPreFixNotation() => $"sin {Parameter.GetPreFixNotation()} ";

        public string GetInFixNotation() => $"sin({Parameter.GetInFixNotation()}) ";

        public bool ContainsVariable() => Parameter.ContainsVariable();

        public IExpressionNode Derivate()
        {
            return new OperationMultiplication(new FunctionCos(Parameter.DeepCopy()), Parameter.Derivate());
        }

        public double Evaluate(Dictionary<string, double> input)
        {
            return Math.Sin(Parameter.Evaluate(input));
        }
    }
}
