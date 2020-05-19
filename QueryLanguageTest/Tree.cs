using System.Text;

namespace QueryLanguageTest
{
    public class Tree
    {
        public string Value;
        public Tree Left;
        public Tree Right;

        public override string ToString()
        {
            StringBuilder buffer = new StringBuilder();
            print(buffer, "", "");
            return buffer.ToString();
        }

        private void print(StringBuilder buffer, string prefix, string childrenPrefix)
        {
            buffer.Append(prefix);
            buffer.Append(Value);
            buffer.Append('\n');

            Left?.print(buffer, childrenPrefix + "├── ", childrenPrefix + "│   ");
            Right?.print(buffer, childrenPrefix + "└── ", childrenPrefix + "    ");
        }
    }
}