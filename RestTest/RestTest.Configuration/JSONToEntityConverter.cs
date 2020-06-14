using Newtonsoft.Json.Linq;
using RestTest.Configuration.JsonNotation;
using RestTest.Library.Config;
using System;
using System.Collections.Generic;

namespace RestTest.Configuration
{
    internal class JSONToEntityConverter
    {
        public static UniqueConfiguration ConvertUniqueConfiguration(UniqueConfigurationJsonNotation uniqueConfigurationJSONNotation)
        {
            Enum.TryParse<Method>(uniqueConfigurationJSONNotation.method, ignoreCase: true, out var method);
            return new UniqueConfiguration(
                TestType.unique_test,
                uniqueConfigurationJSONNotation.name,
                uniqueConfigurationJSONNotation.url,
                method,
                JSONToDictionary(uniqueConfigurationJSONNotation.header as JObject)
            );
        }

        private static Dictionary<string, string> JSONToDictionary(JObject obj)
        {
            if (obj is null) return new Dictionary<string, string>();

            return obj.ToObject<Dictionary<string, string>>();
        }
    }
}
