using System.IO;
using System.Net;

namespace RestTest.RestRequest
{
    public class Requests
    {
        private readonly WebRequest _request;

        public Requests(WebRequest request)
        {
            _request = request;
        }

        public static Requests Create(RequestConfig uniqueConfiguration)
        {
            var request = WebRequest.Create(uniqueConfiguration.Url);
            request.Method = uniqueConfiguration.Method.ToString();

            request.Headers = request.Headers ?? new WebHeaderCollection();
            foreach (var item in uniqueConfiguration.Header)
            {
                request.Headers.Add(item.Key, item.Value);
            }

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                string json = uniqueConfiguration.BodyStr;
                streamWriter.Write(json);
            }

            return new Requests(request);
        }

        public Response Send()
        {
            var response = _request.GetResponse();
            return null;
        }
    }
}