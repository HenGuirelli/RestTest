using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.HttpServer.Test;
using RestTest.RestRequest;
using System;
using System.Threading;

namespace RestTest.Library.Test
{
    internal class ClassTest : IRequestLifeClycle
    {
        public Requests Request { get; set; }
        private UseRTTest _testInstance;

        public ClassTest(UseRTTest testInstance)
        {
            _testInstance = testInstance;
        }

        public void OnFinished(TestResult testResult)
        {
            _testInstance.testFinished = true;
            _testInstance.testResult = testResult;
        }

        public void OnStart()
        {
        }
    }

    [TestClass]
    public class UseRTTest
    {
        private const int Port = 8091;
        public volatile bool testFinished = false;
        public TestResult testResult;

        [TestInitialize]
        public void CreateHttpServer()
        {
            var server = new HttpServerHelp();
            server.CreateHttpServer(Port);
        }

        [TestMethod]
        public void OnUseRestTest()
        {
            var url = "http://localhost:" + Port;
            var method = "GET";
            var requestCallbackMap = new RequestCallbackMap();
            requestCallbackMap["request 1"] = new ClassTest(this)
            {
                Request = Requests.Create(new RequestConfig(url, method))
            };

            var restTest = new RT("./test.json", requestCallbackMap);

            restTest.Start();

            SpinWait.SpinUntil(() => testFinished);

            Assert.AreEqual(Status.Ok, testResult.Status);
        }
    }
}
