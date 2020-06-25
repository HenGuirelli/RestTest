using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestTest.JsonHelper
{
    public class JsonValueObject : JsonValue
    {
        public override JsonValue this[string key] => JsonValue.Create((_typedValue as JObject)[key].ToString());
        public override bool IsObject => true;
        protected JObject _json;

        public JsonValueObject(string value)
        {
            _json = JsonConvert.DeserializeObject(value) as JObject;
            _typedValue = _json;
        }
    }
}
