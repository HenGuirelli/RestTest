using RestTest.Configuration;
using RestTest.JsonHelper;
using RestTest.Library;

namespace RestTest.RestRequest
{
    public class Response
    {
        public int Status { get; private set; }
        public Json Body { get; private set; }
        public string Error { get; private set; }
        public Cookies Cookies { get; private set; }
        public Header Header { get; private set; }

        public Response(int status, Json body, Cookies cookies, Header header, string error = "")
        {
            Status = status;
            Body = body;
            Error = error;
            Cookies = cookies;
            Header = header;
        }
    }
}