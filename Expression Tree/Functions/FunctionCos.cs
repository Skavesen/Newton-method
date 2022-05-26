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
    class FunctionCos : IFunctionSingular
    {
        public IExpressionNode Parameter { get; set; }
        public FunctionCos(IExpressionNode parameter)
        {
            this.Parameter = parameter;
        }
        public FunctionCos() { }
        public IExpressionNode Simplify()
        {
            Parameter = Parameter.Simplify();
            if (!Parameter.ContainsVariable())
                return new Constant(this.Evaluate(null));
            return this;
        }
        public IExpressionNode DeepCopy() => new FunctionCos(Parameter.DeepCopy());

        public string GetPostFixNotation() => $"{Parameter.GetPostFixNotation()} cos ";

        public string GetPreFixNotation() => $"cos {Parameter.GetPreFixNotation()} ";

        public string GetInFixNotation() => $"cos({Parameter.GetInFixNotation()}) ";

        public bool ContainsVariable() => Parameter.ContainsVariable();

        public IExpressionNode Derivate()
        {
            return new OperationMultiplication(
                new OperationMultiplication(
                    new FunctionSin(Parameter.DeepCopy()), 
                    new Constant(-1)),
                Parameter.Derivate());
        }

        public double Evaluate(Dictionary<string, double> input)
        {
            return Math.Cos(Parameter.Evaluate(input));
        }
    }
}
