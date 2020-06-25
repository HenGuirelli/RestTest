using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestTest.JsonHelper
{
    internal class JsonValueList : JsonValue
    {
        public JsonValueList(string value)
        {
            _typedValue = JsonConvert.DeserializeObject(value) as JToken;
        }

        public override JsonValue this[string key] => throw new System.NotImplementedException();
        public override bool IsObject => false;
    }
}
