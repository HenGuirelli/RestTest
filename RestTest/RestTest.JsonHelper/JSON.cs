using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace RestTest.JsonHelper
{
    public abstract class JsonValue
    {
        protected object _typedValue;
        public abstract JsonValue this[string key] { get; }

        public static JsonValue Create(string item)
        {
            if (item.StartsWith("{"))
            {
                return new JsonValueObject(item);
            }

            if (item.StartsWith("["))
            {
                return new JsonValueList(item);
            }

            if (long.TryParse(item, out var respInt))
            {
                return new JsonValueLong(respInt);
            }
            
            if (double.TryParse(item, out var respFloat))
            {
                return new JsonValueDouble(respFloat);
            }

            return new JsonValueString(item);
        }

        public override string ToString()
        {
            return _typedValue.ToString();
        }

        public List<T> ToList<T>()
        {
            return ((JArray)((JToken)_typedValue)).ToObject<List<T>>();
        }
    }

    internal class JsonValueList : JsonValue
    {
        public JsonValueList(string value)
        {
            _typedValue = JsonConvert.DeserializeObject(value) as JToken;
        }

        public override JsonValue this[string key] => throw new System.NotImplementedException();
    }
    
    internal class JsonValueObject : JsonValue
    {
        public JsonValueObject(string value)
        {
            _typedValue = JsonConvert.DeserializeObject(value) as JObject;
        }

        public override JsonValue this[string key] => JsonValue.Create((_typedValue as JObject)[key].ToString());
    }

    internal class JsonValueString : JsonValue
    {
        public JsonValueString(string value)
        {
            _typedValue = value;
        }

        public override JsonValue this[string key] { get => throw new System.NotImplementedException(); }
    }

    internal class JsonValueLong : JsonValue
    {
        public JsonValueLong(long value)
        {
            _typedValue = value;
        }

        public override JsonValue this[string key] { get => throw new System.NotImplementedException(); }
    }
    
    internal class JsonValueDouble : JsonValue
    {
        public JsonValueDouble(double value)
        {
            _typedValue = value;
        }

        public override JsonValue this[string key] { get => throw new System.NotImplementedException(); }
    }

    public class Json
    {
        private readonly JObject _json;
        public JsonValue this[string index] => JsonValue.Create(_json[index].ToString());

        public Json(string json)
        {
            _json = JsonConvert.DeserializeObject(json) as JObject;
        }

        public bool Compare(string other)
        {
            return JToken.DeepEquals(_json, JsonConvert.DeserializeObject(other) as JObject);
        }
    }
}
