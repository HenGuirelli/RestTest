using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.Configuration;
using RestTest.JsonHelper;
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
        public void OnCtor_EmptyBodyJson()
        {
            var body = new Json("");
            var validation = new ValidationConfig(body, default, default, default, status: 200, default, default);
            var response = new Response(200, Json.Empty, CookiesConfig.Empty, HeaderConfig.Empty);
            var testResult = new TestResult("name", validation, response);

            Assert.AreEqual(Status.Ok, testResult.Status);
        }
        
        [TestMethod]
        public void OnCtor_SimpleBodyJson()
        {
            var bodyString = "{ name: \"Ellie\" }";
            var body = new Json(bodyString);

            var validation = new ValidationConfig(body, default, default, default, status: 200, default, default);
            var response = new Response(200, new Json(bodyString), CookiesConfig.Empty, HeaderConfig.Empty);

            var testResult = new TestResult("name", validation, response);
            Assert.AreEqual(Status.Ok, testResult.Status);


            response = new Response(200, new Json("{ name: \"Marlene\" }"), CookiesConfig.Empty, HeaderConfig.Empty);
            testResult = new TestResult("name", validation, response);
            Assert.AreEqual(Status.Fail, testResult.Status);
        }  
        
        [TestMethod]
        public void OnCtor_ComplexBodyJson()
        {
            var bodyString = "{ person: { name: \"Ellie\", age: 14 } }";
            var body = new Json(bodyString);

            var validation = new ValidationConfig(body, default, default, default, status: 200, default, default);
            var response = new Response(200, new Json(bodyString), CookiesConfig.Empty, HeaderConfig.Empty);

            var testResult = new TestResult("name", validation, response);
            Assert.AreEqual(Status.Ok, testResult.Status);


            response = new Response(200, new Json("{ person: { name: \"Joel\", age: 40 } }"), CookiesConfig.Empty, HeaderConfig.Empty);
            testResult = new TestResult("name", validation, response);
            Assert.AreEqual(Status.Fail, testResult.Status);
        }

        [TestMethod]
        public void OnCtor_Status()
        {
            var validation = new ValidationConfig(default, default, default, default, status: 200, default, default);
            var response = new Response(200, Json.Empty, CookiesConfig.Empty, HeaderConfig.Empty);
            var testResult = new TestResult("name", validation, response);
            Assert.AreEqual(Status.Ok, testResult.Status);
        }
    }
}
