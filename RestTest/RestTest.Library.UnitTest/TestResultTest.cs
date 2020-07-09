using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.Library.Entity;
using RestTestResult = RestTest.Library.Entity.Test.TestResult;
using RestTest.Library.Entity.Http;
using RestTest.Library.Entity.Test;

namespace RestTest.Library.Test
{
    [TestClass]
    public class TestResultTest
    {
        [TestMethod]
        public void OnCtor_EmptyBodyJson()
        {
            var body = new Body();
            var validation = new Validation(body, default, default, default, status: 200, default, default);
            var response = new Response(200, Body.Empty, Cookies.Empty, Header.Empty);
            var testResult = new RestTestResult("name", validation, response);

            Assert.AreEqual(Status.Ok, testResult.Status);
        }
        
        [TestMethod]
        public void OnCtor_SimpleBodyJson()
        {
            var jsonReader = new JsonReader.JsonReaderBody();
            var bodyString = "{ name: \"Ellie\" }";
            var body = jsonReader.Read(bodyString);

            var validation = new Validation(body, default, default, default, status: 200, default, default);
            var response = new Response(200, jsonReader.Read(bodyString), Cookies.Empty, Header.Empty);

            var testResult = new RestTestResult("name", validation, response);
            Assert.AreEqual(Status.Ok, testResult.Status);


            response = new Response(200, jsonReader.Read("{ name: \"Marlene\" }"), Cookies.Empty, Header.Empty);
            testResult = new RestTestResult("name", validation, response);
            Assert.AreEqual(Status.Fail, testResult.Status);
        }  
        
        [TestMethod]
        public void OnCtor_ComplexBodyJson()
        {
            var jsonReader = new JsonReader.JsonReaderBody();
            var bodyString = "{ person: { name: \"Ellie\", age: 14 } }";
            var body = jsonReader.Read(bodyString);

            var validation = new Validation(body, default, default, default, status: 200, default, default);
            var response = new Response(200, jsonReader.Read(bodyString), Cookies.Empty, Header.Empty);

            var testResult = new RestTestResult("name", validation, response);
            Assert.AreEqual(Status.Ok, testResult.Status);


            response = new Response(200, jsonReader.Read("{ person: { name: \"Joel\", age: 40 } }"), Cookies.Empty, Header.Empty);
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
