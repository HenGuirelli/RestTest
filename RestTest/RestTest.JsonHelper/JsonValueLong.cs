namespace RestTest.JsonHelper
{
    internal class JsonValueLong : JsonValue
    {
        private readonly long _value;

        public JsonValueLong(long value)
        {
            _typedValue = value;
            _value = value;
        }

        public override JsonValue this[string key] { get => throw new System.NotImplementedException(); }

        public override bool IsObject => false;

        public override bool Equals(object obj)
        {
            if (obj is null) return false;

            if (obj is JsonValueLong) return _value == ((JsonValueLong)obj)._value;

            return false;
        }
    }
}
