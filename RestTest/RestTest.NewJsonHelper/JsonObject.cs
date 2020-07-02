using System;
using System.Collections.Generic;

namespace RestTest.NewJsonHelper
{
    public class JsonObject : Json
    {
        private readonly Dictionary<string, JsonAttribute> _jsons = new Dictionary<string, JsonAttribute>();

        public IEnumerable<string> Keys => _jsons.Keys;
        public JsonAttribute this[string key] => GetAttribute(key);

        private JsonAttribute GetAttribute(string key)
        {
            return _jsons[key];
        }

        public void Add(JsonAttribute json)
        {
            if (string.IsNullOrWhiteSpace(json.Key)) throw new ArgumentException("Json attribute should be an key");
            _jsons.Add(json.Key, json);
        }

        public void Remove(JsonAttribute json)
        {
            _jsons.Remove(json.Key);
        }

        public void Remove(string key)
        {
            _jsons.Remove(key);
        }

        public override object GetValue()
        {
            return this;
        }
    }
}
