using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.HttpServer.Test;
using RestTest.RestRequest;
using System.Collections.Concurrent;
using System.Threading;

namespace RestTest.Library.Test
{
    internal class ClassTest
    {
        public Requests Request { get; set; }
        public ConcurrentDictionary<string, TestResult> Results { get; private set; }
            = new ConcurrentDictionary<string, TestResult>();

        public void OnFinished(TestResult testResult)
        {
            Results[testResult.TestName] = testResult;
        }
    }

    [TestClass]
    public class ValidateTests
    {
        private const int Port = 8091;
        private volatile bool testFinished = false;
        HttpServerHelp _server;

        [TestInitialize]
        public void CreateHttpServer()
        {
            _server = new HttpServerHelp();
            _server.CreateHttpServer(Port);
        }

        [TestMethod]
        public void OnUseRestTest_ValidateStatus()
        {
            var classTest = new ClassTest();

            var restTest = new RT("./test.json");
            restTest.OnTestFinished += classTest.OnFinished;
            restTest.OnAllTestsFinished += () => testFinished = true;

            restTest.Start();
            SpinWait.SpinUntil(() => testFinished);
            Assert.AreEqual(Status.Ok, classTest.Results["validation status 200"].Status);
            Assert.AreEqual(string.Empty, classTest.Results["validation status 200"].Error);

            Assert.AreEqual(Status.Fail, classTest.Results["validation status wrong port"].Status);
            Assert.AreEqual("Status => expected 200 received 404", classTest.Results["validation status wrong port"].Error);

            Assert.AreEqual(Status.Ok, classTest.Results["without status validation. Status 200"].Status);
            Assert.AreEqual(string.Empty, classTest.Results["without status validation. Status 200"].Error);

            Assert.AreEqual(Status.Ok, classTest.Results["without status validation. Status 404"].Status);
            Assert.AreEqual(string.Empty, classTest.Results["without status validation. Status 404"].Error);
        }

        [TestMethod]
        public void OnUseRestTest_ValidateBody()
        {
            var classTest = new ClassTest();

            var restTest = new RT("./test.json");
            restTest.OnTestFinished += classTest.OnFinished;
            restTest.OnAllTestsFinished += () => testFinished = true;

            _server.ResponseBody = "{\"responseStr\": \"any\", \"responseInt\": 19 }";

            restTest.Start();
            SpinWait.SpinUntil(() => testFinished);
            Assert.AreEqual(Status.Ok, classTest.Results["request 3"].Status);
            Assert.AreEqual(string.Empty, classTest.Results["request 3"].Error);

            Assert.AreEqual(Status.Fail, classTest.Results["request 4"].Status);
            Assert.AreEqual($"Body => expected {{\"responseStr\": \"wrong response\", \"responseInt\": 19}} received {_server.ResponseBody}", classTest.Results["request 4"].Error);
        }
    }
}
