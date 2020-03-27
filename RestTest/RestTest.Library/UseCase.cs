using System;

namespace RestTest.Library
{
    public class UseCase
    {
        public object Request { get; private set; }
        public object Response { get; private set; }

        public UseCase(object request, object response)
        {
            Request = request;
            Response = response;
        }

        public bool CompareResponse (object otherResponse)
        {
            return object.Equals(Response, otherResponse);
        }
    }
}
