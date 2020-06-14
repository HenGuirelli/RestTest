using System.Collections.Generic;

namespace RestTest.Library.Config
{
    public class ValidationConfig
    {
        public Dictionary<string, string> Body { get; private set; } = new Dictionary<string, string>();
        public Dictionary<string, string> Header { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> QueryString { get; private set; } = new Dictionary<string, string>();
        public Dictionary<string, string> Cookies { get; private set; } = new Dictionary<string, string>();
        public int Status { get; private set; }

        public int MaxTime { get; private set; }
        public int MinTime { get; set; }

        public ValidationConfig() { }

        public ValidationConfig(
            Dictionary<string, string> body,
            Dictionary<string, string> header,
            Dictionary<string, string> queryString,
            Dictionary<string, string> cookies,
            int status,
            int maxTime,
            int minTime)
        {
            Body = body;
            Header = header;
            QueryString = queryString;
            Cookies = cookies;
            Status = status;
            MaxTime = maxTime;
            MinTime = minTime;
        }
    }
}
