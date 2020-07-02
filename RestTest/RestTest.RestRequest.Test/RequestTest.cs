using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.HttpServer.Test;
using RestTest.JsonHelper;
using System.Collections.Generic;
using RestTest.Library.Entity;

namespace RestTest.RestRequest.Test
{
    [TestClass]
    public class RequestTest
    {
        private const int Port = 8089;
        private HttpServerHelp _server;

        [TestInitialize]
        public void CreateHttpServer()
        {
            _server = new HttpServerHelp();
            _server.CreateHttpServer(Port);
        }

        [TestCleanup]
        public void StopHttpServer()
        {
            _server.StopHttpServer();
        }

        [TestMethod]
        public void OnCreate_ErrorOnSendBodyInGET()
        {
            try
            {
                var header = new Dictionary<string, string>()
                {
                    { "Content-Type", "application/json" }
                };
                var body = "{name: \"Robert\"}";
                var cookies = new Dictionary<string, string>();
                var queryString = new Dictionary<string, string>();
                var request = Requests.Create(new RequestConfig($"http://localhost:{Port}/resource", "GET", header, cookies, queryString, body));
            }
            catch
            {
                return;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void OnSendMessage404()
        {
            var wrongPort = 8083;
            var request = Requests.Create(new RequestConfig($"http://localhost:{wrongPort}/resource", "GET"));
            var response = request.Send().Result;

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
            _server.ResponseBody = body;
            var cookies = new Dictionary<string, string>();
            var queryString = new Dictionary<string, string>();
            var request = Requests.Create(new RequestConfig($"http://localhost:{Port}/resource", "POST", header, cookies, queryString, body));
            var response = request.Send().Result;

            Assert.AreEqual(200, response.Status);
            Assert.IsTrue(new Json(body).Compare(response.Body));
        }
    }
}
