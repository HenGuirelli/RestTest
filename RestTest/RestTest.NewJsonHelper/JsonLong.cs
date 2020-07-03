using System;

namespace RestTest.NewJsonHelper
{
    public class JsonLong : JsonAttribute, IEquatable<JsonString>, IEquatable<JsonLong>
    {
        public long Value { get; set; }

        public JsonLong(long value)
            : this(string.Empty, value)
        {
        }

        public JsonLong(string key, long value)
        {
            Key = key;
            Value = value;
        }

        public override object GetValue()
        {
            return Value;
        }

        public override string ToString()
        {
            return string.IsNullOrWhiteSpace(Key) ? Value.ToString() : $"\"{Key}\": {Value}";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as JsonString) || Equals(obj as JsonLong);
        }

        public bool Equals(JsonString other)
        {
            if (other is null) return false;
            return other.Equals(this);
        }

        public bool Equals(JsonLong other)
        {
            if (other is null) return false;
            return Value == other.Value;
        }
    }
}
