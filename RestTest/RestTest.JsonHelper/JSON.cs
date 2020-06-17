using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace RestTest.JsonHelper
{
    public class Json
    {
        private readonly JObject _json;
        public JsonValue this[string index] => JsonValue.Create(_json[index].ToString());

        public Json(string json)
        {
            json = string.IsNullOrWhiteSpace(json) ? "{}" : json;
            _json = JsonConvert.DeserializeObject(json) as JObject;
        }

        public IEnumerable<string> Keys => _json.Properties().Select(x => x.Name.ToString());
        public IEnumerable<object> Values => _json.Properties().Select(x => x.Value);

        public bool Compare(string other)
        {
            return JToken.DeepEquals(_json, JsonConvert.DeserializeObject(other) as JObject);
        }
    }
}
