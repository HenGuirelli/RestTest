using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestTest.JsonReader;
using RestTest.Library.Entity;
using RestTest.Library.Entity.Http;

namespace RestTest.RestRequest
{
    public class Requests
    {
        private readonly HttpWebRequest _request;
        private readonly IJsonReader<Body> _jsonReaderBody = new JsonReaderBody();

        public Requests(HttpWebRequest request)
        {
            _request = request;
        }

        public async Task<Response> Send()
        {
            try
            {
                var response = (HttpWebResponse)(await _request.GetResponseAsync());
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return new Response(
                        (int)response.StatusCode,
                        _jsonReaderBody.Read(await reader.ReadToEndAsync()),
                        new Cookies(response.Cookies),
                        new Header(response.Headers));
                }
            }
            catch (WebException ex) when (ex.Response != null)
            {
                var response = (HttpWebResponse)ex.Response;
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    var responseStr = reader.ReadToEndAsync();
                    return new Response(
                        (int)response.StatusCode,
                        _jsonReaderBody.Read(await responseStr),
                        new Cookies(response.Cookies),
                        new Header(response.Headers),
                        error: await responseStr);
                }
            }
            catch (Exception ex)
            {
                return new Response(404, Body.Empty, Cookies.Empty, Header.Empty, ex.Message);
            }
        }

        public static Requests Create(RequestConfig requestConfig)
        {
            var uri = GetUri(requestConfig);
            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = requestConfig.Method.ToString();
            SetHeaders(requestConfig, request);
            SetBody(requestConfig, request);
            SetCookies(requestConfig, request);
            return new Requests(request);
        }

        private static void SetCookies(RequestConfig requestConfig, HttpWebRequest request)
        {
            if (request.CookieContainer is null) request.CookieContainer = new CookieContainer();
            if (requestConfig.Cookies.Any())
            {
                foreach (var cook in requestConfig.Cookies)
                {
                    request.CookieContainer.Add(new Cookie(cook.Key, cook.Value) { Domain = new Uri(requestConfig.Url).Host });
                }
            }
        }

        private static void SetBody(RequestConfig requestConfig, HttpWebRequest request)
        {
            if (!string.IsNullOrWhiteSpace(requestConfig.Body))
            {
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    string json = requestConfig.Body;
                    streamWriter.Write(json);
                }
            }
        }

        private static void SetHeaders(RequestConfig requestConfig, HttpWebRequest request)
        {
            request.Headers = request.Headers ?? new WebHeaderCollection();
            foreach (var item in requestConfig.Header)
            {
                request.Headers.Add(item.Key, item.Value);
            }
        }

        private static Uri GetUri(RequestConfig requestConfig)
        {
            if (requestConfig.QueryString.Any())
            {
                return new Uri($"{requestConfig.Url}?" + string.Join("&", requestConfig.QueryString.Select(x => $"{x.Key}={x.Value}")));
            }
            return new Uri(requestConfig.Url);
        }

    }
}