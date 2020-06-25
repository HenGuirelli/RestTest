using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace RestTest.JsonHelper
{
    public abstract class JsonValue
    {
        protected object _typedValue;
        public abstract JsonValue this[string key] { get; }
        public abstract bool IsObject { get; }

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
}
