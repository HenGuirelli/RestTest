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
        private const string PlainTextResponse = "example plaint text response from API";

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

                byte[] buffer = Encoding.UTF8.GetBytes(PlainTextResponse);

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
        public void OnCreate()
        {
            var header = new Dictionary<string, string>()
            {
                { "Content-Type", "application/json" }
            };
            var request = Requests.Create(new RequestConfig($"http://localhost:{Port}/resource", "POST", header, "{name: \"Robert\"}"));
            var resp = request.Send();

            Assert.AreEqual(200, resp.Status);
            Assert.AreEqual(PlainTextResponse, resp.Body);
        }
    }
}
