using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace QueryLanguageTest
{
    public class Tokenizer
    {
        private readonly List<Regex> TokenRegexs = Token.AllTokens.Select(x => new Regex(x.Value.Regex, RegexOptions.IgnoreCase)).ToList();

        public string[] Tokenize(string expression)
        {
            var tokens = new List<string>();
            int currentPosition = 0;
            int? notOperatorPosition = null;

            do
            {
                var (token, position) = FindMatch(expression, currentPosition);
                if (!string.IsNullOrWhiteSpace(token))
                {
                    if (notOperatorPosition.HasValue)
                    {
                        var notOperator = expression.Substring(notOperatorPosition.Value, currentPosition - notOperatorPosition.Value);
                        tokens.Add(notOperator);
                        notOperatorPosition = null;
                    }

                    tokens.Add(token);
                    currentPosition = position;
                }
                else
                {
                    notOperatorPosition ??= currentPosition; // if (!notOperatorPosition.HasValue) notOperatorPosition = currentPosition
                    currentPosition += 1;
                }
            } while (currentPosition < expression.Length);

            if (notOperatorPosition.HasValue)
            {
                tokens.Add(expression.Substring(notOperatorPosition.Value, expression.Length - notOperatorPosition.Value));
            }

            return tokens
                .Select(x => x.Replace(" ", ""))
                .Select(x => Token.IsNotToken(x) ?  x : x.ToUpperInvariant())
                .ToArray();
        }

        private (string, int) FindMatch(string text, int position)
        {
            foreach (var tokenRegex in TokenRegexs)
            {
                var match = tokenRegex.Match(text, position);
                if (match.Success && match.Index == position)
                    return (match.Value, match.Index + match.Length);
            }

            return ("", position);
        }
    }
}
