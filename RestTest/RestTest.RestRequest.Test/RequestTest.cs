using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.RestRequest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace RestTest.RestRequest.Test
{
    [TestClass]
    public class RequestTest
    {
        private const int Port = 8089;

        private static void CreateHttpServerInternal()
        {
            var server = new HttpListener();
            server.Prefixes.Add($"http://127.0.0.1:{Port}/");
            server.Prefixes.Add($"http://localhost:{Port}/");

            server.Start();

            while (true)
            {
                HttpListenerContext context = server.GetContext();
                HttpListenerResponse response = context.Response;

                var body = new StreamReader(context.Request.InputStream).ReadToEnd();
                byte[] buffer = Encoding.UTF8.GetBytes(body);

                response.ContentLength64 = buffer.Length;
                Stream st = response.OutputStream;
                st.Write(buffer, 0, buffer.Length);

                context.Response.Close();
            }
        }

        [TestInitialize]
        public void CreateHttpServer()
        {
            var thread = new Thread(() => CreateHttpServerInternal());
            thread.Name = "Http Server";
            thread.Start();
            Thread.Sleep(2000);
        }

        [TestMethod]
        public void OnSendMessage404()
        {
            var wrongPort = 8083;
            var request = Requests.Create(new RequestConfig($"http://localhost:{wrongPort}/resource", "GET"));
            var response = request.Send();

            Assert.AreEqual(404, response.Status);
        }

        [TestMethod]
        public void OnSendMessageSucess()
        {
            var header = new Dictionary<string, string>()
            {
                { "Content-Type", "application/json" }
            };
            var body = "{name: \"Robert\"}";
            var request = Requests.Create(new RequestConfig($"http://localhost:{Port}/resource", "POST", header, body));
            var response = request.Send();

            Assert.AreEqual(200, response.Status);
            Assert.AreEqual(body, response.Body);
        }
    }
}
