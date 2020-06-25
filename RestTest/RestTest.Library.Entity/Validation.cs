using RestTest.Configuration;
using RestTest.JsonHelper;
using System.Collections.Generic;

namespace RestTest.Library
{
    public class Validation
    {
        public Json Body { get; private set; }
        public Header Header { get; set; }
        public Dictionary<string, string> QueryString { get; private set; }
        public Cookies Cookies { get; private set; }
        public int? Status { get; private set; }

        public int MaxTime { get; private set; }
        public int MinTime { get; set; }

        public Validation() { }

        public Validation(
            Json body,
            Header header,
            Dictionary<string, string> queryString,
            Cookies cookies,
            int status,
            int maxTime,
            int minTime)
        {
            Body = body ?? new Json(string.Empty);
            Header = header ?? new Header();
            QueryString = queryString ?? new Dictionary<string, string>();
            Cookies = cookies ?? new Cookies();
            Status = status == 0 ? (int?)null : status;
            MaxTime = maxTime;
            MinTime = minTime;
        }
    }
}
