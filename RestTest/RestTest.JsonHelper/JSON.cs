using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RestTest.JsonHelper
{
    public class Json
    {
        private readonly JObject _json;
        public JsonValue this[string index] => JsonValue.Create(_json[index].ToString());

        public Json(string json)
        {
            _json = JsonConvert.DeserializeObject(json) as JObject;
        }

        public bool Compare(string other)
        {
            return JToken.DeepEquals(_json, JsonConvert.DeserializeObject(other) as JObject);
        }
    }
}
