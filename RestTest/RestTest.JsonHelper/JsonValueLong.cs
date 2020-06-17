namespace RestTest.JsonHelper
{
    internal class JsonValueLong : JsonValue
    {
        public JsonValueLong(long value)
        {
            _typedValue = value;
        }

        public override JsonValue this[string key] { get => throw new System.NotImplementedException(); }
    }
}
