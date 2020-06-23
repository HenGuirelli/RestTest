using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace RestTest.HttpServer.Test
{
    public class HttpServerHelp
    {
        public string ResponseBody { get; set; } = string.Empty;
        public Dictionary<string, string> ResponseQueryString { get; private set; } = new Dictionary<string, string>();
        public Dictionary<string, string> ResponseCookies { get; private set; } = new Dictionary<string, string>();

        private void CreateHttpServerInternal(int port)
        {
            var server = new HttpListener();
            server.Prefixes.Add($"http://127.0.0.1:{port}/");
            server.Prefixes.Add($"http://localhost:{port}/");

            server.Start();

            while (true)
            {
                HttpListenerContext context = server.GetContext();
                HttpListenerResponse response = context.Response;

                //var body = new StreamReader(context.Request.InputStream).ReadToEnd();
                byte[] buffer = Encoding.UTF8.GetBytes(ResponseBody);
                if (ResponseQueryString.Any())
                {
                    response.Redirect(context.Request.Url + "?" + string.Join("&", ResponseQueryString.Select(x => $"{x.Key}={x.Value}")));
                }

                if (ResponseCookies.Any())
                {
                    response.Cookies.AddRange(ResponseCookies);
                }

                response.ContentLength64 = buffer.Length;
                Stream st = response.OutputStream;
                st.Write(buffer, 0, buffer.Length);

                context.Response.Close();
            }
        }

        public void CreateHttpServer(int port)
        {
            var thread = new Thread(() => CreateHttpServerInternal(port));
            thread.Name = "Http Server";
            thread.Start();
            Thread.Sleep(2000);
        }
    }
}
