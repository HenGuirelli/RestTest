using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.HttpServer.Test;
using System.Text.RegularExpressions;
using RestTest.Library.Entity.Test;

namespace RestTest.Library.Test
{
    [TestClass]
    public class ValidateTests
    {
        private const int Port = 8091;
        HttpServerHelp _server;

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
        public void OnUseRestTest_ValidateStatus()
        {
            var classTest = new ClassTest();

            var restTest = new RT("./test.json");
            restTest.OnTestFinished += classTest.OnFinished;

            restTest.Start();
            Assert.AreEqual(Status.Ok, classTest.Results["validation status 200"].Status);
            Assert.AreEqual(string.Empty, classTest.Results["validation status 200"].Error);

            Assert.AreEqual(Status.Fail, classTest.Results["validation status wrong port"].Status);
            Assert.IsTrue(classTest.Results["validation status wrong port"].Error.Contains("General error"));

            Assert.AreEqual(Status.Ok, classTest.Results["without status validation. Status 200"].Status);
            Assert.AreEqual(string.Empty, classTest.Results["without status validation. Status 200"].Error);
        }

        [TestMethod]
        public void OnUseRestTest_ValidateBody()
        {
            var classTest = new ClassTest();

            var restTest = new RT("./test.json");
            restTest.OnTestFinished += classTest.OnFinished;

            _server.ResponseBody = "{\"responseStr\": \"any\", \"responseInt\": 19 }";

            restTest.Start();
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

            _server.ResponseCookies.Add("Country", "EUA");

            restTest.Start();

            Assert.AreEqual(Status.Ok, classTest.Results["cookie test"].Status);
            Assert.AreEqual(string.Empty, classTest.Results["cookie test"].Error);

            Assert.AreEqual(Status.Fail, classTest.Results["cookie test error"].Status);
            Assert.AreEqual("Cookie => expected { country: wrong cookie, cookie1: value1 } received { Country: EUA, cookie1: value1 }", classTest.Results["cookie test error"].Error);

            Assert.AreEqual(Status.Ok, classTest.Results["no cookie validation"].Status);
            Assert.AreEqual(string.Empty, classTest.Results["no cookie validation"].Error);
        }

        [TestMethod]
        public void OnUseRestTest_ValidateHeader()
        {
            var classTest = new ClassTest();

            var restTest = new RT("./header_validation.json");
            restTest.OnTestFinished += classTest.OnFinished;
            
            _server.ResponseHeader.Add("Content-Type", "application/json");

            restTest.Start();

            //Assert.AreEqual(Status.Ok, classTest.Results["header validation"].Status);
            //Assert.AreEqual(string.Empty, classTest.Results["header validation"].Error);

            Assert.AreEqual(Status.Fail, classTest.Results["header wrong validation"].Status);
            var expected = "Header => expected { \"custom-header\": \"custom-value\", \"Server\": \"Microsoft-HTTPAPI/2.0\", \"Date\": \"${ANY}\", \"Content-Length\": \"0\" } received { \"Server\": \"Microsoft-HTTPAPI/2.0\", \"Date\": \"${ANY}\", \"Content-Length\": \"0\", \"Content-Type\": \"application/json\" }";
            var expectedFormated = new TextFormatter(expected).RemoveAllSpaces().ToString();

            // Replace date from test 
            var actual = Regex.Replace(classTest.Results["header wrong validation"].Error, "received.*\"Date\":.*GMT", "received { \"Server\": \"Microsoft-HTTPAPI/2.0\", \"Date\": \"${ANY}");
            var actualFormated = new TextFormatter(actual).RemoveAllSpaces().ToString();

            Assert.AreEqual(expectedFormated, actualFormated);
        }
    }
}
