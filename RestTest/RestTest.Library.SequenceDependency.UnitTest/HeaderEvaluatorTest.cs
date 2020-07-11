using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.Library.Entity.Http;
using RestTest.Library.SequenceDependency.Evaluators;
using RestTest.NewJsonHelper;
using System;

namespace RestTest.Library.SequenceDependency.UnitTest
{
    [TestClass]
    public class HeaderEvaluatorTest
    {
        [TestMethod]
        public void OnEvaluate_ShouldFindResult()
        {
            var evaluator = new HeaderEvaluator();
            var header = new Header();
            header.Add(new JsonString("key_str", "value"));
            header.Add(new JsonLong("key_int", 20));


            var actual = evaluator.Evaluate("${call1.response.header.key_str}", CreateTestResult("call1", header));
            Assert.AreEqual("value", actual);

            actual = evaluator.Evaluate("${call1.response.header.key_int}", CreateTestResult("call1", header));
            Assert.AreEqual(20, long.Parse(actual));
        }

        [TestMethod]
        public void OnEvaluate_Error()
        {
            var evaluator = new HeaderEvaluator();
            var header = new Header();

            try
            {
                var actual = evaluator.Evaluate("${call1.response.header.key_str}", CreateTestResult("call1", header));
            }
            catch(Exception ex)
            {
                Assert.AreEqual("Depedency ${call1.response.header.key_str} not found", ex.Message);
                return;
            }
            Assert.Fail();
        }

        private Entity.Test.TestResult CreateTestResult(string name, Header header)
        {
            var validation = new Entity.Test.Validation();
            var response = new Response(200, new Body(), new Cookies(), header);
            return new Entity.Test.TestResult(name, validation, response);
        }
    }
}
