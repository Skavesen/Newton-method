using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VP_LW_4.Expression_Tree
{
    public interface IOperation : IExpressionNode
    {
        IExpressionNode LeftOperand { get; set; }
        IExpressionNode RightOperand { get; set; }
    }
}
