namespace RestTest.JsonHelper
{
    internal class JsonValueString : JsonValue
    {
        public JsonValueString(string value)
        {
            _typedValue = value;
        }

        public override JsonValue this[string key] { get => throw new System.NotImplementedException(); }
    }
}
