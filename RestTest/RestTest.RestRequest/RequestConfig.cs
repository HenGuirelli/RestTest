using System.Collections.Generic;

namespace RestTest.RestRequest
{
    public class RequestConfig
    {
        public string Url { get; private set; }
        public string Method { get; private set; }
        public IDictionary<string, string> Header { get; private set; }
        public string Body { get; private set; }

        public RequestConfig(
            string url,
            string method,
            IDictionary<string, string> header,
            string body)
        {
            Url = url;
            Method = method;
            Header = header;
            Body = body;
        }
    }
}