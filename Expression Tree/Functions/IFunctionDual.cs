using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VP_LW_4.Expression_Tree.Functions
{
    public interface IFunctionDual : IExpressionNode
    {
        IExpressionNode FirstParameter { get; set; }
        IExpressionNode SecondParameter { get; set; }

    }
}
