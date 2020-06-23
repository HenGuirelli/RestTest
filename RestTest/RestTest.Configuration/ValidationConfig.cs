using RestTest.JsonHelper;
using System.Collections.Generic;

namespace RestTest.Library.Config
{
    public class ValidationConfig
    {
        public Json Body { get; private set; }
        public Dictionary<string, string> Header { get; set; }
        public Dictionary<string, string> QueryString { get; private set; }
        public Dictionary<string, string> Cookies { get; private set; }
        public int? Status { get; private set; }

        public int MaxTime { get; private set; }
        public int MinTime { get; set; }

        public ValidationConfig() { }

        public ValidationConfig(
            Json body,
            Dictionary<string, string> header,
            Dictionary<string, string> queryString,
            Dictionary<string, string> cookies,
            int status,
            int maxTime,
            int minTime)
        {
            Body = body ?? new Json(string.Empty);
            Header = header ?? new Dictionary<string, string>();
            QueryString = queryString ?? new Dictionary<string, string>();
            Cookies = cookies ?? new Dictionary<string, string>();
            Status = status == 0 ? (int?)null : status;
            MaxTime = maxTime;
            MinTime = minTime;
        }
    }
}
