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
        public bool HasValue { get; private set; }

        public static Json Empty => new Json("{}");

        public Json(string json)
        {
            HasValue = !string.IsNullOrWhiteSpace(json);
            json = string.IsNullOrWhiteSpace(json) ? "{}" : json;
            _json = JsonConvert.DeserializeObject(json) as JObject;
        }

        public IEnumerable<string> Keys => _json.Properties().Select(x => x.Name.ToString());
        public IEnumerable<object> Values => _json.Properties().Select(x => x.Value);

        public override string ToString()
        {
            return _json.ToString();
        }

        public bool Compare(Json other)
        {
            return Compare(other.ToString());
        }

        public bool Compare(string other)
        {
            return JToken.DeepEquals(_json, JsonConvert.DeserializeObject(other) as JObject);
        }
    }
}
