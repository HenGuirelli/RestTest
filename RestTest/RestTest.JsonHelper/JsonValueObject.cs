using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestTest.JsonHelper
{
    internal class JsonValueObject : JsonValue
    {
        public JsonValueObject(string value)
        {
            _typedValue = JsonConvert.DeserializeObject(value) as JObject;
        }

        public override JsonValue this[string key] => JsonValue.Create((_typedValue as JObject)[key].ToString());
    }
}
