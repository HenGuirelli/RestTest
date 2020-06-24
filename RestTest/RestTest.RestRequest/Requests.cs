using RestTest.Configuration;
using RestTest.JsonHelper;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace RestTest.RestRequest
{
    public class Requests
    {
        private readonly HttpWebRequest _request;

        public Requests(HttpWebRequest request)
        {
            _request = request;
        }

        public static Requests Create(RequestConfig requestConfig)
        {
            var request = (HttpWebRequest)WebRequest.Create(requestConfig.Url);
            request.Method = requestConfig.Method.ToString();

            request.Headers = request.Headers ?? new WebHeaderCollection();
            foreach (var item in requestConfig.Header)
            {
                request.Headers.Add(item.Key, item.Value);
            }

            if (!string.IsNullOrWhiteSpace(requestConfig.Body))
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = requestConfig.Body;
                    streamWriter.Write(json);
                }
            }
            if (request.CookieContainer is null) request.CookieContainer = new CookieContainer();
            if (requestConfig.Cookies.Any()) 
            {
                foreach(var cook in requestConfig.Cookies)
                {
                    request.CookieContainer.Add(new Cookie(cook.Key, cook.Value) { Domain = new Uri(requestConfig.Url).Host });
                }
            }

            return new Requests(request);
        }

        public Response Send()
        {
            try
            {
                var response = (HttpWebResponse)_request.GetResponse();
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return new Response(
                        (int)response.StatusCode, 
                        new Json(reader.ReadToEnd()), 
                        new CookiesConfig(response.Cookies),
                        new HeaderConfig(response.Headers));
                }
            }
            catch(Exception ex)
            {
                return new Response(404, Json.Empty, CookiesConfig.Empty, HeaderConfig.Empty, ex.Message); ;
            }
        }
    }
}