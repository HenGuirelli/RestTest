using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.RestRequest;
using System;

namespace RestTest.Library.Test
{
    internal class ClassTest : IRequestLifeClycle
    {
        public Requests Request { get; set; }

        public void OnFinished(TestResult testResult)
        {
        }

        public void OnStart()
        {
        }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void OnUseRestTest()
        {
            var url = "http://localhost";
            var method = "GET";
            var requestCallbackMap = new RequestCallbackMap();
            requestCallbackMap["request1"] = new ClassTest
            {
                Request = Requests.Create(new RequestConfig(url, method))
            };

            var restTest = new RT("./tests.json", requestCallbackMap);

            restTest.Start();
        }
    }
}
