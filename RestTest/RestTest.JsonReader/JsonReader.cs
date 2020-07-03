using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestTest.Library.Entity;
using RestTest.NewJsonHelper;
using System.Collections.Generic;
using System.IO;

namespace RestTest.JsonReader
{
    public class JsonReader : IJsonReader
    {
        private readonly JsonConverterManager _converter = new JsonConverterManager();

        public Body CreateByFile(string path)
        {
            return Create(File.ReadAllText(path));
        }

        public Body Create(string json)
        {
            var body = new Body();
            var jObj = JsonConvert.DeserializeObject(json) as JObject;

            foreach (KeyValuePair<string, JToken> item in jObj)
            {
                JsonAttribute jsonAttribute = _converter[item.Value.Type].Convert(item);
                body.Add(jsonAttribute);
            }

            return body;
        }
    }
}
