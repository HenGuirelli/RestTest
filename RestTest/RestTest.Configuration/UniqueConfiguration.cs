using RestTest.Library.Entity.Http;
using RestTest.Library.Entity.Test;

namespace RestTest.Configuration
{
    public class UniqueConfiguration
    {
        public string Name { get; private set; }
        public string Url { get; private set; }
        public Method Method { get; private set; }
        public Header Header { get; private set; }
        public Cookies Cookies { get; private set; }
        public QueryString QueryString { get; private set; }
        public Body Body { get; private set; }
        public string BodyStr { get; private set; }
        public string Wait { get; private set; }

        public Validation Validation { get; private set; } = new Validation();

        public UniqueConfiguration(
            string name, 
            string url, 
            Method method,
            Header header,
            Cookies cookies,
            QueryString queryString,
            Body body,
            string bodyStr,
            Validation validation,
            string wait)
        {
            Name = name;
            Url = url;
            Method = method;
            Header = header;
            Cookies = cookies;
            QueryString = queryString;
            Body = body;
            BodyStr = bodyStr;
            Validation = validation;
            Wait = wait;
        }
    }
}
