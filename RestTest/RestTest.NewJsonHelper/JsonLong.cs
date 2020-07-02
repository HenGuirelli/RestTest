namespace RestTest.NewJsonHelper
{
    public class JsonLong : JsonAttribute
    {
        public long Value { get; set; }

        public JsonLong(long value)
            : this (string.Empty, value)
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
            return Value.ToString();
        }
    }
}
