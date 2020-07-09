using RestTest.Configuration;
using RestTest.RestRequest;
using System.Collections.Generic;
using System.Linq;

namespace RestTest.Library
{
    public static class ExtensionMethod
    {
        public static bool Contains(this IEnumerable<UniqueConfiguration> input, string name)
        {
            return input.Any(item => item.Name == name);
        }
        
        public static bool Contains(this IEnumerable<SequenceConfiguration> input, string name)
        {
            return input.Any(item => item.Name == name);
        }

        public static RequestConfig ToRequestConfig(this UniqueConfiguration input)
        {
            return new RequestConfig(input.Url, input.Method.ToString(), input.Header.ToDictionary(), input.Cookies, input.QueryString, input.BodyStr);
        }
    }
}
