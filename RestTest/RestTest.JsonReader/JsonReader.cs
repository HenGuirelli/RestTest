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

        public Body ReadByFile(string path)
        {
            return Read(File.ReadAllText(path));
        }

        public Body Read(string json)
        {
            if (string.IsNullOrEmpty(json)) return Body.Empty;

            var body = new Body();
            var jObj = JsonConvert.DeserializeObject(json) as JObject;
            if (jObj != null)
            {
                foreach (KeyValuePair<string, JToken> item in jObj)
                {
                    JsonAttribute jsonAttribute = _converter[item.Value.Type].Convert(item);
                    body.Add(jsonAttribute);
                }
            }

            return body;
        }
    }
}
