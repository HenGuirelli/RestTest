using RestTest.NewJsonHelper;
using System.Collections.Generic;

namespace RestTest.Library.Entity.Http
{
    public class QueryString : JsonObject
    {
        public new static QueryString Empty => new QueryString() { HasValue = false };

        public bool HasValue { get; private set; }

        public QueryString()
        {
            HasValue = true;
        }

        public QueryString(IDictionary<string, string> queryStringDict)
            : this()
        {
            foreach(var item in queryStringDict)
            {
                Add(new JsonString(item.Key, item.Value));
            }
        }

        public Dictionary<string, string> ToDictionary()
        {
            var dict = new Dictionary<string, string>();
            foreach (var keyValuePair in _jsons)
            {
                var key = keyValuePair.Key;
                var value = keyValuePair.Value.GetValue().ToString();
                dict.Add(key, value);
            }
            return dict;
        }
    }
}
