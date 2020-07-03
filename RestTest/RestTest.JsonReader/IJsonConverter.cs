using Newtonsoft.Json.Linq;
using RestTest.NewJsonHelper;
using System.Collections.Generic;

namespace RestTest.JsonReader
{
    public interface IJsonConverter
    {
        JsonAttribute Convert(KeyValuePair<string, JToken> item);
    }
}
