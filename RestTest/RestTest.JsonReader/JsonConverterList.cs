using Newtonsoft.Json.Linq;
using RestTest.NewJsonHelper;
using System.Collections.Generic;

namespace RestTest.JsonReader
{
    public class JsonConverterList : IJsonConverter
    {
        public JsonAttribute Convert(KeyValuePair<string, JToken> item)
        {
            var _converter = new JsonConverterManager();

            var jsonList = new JsonList(item.Key);
            foreach (var listItem in item.Value)
            {
                var jsonValue = _converter[listItem.Type].Convert(new KeyValuePair<string, JToken>(null, listItem));
                jsonList.Add(jsonValue);
            }
            return jsonList;
        }
    }
}
