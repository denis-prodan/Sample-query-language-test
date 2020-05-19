using System;
using System.Linq;

namespace QueryLanguageTest
{
    class Program
    {
        private static ParserDijkstra parser = new ParserDijkstra();
        private static Tokenizer tokenizer = new Tokenizer();

        static void Main(string[] args)
        {
            //TestExpression("x<4 OR y>= 3 AND z =5");
            //TestExpression("(x< 4 OR y >=3 ) AnD z = 5");
            //TestExpression("( x < 4 oR y>=3) AND z = 5 OR k> 7");

            TestExpression("( TextField = \"test\"     oR IntField>=3) AnD IntField2 = 5 Or FloatField> 7.23");

            Console.ReadKey();
        }

        private static void TestExpression(string expression)
        {
            Console.WriteLine($"Expression: {expression}");

            var tokens = tokenizer.Tokenize(expression);
            //Console.WriteLine("Tokens:");
            //foreach (var token in tokens)
            //{
            //    Console.WriteLine(token);
            //}

            var postfix = parser.ConvertFromInfixToPostfix(tokens);
            var prefix = postfix.Reverse().ToArray();

            var (tree, _) = parser.ConvertToTree(prefix);
            Console.WriteLine("Expression tree:");
            Console.WriteLine(tree);
        }
    }
}
