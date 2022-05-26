using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VP_LW_4.Expression_Tree.Functions
{
    public interface IFunctionSingular : IExpressionNode
    {
        IExpressionNode Parameter { get; set; }
    }
}
