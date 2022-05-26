//using DerivationSimple.Drawer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VP_LW_4.Expression_Tree
{
    class Variable : IExpressionNode
    {
        private readonly string variableName;
        
        public Variable(string name)
        {
            variableName = name;
        }
        public IExpressionNode Simplify()
        {
            return this;
        }
        public double Evaluate(Dictionary<string, double> input)
        {
            if(input.Keys.Contains(variableName))
            {
                return input[variableName];
            }
            else
            {
                throw new Exception($"Variable name could not be found in dictionary:{variableName}");
            }
        }

        public IExpressionNode Derivate() => new Constant(1);

        public IExpressionNode DeepCopy() => new Variable(this.variableName);

        public string GetPostFixNotation()
            => this.variableName;

        public string GetPreFixNotation() 
            => this.variableName;

        public string GetInFixNotation() 
            => variableName;

        public bool ContainsVariable()
            => true;

    }
}
