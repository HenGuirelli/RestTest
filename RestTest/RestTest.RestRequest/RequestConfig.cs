using System.Collections.Generic;

namespace RestTest.RestRequest
{
    public class RequestConfig
    {
        public string Name { get; private set; }
        public string Url { get; private set; }
        public string Method { get; private set; }
        public IDictionary<string, string> Header { get; private set; }
        public string BodyStr { get; private set; }

        public RequestConfig(
            string name,
            string url,
            string method,
            IDictionary<string, string> header,
            string bodyStr)
        {
            Name = name;
            Url = url;
            Method = method;
            Header = header;
            BodyStr = bodyStr;
        }
    }
}