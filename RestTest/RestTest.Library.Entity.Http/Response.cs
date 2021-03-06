﻿
namespace RestTest.Library.Entity.Http
{
    public class Response
    {
        public int Status { get; private set; }
        public Body Body { get; private set; }
        public string Error { get; private set; }
        public Cookies Cookies { get; private set; }
        public Header Header { get; private set; }

        public Response(int status, Body body, Cookies cookies, Header header, string error = "")
        {
            Status = status;
            Body = body;
            Error = error;
            Cookies = cookies;
            Header = header;
        }
    }
}