using VP_LW_4.Expression_Tree;
using VP_LW_4.Expression_Tree.Functions;
using VP_LW_4.Expression_Tree.Operations;
using System;
using System.Collections.Generic;

namespace VP_LW_4.Parser
{
    public static class ParseHelper
    {
        public struct OperatorInfo
        {
            public int precedence;
            public bool isLeftAssociative;
            public int numberOfParameters;
            public Type nodeClass;
            public OperatorInfo(int precedence, bool isLeftAssociative, int numberOfParams, Type nodeClass)
            {
                this.precedence = precedence;
                this.isLeftAssociative = isLeftAssociative;
                this.numberOfParameters = numberOfParams;
                this.nodeClass = nodeClass;
            }
        };
        private static readonly List<string> allowedOperators = new List<string>() {
            "+", "-", "*", "/", "^"
        };

        private static readonly List<string> allowedFunctions = new List<string>() {
            "sin", "cos", "tg", "ctg", "arcsin", "arccos", "arctg", "arcctg", "ln", "log"
        };
        public static readonly Dictionary<string, OperatorInfo> tokenInformation = new Dictionary<string, OperatorInfo>() {
            { "+", new OperatorInfo(2, true, 2, typeof(OperationAdd)) },
            { "-", new OperatorInfo(2, true, 2, typeof(OperationSubtraction)) },
            { "*", new OperatorInfo(3, true, 2, typeof(OperationMultiplication)) },
            { "/", new OperatorInfo(3, true, 2, typeof(OperationDivision)) },
            { "^", new OperatorInfo(4, false, 2, typeof(OperationPower)) },
            { "sin", new OperatorInfo(4, true, 1, typeof(FunctionSin)) },
            { "cos", new OperatorInfo(4, true, 1, typeof(FunctionCos)) },
            { "tg", new OperatorInfo(4, true, 1, typeof(FunctionTg)) },
            { "ctg", new OperatorInfo(4, true, 1, typeof(FunctionCtg)) },
            { "arcsin", new OperatorInfo(4, true, 1, typeof(FunctionArcSin)) },
            { "arccos", new OperatorInfo(4, true, 1, typeof(FunctionArcCos)) },
            { "arctg", new OperatorInfo(4, true, 1, typeof(FunctionArcTg)) },
            { "arcctg", new OperatorInfo(4, true, 1, typeof(FunctionArcCtg)) },
            { "ln", new OperatorInfo(4, true, 1, typeof(FunctionLn)) },
            { "log", new OperatorInfo(4, true, 2, typeof(FunctionLog)) },
        };

        public static bool IsDigit(this char c) => c >= '0' && c <= '9';
        public static bool IsLetter(this char c) => c >= 'a' && c <= 'z';
        public static bool IsNumber(this string s) => double.TryParse(s, out _);

        public static bool IsOperator(this string s) => allowedOperators.Contains(s);

        public static bool IsFunctionCall(this string s) => allowedFunctions.Contains(s);

        public static bool IsVariable(this string s) => s == "x";

    }
}
