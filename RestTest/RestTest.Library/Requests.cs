using RestTest.Configuration;
using System;
using System.IO;
using System.Net;

namespace RestTest.Library
{
    internal class Requests
    {
        private WebRequest _request;

        public Requests(WebRequest request)
        {
            _request = request;
        }

        internal static Requests Create(UniqueConfiguration uniqueConfiguration)
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

        internal Response Send()
        {
            var response = _request.GetResponse();
        }
    }
}