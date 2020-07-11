using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.Library.Entity.Http;
using RestTest.Library.SequenceDependency.Evaluators;
using RestTest.NewJsonHelper;
using System;

namespace RestTest.Library.SequenceDependency.UnitTest
{
    [TestClass]
    public class BodyEvaluatorTest
    {
        [TestMethod]
        public void OnEvaluateBodyEvaluator()
        {
            var evaluator = new BodyEvaluator();
            var body = new Body();
            body.Add(new JsonString("key_str", "value"));
            body.Add(new JsonLong("key_int", 20));

            var property = new JsonObject("key_obj");
            property.Add(new JsonString("key_str", "value2"));
            body.Add(property);


            var actual = evaluator.Evaluate("${call1.response.body.key_str}", CreateTestResult("call1", body));
            Assert.AreEqual("value", actual);

            actual = evaluator.Evaluate("${call1.response.body.key_int}", CreateTestResult("call1", body));
            Assert.AreEqual(20, long.Parse(actual));

            actual = evaluator.Evaluate("${call1.response.body.key_obj.key_str}", CreateTestResult("call1", body));
            Assert.AreEqual("value2", actual);
        }

        [TestMethod]
        public void OnEvaluate_Error()
        {
            var evaluator = new HeaderEvaluator();
            var body = new Body();

            try
            {
                var actual = evaluator.Evaluate("${call1.response.body.key_str}", CreateTestResult("call1", body));
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Depedency ${call1.response.body.key_str} not found", ex.Message);
                return;
            }
            Assert.Fail();
        }

        private Entity.Test.TestResult CreateTestResult(string name, Entity.Http.Body body)
        {
            var validation = new Entity.Test.Validation();
            var response = new Response(200, body, new Cookies(), new Header());
            return new Entity.Test.TestResult(name, validation, response);
        }
    }
}
