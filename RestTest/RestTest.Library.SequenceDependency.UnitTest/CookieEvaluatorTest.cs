using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.Library.Entity.Http;
using RestTest.Library.SequenceDependency.Evaluators;
using RestTest.NewJsonHelper;
using System;

namespace RestTest.Library.SequenceDependency.UnitTest
{
    [TestClass]
    public class CookieEvaluatorTest
    {
        [TestMethod]
        public void OnEvaluate_ShouldFindResult()
        {
            var evaluator = new CookieEvaluator();
            var cookies = new Cookies();
            cookies.Add(new JsonString("key_str", "value"));
            cookies.Add(new JsonLong("key_int", 20));


            var actual = evaluator.Evaluate("${call1.response.cookies.key_str}", CreateTestResult("call1", cookies));
            Assert.AreEqual("value", actual);

            actual = evaluator.Evaluate("${call1.response.cookie.key_int}", CreateTestResult("call1", cookies));
            Assert.AreEqual(20, long.Parse(actual));
        }

        [TestMethod]
        public void OnEvaluate_Error()
        {
            var evaluator = new CookieEvaluator();
            var cookies = new Cookies();

            try
            {
                var actual = evaluator.Evaluate("${call1.response.cookies.key_str}", CreateTestResult("call1", cookies));
            }
            catch(Exception ex)
            {
                Assert.AreEqual("Depedency ${call1.response.cookies.key_str} not found", ex.Message);
                return;
            }
            Assert.Fail();
        }

        private Entity.Test.TestResult CreateTestResult(string name, Cookies cookies)
        {
            var validation = new Entity.Test.Validation();
            var response = new Response(200, new Body(), cookies, new Header());
            return new Entity.Test.TestResult(name, validation, response);
        }
    }
}
