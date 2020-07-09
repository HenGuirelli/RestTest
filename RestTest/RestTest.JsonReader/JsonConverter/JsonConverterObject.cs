using Newtonsoft.Json.Linq;
using RestTest.NewJsonHelper;
using System.Collections.Generic;

namespace RestTest.JsonReader
{
    public class JsonConverterObject : IJsonConverter
    {
        public JsonAttribute Convert(KeyValuePair<string, JToken> item)
        {
            var _converter = new JsonConverterManager();

            var jsonObj = new JsonObject(item.Key);
            foreach (JToken listItem in item.Value)
            {
                var jsonValue = _converter[listItem.Type].Convert(new KeyValuePair<string, JToken>(null, listItem));
                jsonObj.Add(jsonValue);
            }
            return jsonObj;
        }
    }
}