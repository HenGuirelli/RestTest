using RestTest.NewJsonHelper;
using System.Collections.Generic;
using System.Net;

namespace RestTest.Library.Entity.Http
{
    public class Cookies : JsonObject
    {
        public static Cookies Empty => new Cookies() { HasValue = false };
        public bool HasValue { get; private set; }

        public Cookies()
        {
            HasValue = true;
        }

        public Cookies(CookieCollection cookies)
        {
            foreach(Cookie cook in cookies)
            {
                Add(cook.Name, cook.Value);
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

        public void Add(string key, string value)
        {
            HasValue = true;
            Add(new JsonString(key, value));
        }
    }
}
