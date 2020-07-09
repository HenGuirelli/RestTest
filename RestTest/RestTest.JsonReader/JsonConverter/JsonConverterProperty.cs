using Newtonsoft.Json.Linq;
using RestTest.NewJsonHelper;
using System.Collections.Generic;

namespace RestTest.JsonReader
{
    internal class JsonConverterProperty : IJsonConverter
    {
        public JsonAttribute Convert(KeyValuePair<string, JToken> item)
        {
            var _converter = new JsonConverterManager();
            var prop = item.Value as JProperty;
            return _converter[prop.Value.Type].Convert(new KeyValuePair<string, JToken>(prop.Name, prop.Value));
        }
    }
}