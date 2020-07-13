using System;
using System.Text.RegularExpressions;

namespace RestTest.NewJsonHelper
{
    public class JsonString : JsonAttribute, IEquatable<JsonString>, IEquatable<JsonLong>
    {
        public string Value { get; set; }
        public bool _isRegex;
        public string _regexPattern;

        private const string ANY = "${ANY}";
        private const string NUMBER = "${NUMBER}";

        public JsonString(string value)
            : this(string.Empty, value)
        {
        }

        public JsonString(string key, string value)
        {
            Key = key;
            Value = value;

            Match regexResultAttrBody = Regex.Match(value, @"\${Regex:(.*)}");
            _isRegex = regexResultAttrBody.Success;
            _regexPattern = _isRegex ? regexResultAttrBody.Groups[1].Value.Trim() : string.Empty;
        }

        public override object GetValue()
        {
            return Value;
        }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(Key) ? $"\"{Value}\"" : $"\"{Key}\": \"{Value}\"";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as JsonString) || Equals(obj as JsonLong);
        }

        public bool Equals(JsonString other)
        {
            if (other is null) return false;
            if (Value == ANY || other.Value == ANY) return true;
            if ((Value == NUMBER || other.Value == NUMBER) && (IsNumeric(Value) || IsNumeric(other.Value))) return true;

            if (_isRegex && Regex.Match(other.Value, _regexPattern).Length > 0 ||
                other._isRegex && Regex.Match(Value, other._regexPattern).Length > 0) return true;

            return Value == other.Value;
        }

        private bool IsNumeric(string value)
        {
            return long.TryParse(value, out var _);
        }

        public bool Equals(JsonLong other)
        {
            if (other is null) return false;
            if (_isRegex && Regex.Match(other.Value.ToString(), _regexPattern).Length > 0) return true;
            return Value == NUMBER || Value == ANY;
        }
    }
}
