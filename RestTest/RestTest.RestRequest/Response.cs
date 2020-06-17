using RestTest.JsonHelper;

namespace RestTest.RestRequest
{
    public class Response
    {
        public int Status { get; private set; }
        public Json Body { get; private set; }
        public string Error { get; private set; }

        public Response(int status, Json body, string error = "")
        {
            Status = status;
            Body = body;
            Error = error;
        }
    }
}