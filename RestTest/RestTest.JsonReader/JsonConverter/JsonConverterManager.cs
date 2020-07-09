using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace RestTest.JsonReader
{
    public class JsonConverterManager
    {
        private readonly Dictionary<JTokenType, IJsonConverter> _converter 
            = new Dictionary<JTokenType, IJsonConverter>();

        public IJsonConverter this[JTokenType type] => Get(type);

        public JsonConverterManager()
        {
            _converter[JTokenType.Integer] = new JsonConverterLong();
            _converter[JTokenType.String] = new JsonConverterString();
            _converter[JTokenType.Array] = new JsonConverterList();
            _converter[JTokenType.Object] = new JsonConverterObject();
            _converter[JTokenType.Property] = new JsonConverterProperty();
        }
        
        public IJsonConverter Get(JTokenType type)
        {
            return _converter[type];
        }
    }
}
