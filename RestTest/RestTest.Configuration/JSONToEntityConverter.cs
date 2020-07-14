using Newtonsoft.Json.Linq;
using RestTest.Configuration.JsonNotation;
using RestTest.JsonReader;
using RestTest.Library.Entity.Http;
using RestTest.Library.Entity.Test;
using System;
using System.Collections.Generic;

namespace RestTest.Configuration
{
    internal class JSONToEntityConverter
    {
        static readonly IJsonReader<Body> _readerBody = new JsonReaderBody();
        static readonly IJsonReader<Header> _readerHeader = new JsonReaderHeader();
        static readonly IJsonReader<Cookies> _readerCookies = new JsonReaderCookie();
        static readonly IJsonReader<QueryString> _readerQueryString = new JsonReaderQueryString();

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
                _readerHeader.Read(uniqueConfigurationJSONNotation.header?.ToString() ?? string.Empty),
                _readerCookies.Read(uniqueConfigurationJSONNotation.cookies?.ToString() ?? string.Empty),
                _readerQueryString.Read(uniqueConfigurationJSONNotation.query_string?.ToString() ?? string.Empty),
                _readerBody.Read(uniqueConfigurationJSONNotation.body?.ToString()?.Trim() ?? string.Empty),
                uniqueConfigurationJSONNotation.body?.ToString()?.Trim() ?? string.Empty,
                JSONToValidation(uniqueConfigurationJSONNotation.validation)
            );
        }

        private static Validation JSONToValidation(ValidationJsonNotation validation)
        {
            if (validation is null) return new Validation();

            return new Validation
            (
                _readerBody.Read(validation.body?.ToString() ?? string.Empty),
                _readerHeader.Read(validation.header?.ToString() ?? string.Empty),
                _readerQueryString.Read(validation.query_string?.ToString() ?? string.Empty),
                _readerCookies.Read(validation.cookies?.ToString() ?? string.Empty),
                validation.status,
                validation.max_time,
                validation.min_time
            );
        }
    }
}
