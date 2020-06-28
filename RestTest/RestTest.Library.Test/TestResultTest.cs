using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.Library.Entity;
using RestTest.JsonHelper;
using RestTestResult = RestTest.Library.Entity.TestResult;

namespace RestTest.Library.Test
{
    [TestClass]
    public class TestResultTest
    {
        [TestMethod]
        public void OnCtor_EmptyBodyJson()
        {
            var body = new Json("");
            var validation = new Validation(body, default, default, default, status: 200, default, default);
            var response = new Response(200, Body.Empty, Cookies.Empty, Header.Empty);
            var testResult = new RestTestResult("name", validation, response);

            Assert.AreEqual(Status.Ok, testResult.Status);
        }
        
        [TestMethod]
        public void OnCtor_SimpleBodyJson()
        {
            var bodyString = "{ name: \"Ellie\" }";
            var body = new Json(bodyString);

            var validation = new Validation(body, default, default, default, status: 200, default, default);
            var response = new Response(200, new Body(bodyString), Cookies.Empty, Header.Empty);

            var testResult = new RestTestResult("name", validation, response);
            Assert.AreEqual(Status.Ok, testResult.Status);


            response = new Response(200, new Body("{ name: \"Marlene\" }"), Cookies.Empty, Header.Empty);
            testResult = new RestTestResult("name", validation, response);
            Assert.AreEqual(Status.Fail, testResult.Status);
        }  
        
        [TestMethod]
        public void OnCtor_ComplexBodyJson()
        {
            var bodyString = "{ person: { name: \"Ellie\", age: 14 } }";
            var body = new Json(bodyString);

            var validation = new Validation(body, default, default, default, status: 200, default, default);
            var response = new Response(200, new Body(bodyString), Cookies.Empty, Header.Empty);

            var testResult = new RestTestResult("name", validation, response);
            Assert.AreEqual(Status.Ok, testResult.Status);


            response = new Response(200, new Body("{ person: { name: \"Joel\", age: 40 } }"), Cookies.Empty, Header.Empty);
            testResult = new RestTestResult("name", validation, response);
            Assert.AreEqual(Status.Fail, testResult.Status);
        }

        [TestMethod]
        public void OnCtor_Status()
        {
            var validation = new Validation(default, default, default, default, status: 200, default, default);
            var response = new Response(200, Body.Empty, Cookies.Empty, Header.Empty);
            var testResult = new RestTestResult("name", validation, response);
            Assert.AreEqual(Status.Ok, testResult.Status);
        }
    }
}
