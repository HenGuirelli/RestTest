using Newtonsoft.Json.Linq;
using RestTest.NewJsonHelper;
using System.Collections.Generic;

namespace RestTest.JsonReader
{
    public class JsonConverterString : IJsonConverter
    {
        public JsonAttribute Convert(KeyValuePair<string, JToken> item)
        {
            return new JsonString(item.Key, item.Value.ToString());
        }
    }
}