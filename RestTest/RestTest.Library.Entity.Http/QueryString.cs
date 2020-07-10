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
