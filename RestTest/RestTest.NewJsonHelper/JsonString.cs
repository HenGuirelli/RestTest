namespace RestTest.NewJsonHelper
{
    public class JsonString : JsonAttribute
    {
        public string Value { get; set; }

        public JsonString(string key, string value)
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
