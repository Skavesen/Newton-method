using System.Collections.Generic;
using System.Drawing;

namespace VP_LW_4.Expression_Tree
{
    public interface IExpressionNode
    {
        double Evaluate(Dictionary<string, double> input);
        IExpressionNode Simplify();
        IExpressionNode Derivate();
        string ToString();
        IExpressionNode DeepCopy();
        string GetPostFixNotation();
        string GetPreFixNotation();
        string GetInFixNotation();
        bool ContainsVariable();
    }
}
