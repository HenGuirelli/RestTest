using Newtonsoft.Json.Linq;
using RestTest.Configuration.JsonNotation;
using RestTest.Library.Config;
using System;
using System.Collections.Generic;

namespace RestTest.Configuration
{
    internal class JSONToEntityConverter
    {
        public static SequenceConfiguration ConvertSequenceConfiguration(SequenceConfigurationJsonNotation sequenceConfigurationJSONNotation)
        {
            return new SequenceConfiguration(
                sequenceConfigurationJSONNotation.name,
                sequenceConfigurationJSONNotation.type,
                ConvertToUniqueTestSequence(sequenceConfigurationJSONNotation.sequence)
            );
        }

        private static List<UniqueConfiguration> ConvertToUniqueTestSequence(List<UniqueConfigurationJsonNotation> sequence)
        {
            var result = new List<UniqueConfiguration>();
            foreach (var item in sequence)
            {
                result.Add(ConvertUniqueConfiguration(item));
            }
            return result;
        }

        public static UniqueConfiguration ConvertUniqueConfiguration(UniqueConfigurationJsonNotation uniqueConfigurationJSONNotation)
        {
            Enum.TryParse<Method>(uniqueConfigurationJSONNotation.method, ignoreCase: true, out var method);
            return new UniqueConfiguration(
                TestType.unique_test,
                uniqueConfigurationJSONNotation.name,
                uniqueConfigurationJSONNotation.url,
                method,
                JSONToDictionary(uniqueConfigurationJSONNotation.header as JObject),
                JSONToValidation(uniqueConfigurationJSONNotation.validation)
            );
        }

        private static ValidationConfig JSONToValidation(ValidationConfigJsonNotation validation)
        {
            if (validation is null) return new ValidationConfig();

            return new ValidationConfig
            (
                JSONToDictionary(validation.body as JObject),
                JSONToDictionary(validation.header as JObject),
                JSONToDictionary(validation.query_string as JObject),
                JSONToDictionary(validation.cookie as JObject),
                validation.status,
                validation.max_time,
                validation.min_time
            );
        }

        private static Dictionary<string, string> JSONToDictionary(JObject obj)
        {
            if (obj is null) return new Dictionary<string, string>();

            return obj.ToObject<Dictionary<string, string>>();
        }
    }
}
