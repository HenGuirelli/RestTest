using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.HttpServer.Test;
using RestTest.RestRequest;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
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
            Assert.AreEqual("Status => expected 200\nreceived 404", classTest.Results["validation status wrong port"].Error);

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
            Assert.AreEqual(Status.Ok, classTest.Results["validation body"].Status);
            Assert.AreEqual(string.Empty, classTest.Results["validation body"].Error);

            Assert.AreEqual(Status.Fail, classTest.Results["wrong validation body"].Status);
            Assert.AreEqual(RemoveNewLine($"Body => expected {{\"responseStr\": \"wrong response\", \"responseInt\": 19}} received {_server.ResponseBody}").Replace(" ", ""),
                RemoveNewLine(classTest.Results["wrong validation body"].Error).Replace(" ", ""));

            Assert.AreEqual(Status.Ok, classTest.Results["no body validation"].Status);
            Assert.AreEqual(string.Empty, classTest.Results["no body validation"].Error);
        }

        private static string RemoveNewLine(string s)
        {
            return Regex.Replace(s, @"\t|\n|\r", "");
        }

        [TestMethod]
        public void OnUseRestTest_ValidateCookies()
        {
            var classTest = new ClassTest();

            var restTest = new RT("./test.json");
            restTest.OnTestFinished += classTest.OnFinished;
            restTest.OnAllTestsFinished += () => testFinished = true;

            _server.ResponseCookies.Add("Country", "EUA");

            restTest.Start();
            SpinWait.SpinUntil(() => testFinished);

            Assert.AreEqual(Status.Ok, classTest.Results["cookie test"].Status);
            Assert.AreEqual(string.Empty, classTest.Results["cookie test"].Error);

            Assert.AreEqual(Status.Fail, classTest.Results["cookie test error"].Status);
            Assert.AreEqual("Cookie => expected { country: wrong cookie, cookie1: value1 }\nreceived { Country: EUA, cookie1: value1 }", classTest.Results["cookie test error"].Error);

            Assert.AreEqual(Status.Ok, classTest.Results["no cookie validation"].Status);
            Assert.AreEqual(string.Empty, classTest.Results["no cookie validation"].Error);
        }
    }
}
