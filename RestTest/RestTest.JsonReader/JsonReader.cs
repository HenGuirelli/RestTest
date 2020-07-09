using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestTest.NewJsonHelper;
using System.Collections.Generic;
using System.IO;

namespace RestTest.JsonReader
{
    public abstract class JsonReader<T> : IJsonReader<T> 
        where T : JsonObject, new()
    {
        private readonly JsonConverterManager _converter = new JsonConverterManager();

        public T ReadByFile(string path)
        {
            return Read(File.ReadAllText(path));
        }

        public T Read(string json)
        {
            if (string.IsNullOrEmpty(json)) return Empty();

            try
            {
                var resultJsonObj = new T();
                var jObj = JsonConvert.DeserializeObject(json) as JObject;
                if (jObj != null)
                {
                    foreach (KeyValuePair<string, JToken> item in jObj)
                    {
                        JsonAttribute jsonAttribute = _converter[item.Value.Type].Convert(item);
                        resultJsonObj.Add(jsonAttribute);
                    }
                }
                return resultJsonObj;
            }
            catch
            {
                return new T();
            }
        }

        protected abstract T Empty();
    }
}
