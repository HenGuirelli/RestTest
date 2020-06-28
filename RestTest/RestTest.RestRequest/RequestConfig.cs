using System.Collections.Generic;

namespace RestTest.RestRequest
{
    public class RequestConfig
    {
        public string Url { get; private set; }
        public string Method { get; private set; }
        public IDictionary<string, string> Header { get; private set; }
        public IDictionary<string, string> Cookies { get; private set; }
        public IDictionary<string, string> QueryString { get; private set; }
        public string Body { get; private set; }

        public RequestConfig(
            string url,
            string method,
            IDictionary<string, string> header,
            IDictionary<string, string> cookies,
            IDictionary<string, string> query_string,
            string body)
        {
            Url = url;
            Method = method;
            Header = header;
            Cookies = cookies;
            QueryString = query_string;
            Body = body;
        }

        public RequestConfig(string url, string method) 
            : this(url, method, new Dictionary<string, string>(), new Dictionary<string, string>(), new Dictionary<string, string>(), string.Empty) { }
    }
}