using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.HttpServer.Test;
using RestTest.RestRequest;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace RestTest.Library.Test
{
    internal class ClassTest
    {
        public Requests Request { get; set; }
        public ConcurrentDictionary<string, TestResult> Results = new ConcurrentDictionary<string, TestResult>();
        public void OnFinished(TestResult testResult)
        {
            Results[testResult.TestName] = testResult;
        }
    }

    [TestClass]
    public class StatusTests
    {
        private const int Port = 8091;
        private volatile bool testFinished = false;

        [TestInitialize]
        public void CreateHttpServer()
        {
            var server = new HttpServerHelp();
            server.CreateHttpServer(Port);
        }

        [TestMethod]
        public void OnUseRestTest_ValidateResponse()
        {
            var classTest = new ClassTest();

            var restTest = new RT("./test.json");
            restTest.OnTestFinished += classTest.OnFinished;
            restTest.OnAllTestsFinished += () => testFinished = true;

            restTest.Start();
            SpinWait.SpinUntil(() => testFinished);
            Assert.AreEqual(Status.Ok, classTest.Results["request 1"].Status);
            Assert.AreEqual(Status.Fail, classTest.Results["request 2"].Status);
        }
    }
}
