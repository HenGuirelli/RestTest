using Newtonsoft.Json.Linq;
using RestTest.NewJsonHelper;
using System.Collections.Generic;

namespace RestTest.JsonReader
{
    public class JsonConverterLong : IJsonConverter
    {
        public JsonAttribute Convert(KeyValuePair<string, JToken> item)
        {
            return new JsonLong(item.Key, long.Parse(item.Value.ToString()));
        }
    }
}
