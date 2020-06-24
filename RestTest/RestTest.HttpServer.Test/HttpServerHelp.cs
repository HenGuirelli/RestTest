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
        private HttpListener _server;
        private Thread _threadServer;
        private volatile bool _serverRunning;

        public string ResponseBody { get; set; } = string.Empty;
        public Dictionary<string, string> ResponseQueryString { get; private set; } = new Dictionary<string, string>();
        public Dictionary<string, string> ResponseCookies { get; private set; } = new Dictionary<string, string>();
        public Dictionary<string, string> ResponseHeader { get; private set; } = new Dictionary<string, string>();

        private void CreateHttpServerInternal(int port)
        {
            _server = new HttpListener();
            _server.Prefixes.Add($"http://127.0.0.1:{port}/");
            _server.Prefixes.Add($"http://localhost:{port}/");

            _server.Start();
            while (_serverRunning)
            {
                HttpListenerContext context = _server.GetContext();
                HttpListenerResponse response = context.Response;

                byte[] buffer = Encoding.UTF8.GetBytes(ResponseBody);
                if (ResponseQueryString.Any())
                {
                    response.Redirect(context.Request.Url + "?" + string.Join("&", ResponseQueryString.Select(x => $"{x.Key}={x.Value}")));
                }

                if (ResponseCookies.Any())
                {
                    response.AddCookiesRange(ResponseCookies);
                }

                if (ResponseHeader.Any())
                {
                    response.AddHeaderRange(ResponseHeader);
                }

                response.ContentLength64 = buffer.Length;
                Stream st = response.OutputStream;
                st.Write(buffer, 0, buffer.Length);

                context.Response.Close();
            }
        }

        public void StopHttpServer()
        {
            _server.Stop();
            _serverRunning = false;
        }

        public void CreateHttpServer(int port)
        {
            _serverRunning = true;
            _threadServer = new Thread(() => CreateHttpServerInternal(port));
            _threadServer.Name = "Http Server";
            _threadServer.Start();
            Thread.Sleep(2000);
        }
    }
}
