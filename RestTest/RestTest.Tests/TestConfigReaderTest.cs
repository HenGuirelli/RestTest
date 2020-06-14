using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.Library.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestTest.Tests
{
    [TestClass]
    public class TestConfigReaderTest
    {
        [TestMethod]
        public void ReadFromFile()
        {
            var testReader = new TestConfigReader();
            testReader.ReadFromFile("./ConfigTestUnique.json");


            var test = testReader.Single();
            Assert.AreEqual(TestType.unique_test, test.type);
            Assert.AreEqual("request name", test.name);
            Assert.AreEqual(Method.Get, test.method);
            Assert.AreEqual("http://127.0.0.1:8081/api/resource", test.url);


            // Header
            Assert.AreEqual("application/json", test.header["Content-Type"]);
            Assert.AreEqual("PUT,GET,POST,DELETE,OPTIONS", test.header["Access-Control-Allow-Methods"]);

            // Query string
            Assert.AreEqual(123L, test.query_string["code"]);
        }
    }
}
