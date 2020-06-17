namespace RestTest.JsonHelper
{
    internal class JsonValueDouble : JsonValue
    {
        public JsonValueDouble(double value)
        {
            _typedValue = value;
        }

        public override JsonValue this[string key] { get => throw new System.NotImplementedException(); }
    }
}
