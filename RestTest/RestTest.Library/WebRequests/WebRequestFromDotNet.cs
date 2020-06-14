using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace RestTest.Library.WebRequests
{
    public class WebRequestFromDotNet : IWebService
    {
        public string GetResponseString(object response)
        {
            if (response is null) return string.Empty;

            if (response is WebResponse)
            {
                return GetResponseFromWebResponse(response as WebResponse);
            }

            return string.Empty;
        }

        public async Task<object> Request(string endpoint)
        {
            var request = WebRequest.Create(endpoint);
            return await request.GetResponseAsync().ConfigureAwait(false);
        }

        private string GetResponseFromWebResponse(WebResponse response)
        {
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
