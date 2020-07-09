using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestTest.NewJsonHelper
{
    public class JsonObject : JsonAttribute, IEquatable<JsonObject>
    {
        protected readonly Dictionary<string, JsonAttribute> _jsons;

        public IEnumerable<string> Keys => _jsons.Keys;
        public override JsonAttribute this[string key] => GetAttribute(key);

        public JsonObject(string key)
            : this()
        {
            Key = key;
        }

        public JsonObject()
            : this(StringComparer.Ordinal)
        {
        }

        public JsonObject(StringComparer comparer)
        {
            _jsons = new Dictionary<string, JsonAttribute>(comparer);
        }

        private JsonAttribute GetAttribute(string key)
        {
            _jsons.TryGetValue(key, out var result);
            return result;
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

        public override bool Equals(object obj)
        {
            return Equals(obj as JsonObject);
        }

        public bool Equals(JsonObject other)
        {
            if (other is null) return false;
            if (Keys.Count() != other.Keys.Count()) return false;

            foreach (var key in Keys)
            {
                if (this[key] is null) return false;
                if (!this[key].Equals(other[key])) return false;
            }

            return true;
        }

        public override string ToString()
        {
            var strBuilder = new StringBuilder();
            strBuilder.Append("{");
            var countKeys = Keys.Count();
            var keyArray = Keys.ToArray();
            for (var  i = 0; i < countKeys; i++)
            {
                var key = keyArray[i];
                strBuilder.Append(i == countKeys - 1 ?  this[key].ToString() : this[key].ToString() + ",");
            }
            strBuilder.Append("}");
            return strBuilder.ToString();
        }
    }
}
