namespace RestTest.JsonHelper
{
    internal class JsonValueString : JsonValue
    {
        private readonly string _value;
        public override JsonValue this[string key] { get => throw new System.NotImplementedException(); }
        public override bool IsObject => false;

        public JsonValueString(string value)
        {
            _typedValue = value;
            _value = value;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;

            if (obj is JsonValueString) return _value == ((JsonValueString)obj)._value;

            return false;
        }
    }
}
