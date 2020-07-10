using RestTest.Library.Entity.Http;

namespace RestTest.Library.Entity.Test
{
    public class Validation
    {
        public Body Body { get; private set; } = Body.Empty;
        public Header Header { get; set; } = Header.Empty;
        public QueryString QueryString { get; private set; } = QueryString.Empty;
        public Cookies Cookies { get; private set; } = Cookies.Empty;
        public int? Status { get; private set; }

        public int MaxTime { get; private set; }
        public int MinTime { get; set; }

        public Validation() { }

        public Validation(
            Body body,
            Header header,
            QueryString queryString,
            Cookies cookies,
            int status,
            int maxTime,
            int minTime)
        {
            Body = body ?? Body.Empty;
            Header = header ?? Header.Empty;
            QueryString = queryString ?? QueryString.Empty;
            Cookies = cookies ?? Cookies.Empty;
            Status = status == 0 ? (int?)null : status;
            MaxTime = maxTime;
            MinTime = minTime;
        }
    }
}
