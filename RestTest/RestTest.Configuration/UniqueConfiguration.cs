using RestTest.Library.Config;
using System.Collections.Generic;

namespace RestTest.Configuration
{
    public class UniqueConfiguration
    {
        public TestType Type { get; private set; }
        public string Name { get; private set; }
        public string Url { get; private set; }
        public Method Method { get; private set; }
        public IDictionary<string, string> Header { get; private set; }
        public IDictionary<string, object> Body { get; private set; }
        public string BodyStr { get; private set; }

        public ValidationConfig Validation { get; private set; } = new ValidationConfig();

        public UniqueConfiguration(
            TestType type, 
            string name, 
            string url, 
            Method method, 
            IDictionary<string, string> header,
            IDictionary<string, object> body,
            string bodyStr,
            ValidationConfig validation)
        {
            Type = type;
            Name = name;
            Url = url;
            Method = method;
            Header = header;
            Body = body;
            BodyStr = bodyStr;
            Validation = validation;
        }
    }
}
