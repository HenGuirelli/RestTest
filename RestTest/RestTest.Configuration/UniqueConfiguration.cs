using System.Collections.Generic;
using RestTest.Library.Entity;
using RestTest.NewJsonHelper;

namespace RestTest.Configuration
{
    public class UniqueConfiguration
    {
        public TestType Type { get; private set; }
        public string Name { get; private set; }
        public string Url { get; private set; }
        public Method Method { get; private set; }
        public IDictionary<string, string> Header { get; private set; }
        public IDictionary<string, string> Cookies { get; private set; }
        public IDictionary<string, string> QueryString { get; private set; }
        public Body Body { get; private set; }
        public string BodyStr { get; private set; }

        public Validation Validation { get; private set; } = new Validation();

        public UniqueConfiguration(
            TestType type, 
            string name, 
            string url, 
            Method method, 
            IDictionary<string, string> header,
            IDictionary<string, string> cookies,
            IDictionary<string, string> queryString,
            Body body,
            string bodyStr,
            Validation validation)
        {
            Type = type;
            Name = name;
            Url = url;
            Method = method;
            Header = header;
            Cookies = cookies;
            QueryString = queryString;
            Body = body;
            BodyStr = bodyStr;
            Validation = validation;
        }
    }
}
