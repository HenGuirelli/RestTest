namespace RestTest.RestRequest
{
    public class Response
    {
        public int Status { get; private set; }
        public string Body { get; private set; }

        public Response(int status, string body)
        {
            Status = status;
            Body = body;
        }
    }
}