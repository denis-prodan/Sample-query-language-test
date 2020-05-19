using System.Collections.Generic;

namespace QueryLanguageTest
{
    public class ParserDijkstra
    {
        private const string LeftParenthesis = "(";
        private const string RightParenthesis = ")";

        public (Tree, int) ConvertToTree(string[] prefixForm, int currentPosition = 0)
        {
            var tree = new Tree
            {
                Value = prefixForm[currentPosition]
            };
            if (Token.IsNotToken(tree.Value.ToUpperInvariant()))
            {
                return (tree, currentPosition + 1);
            }
            var (rightTree, newPosition) = ConvertToTree(prefixForm, currentPosition + 1);
            var (leftTree, newPosition2) = ConvertToTree(prefixForm, newPosition);

            tree.Right = rightTree;
            tree.Left = leftTree;
            return (tree, newPosition2);
        }

        public string[] ConvertFromInfixToPostfix(string[] symbols)
        {
            var operatorStack = new Stack<string>();
            var outputQueue = new Queue<string>();

            foreach (string symbol in symbols)
            {
                if (Token.IsNotToken(symbol))
                {
                    outputQueue.Enqueue(symbol);
                    continue;
                }

                if (Token.IsNonSpecialToken(symbol))
                {
                    while (ShouldPutToQueue(operatorStack, symbol))
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                    }

                    operatorStack.Push(symbol);
                }

                if (symbol == LeftParenthesis)
                {
                    operatorStack.Push(symbol);
                    continue;
                }

                if (symbol == RightParenthesis)
                {
                    while (operatorStack.Peek() != LeftParenthesis)
                    {
                        outputQueue.Enqueue(operatorStack.Pop());
                    }

                    operatorStack.Pop(); // discard left parenthesis
                }
            }

            while (operatorStack.Count > 0)
            {
                outputQueue.Enqueue(operatorStack.Pop());
            }

            return outputQueue.ToArray();
        }

        private bool ShouldPutToQueue(Stack<string> operatorStack, string symbol)
        {
            if (operatorStack.Count == 0)
            {
                return false;
            }

            if (operatorStack.Peek() == LeftParenthesis)
            {
                return false;
            }

            if (HaveGreaterPrecedenceThan(symbol, operatorStack.Peek()))
            {
                return true;
            }

            return false;
        }

        private bool HaveGreaterPrecedenceThan(string op, string compareTo)
        {
            return Token.GetNonSpecialToken(compareTo).Precedence  >= Token.GetNonSpecialToken(op).Precedence;
        }
    }
}
