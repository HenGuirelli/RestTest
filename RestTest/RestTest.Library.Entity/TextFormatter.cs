using System.Text;

namespace RestTest.Library.Entity
{
    public class TextFormatter
    {
        private readonly StringBuilder _text;
        public string Value => _text.ToString();

        private bool _hasDoubleSpaces => Value.Contains("  ");

        private TextFormatter(StringBuilder text)
        {
            _text = text;
        }
        
        public TextFormatter(string text)
            : this(new StringBuilder(text))
        {
        }

        public TextFormatter RemoveEspecialCharacters()
        {
            return new TextFormatter(_text.Replace("\r", "").Replace("\t", " "));
        }

        public TextFormatter RemoveMultipleSpaces()
        {
            TextFormatter result = new TextFormatter(_text.Replace("  ", " "));
            for (; result._hasDoubleSpaces; result = result.RemoveMultipleSpaces()) ;
            return result;
        }

        public TextFormatter Trim()
        {
            return new TextFormatter(_text.ToString().Trim());
        }

        public TextFormatter RemoveNewLine()
        {
            return new TextFormatter(_text.Replace("\n", " "));
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
