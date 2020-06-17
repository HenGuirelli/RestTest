using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.Library.Config;
using RestTest.RestRequest;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestTest.Library.Test
{
    [TestClass]
    public class TestResultTest
    {
        [TestMethod]
        public void OnCtor_BodyJson()
        {
            var body = new Dictionary<string, string>()
            {

            };
            var validation = new ValidationConfig(default, default, default, default, status: 200, default, default);
            var response = new Response(200, string.Empty);
            var testResult = new TestResult(validation, response);
        }

        [TestMethod]
        public void OnCtor_Status()
        {
            var validation = new ValidationConfig(default, default, default, default, status: 200, default, default);
            var response = new Response(200, string.Empty);
            var testResult = new TestResult(validation, response);
            Assert.AreEqual(Status.Ok, testResult.Status);
        }
    }
}
