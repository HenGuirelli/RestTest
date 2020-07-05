using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.HttpServer.Test;
using RestTest.RestRequest;
using System.Collections.Concurrent;
using RestTestResult = RestTest.Library.Entity.Test.TestResult;

namespace RestTest.Library.SequenceDependency.Test
{
    internal class ClassTest
    {
        public Requests Request { get; set; }
        public ConcurrentDictionary<string, RestTestResult> Results { get; private set; }
            = new ConcurrentDictionary<string, RestTestResult>();

        public void OnFinished(RestTestResult testResult)
        {
            Results[testResult.TestName] = testResult;
        }
    }
    [TestClass]
    public class SequenceDependecyResolverTest
    {
        private const int Port = 8091;
        private volatile bool _testFinished;
        HttpServerHelp _server;

        [TestInitialize]
        public void CreateHttpServer()
        {
            _testFinished = false;
            _server = new HttpServerHelp();
            _server.CreateHttpServer(Port);
        }

        [TestCleanup]
        public void StopHttpServer()
        {
            _server.StopHttpServer();
        }

        [TestMethod]
        public void OnReplaceQueryString()
        {
            //var classTest = new ClassTest();

            //var restTest = new RT("./sequence_test.json");
            //restTest.OnTestFinished += classTest.OnFinished;
            //restTest.OnAllTestsFinished += () => _testFinished = true;

            //_server.ResponseBody = "{ id: 123 }";

            //restTest.Start();
            //SpinWait.SpinUntil(() => _testFinished);
        }
    }
}
