using Newtonsoft.Json.Linq;
using RestTest.Configuration.JsonNotation;
using RestTest.JsonReader;
using RestTest.Library.Entity;
using RestTest.Library.Entity.Http;
using RestTest.Library.Entity.Test;
using System;
using System.Collections.Generic;

namespace RestTest.Configuration
{
    internal class JSONToEntityConverter
    {
        static IJsonReader _reader = new JsonReader.JsonReader();

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
            foreach (var item in sequence ?? new List<UniqueConfigurationJsonNotation>())
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
                JSONToDictionary<string, string>(uniqueConfigurationJSONNotation.header as JObject),
                JSONToDictionary<string, string>(uniqueConfigurationJSONNotation.cookies as JObject),
                JSONToDictionary<string, string>(uniqueConfigurationJSONNotation.query_string as JObject),
                _reader.Read(uniqueConfigurationJSONNotation.body?.ToString()?.Trim() ?? string.Empty),
                uniqueConfigurationJSONNotation.body?.ToString()?.Trim() ?? string.Empty,
                JSONToValidation(uniqueConfigurationJSONNotation.validation)
            );
        }

        private static Validation JSONToValidation(ValidationJsonNotation validation)
        {
            if (validation is null) return new Validation();

            return new Validation
            (
                _reader.Read(validation.body?.ToString() ?? string.Empty),
                new Header(JSONToDictionary<string, string>(validation.header as JObject)),
                JSONToDictionary<string, string>(validation.query_string as JObject),
                new Cookies(JSONToDictionary<string, string>(validation.cookies as JObject)),
                validation.status,
                validation.max_time,
                validation.min_time
            );
        }

        private static Dictionary<TKey, TValue> JSONToDictionary<TKey, TValue>(JObject obj)
        {
            if (obj is null) return new Dictionary<TKey, TValue>();

            return obj.ToObject<Dictionary<TKey, TValue>>();
        }
    }
}
