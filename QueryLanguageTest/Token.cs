using System.Collections.Generic;

namespace QueryLanguageTest
{
    public class Token
    {
        // Order is important!
        public static Dictionary<string, Token> AllTokens = new Dictionary<string, Token>
        {
            ["<="] = new Token("<=", 50, "<="),
            [">="] = new Token(">=", 50, ">="),
            ["<>"] = new Token("<>", 50, "<>"),
            ["<"] = new Token("<", 50, "<"),
            [">"] = new Token(">", 50, ">"),
            ["="] = new Token("=", 50, "="),
            ["AND"] = new Token("AND", 40, " AND "),
            ["OR"] = new Token("OR", 30, " OR "),
            ["("] = new Token("(", 60, "\\(", isSpecial: true),
            [")"] = new Token(")", 60, "\\)", isSpecial: true)
        };

        public static bool IsNotToken(string value)
        {
            return !AllTokens.TryGetValue(value.ToUpperInvariant(), out var _);
        }

        public static bool IsNonSpecialToken(string value)
        {
            return GetNonSpecialToken(value) != null;
        }

        public static Token GetNonSpecialToken(string value)
        {
            if (AllTokens.TryGetValue(value.ToUpperInvariant(), out var token))
            {
                if (token.IsSpecial)
                {
                    return null;
                }

                return token;
            }

            return null;
        }

        public Token(string value, int precedence, string regex, bool isSpecial = false)
        {
            Value = value;
            Precedence = precedence;
            Regex = regex;
            IsSpecial = isSpecial;
        }

        public string Value { get; }
        public int Precedence { get; }
        public string Regex { get; }

        public bool IsSpecial { get; }
    }
}
