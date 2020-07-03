using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.HttpServer.Test;
using RestTest.Library.Entity;
using System;

namespace RestTest.Library.SequenceDependency.Test
{
    [TestClass]
    public class DependencyDetectorTest
    {
        private const int Port = 8091;
        private volatile bool _testFinished;
        HttpServerHelp _server;

        [TestInitialize]
        public void CreateHttpServer()
        {
            _testFinished = false;
            _server = new HttpServerHelp();
            _server.CreateHttpServer(Port);
        }

        [TestCleanup]
        public void StopHttpServer()
        {
            _server.StopHttpServer();
        }

        [TestMethod]
        public void OnDependencyDetector_IsDependency()
        {
            var dep = new DependencyDetector();

            Assert.IsTrue(dep.IsDependency("${call1.response}"));
            Assert.IsTrue(dep.IsDependency("${call2.request}"));
            Assert.IsFalse(dep.IsDependency("${ANY}"));
            Assert.IsFalse(dep.IsDependency("${NUMBER}"));
        }

        [TestMethod]
        public void OnDependencyDetector_GetDependencyName()
        {
            var dep = new DependencyDetector();

            Assert.AreEqual("call1", dep.GetDependencyName("${call1.response}"));
            Assert.AreEqual("call2", dep.GetDependencyName("${call2.response}"));

            try
            {
                // throw exception on not recognized a dependency
                dep.GetDependencyName("${ANY}");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("value is not a dependency", ex.Message);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void OnEvaluateDependency()
        {
            var dep = new DependencyDetector();
            var jsonReader = new JsonReader.JsonReader();

            var testResult = new Entity.TestResult("call1",
                new Validation(),
                new Response(200, jsonReader.Read("{ id: 1314, name: \"Robson\", childrens: { name: \"Cleyton\", age: 14 } }"), new Cookies(), new Header()));

            Assert.AreEqual("1314", dep.EvaluateDependency("${call1.response.body.id}", testResult));
            Assert.AreEqual("Robson", dep.EvaluateDependency("${call1.response.body.name}", testResult));

            Assert.AreEqual("Cleyton", dep.EvaluateDependency("${call1.response.body.childrens.name}", testResult));
            Assert.AreEqual("14", dep.EvaluateDependency("${call1.response.body.childrens.age}", testResult));
        }
    }
}
