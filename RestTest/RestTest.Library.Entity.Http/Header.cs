using RestTest.NewJsonHelper;
using System;
using System.Collections.Generic;
using System.Net;

namespace RestTest.Library.Entity.Http
{
    public class Header : JsonObject
    {
        public static Header Empty => new Header() { HasValue = false };
        public bool HasValue { get; private set; }

        public Header()
            : base(StringComparer.OrdinalIgnoreCase)
        {
            HasValue = true;
        }

        public Header(WebHeaderCollection header)
            : this()
        {
            foreach (var headerkey in header.AllKeys)
            {
                Add(headerkey, header[headerkey]);
            }
        }

        public Dictionary<string, string> ToDictionary()
        {
            var dict = new Dictionary<string, string>();
            foreach(var keyValuePair in _jsons)
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