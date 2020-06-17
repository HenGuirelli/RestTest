using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestTest.Configuration.Test
{
    [TestClass]
    public class ConfigurationUniqueTest
    {
        [TestMethod]
        public void OnUniqueConfiguration()
        {
            var conf = new Configuration("./unique_test.json");

            Assert.AreEqual(1, conf.Uniques.Count());
            var test = conf.Uniques.Single();
            Assert.AreEqual(test.Name, "unique_test_name");

            Assert.AreEqual(2, test.Validation.Body.Count());
            Assert.AreEqual("client_status", test.Validation.Body.First().Key);
            Assert.AreEqual("authenticated", test.Validation.Body.First().Value);
            Assert.AreEqual("id", test.Validation.Body.Last().Key);
            Assert.AreEqual("${NUMBER}", test.Validation.Body.Last().Value);
        }

        [TestMethod]
        public void OnManyUniqueConfigurations()
        {
            var conf = new Configuration("./many_unique_test.json");

            Assert.AreEqual(2, conf.Uniques.Count());
            var test = conf.Uniques.Last();
            Assert.AreEqual(test.Name, "unique_test_name2");
        }

        [TestMethod]
        public void OnReadConfigurationFileNotFound()
        {
            try
            {
                var conf = new Configuration("./404file.json");
            }
            catch(Exception ex)
            {
                Assert.AreEqual("configuration file not found", ex.Message);
                return;
            }

            Assert.Fail("Not throw Exception, expected throw");
        }

        [TestMethod]
        public void OnComplexBody()
        {
            var conf = new Configuration("./unique_test_complex_body.json");

            var obj = Newtonsoft.Json.JsonConvert.DeserializeObject("{" +
              "\"attrInt\": 1," +
              "\"attrStr\": \"value\"," +
              "\"atttrList\": [\"item 1\", \"item 2\", \"item 3\"]," +
              "\"attrObj\": {" +
                "\"innerAttr1\": 1," +
                "\"innerAttr2\": 2," +
                "\"innerAttr3WithOtherObject\": {" +
                            "\"innerAttr1List\": [1, 2, 3, 4]" +
                "}" +
              "}" +
             "}");
            
            var other = Newtonsoft.Json.JsonConvert.DeserializeObject("{" +
              "\"attrInt\": 1," +
              "\"attrStr\": \"value\"," +
              "\"atttrList\": [\"item 1\", \"item 2\", \"item 3\"]," +
              "\"attrObj\": {" +
                "\"innerAttr1\": 1," +
                "\"innerAttr2\": 2," +
                "\"innerAttr3WithOtherObject\": {" +
                            "\"innerAttr1List\": [1, 2, 3, 4]" +
                "}" +
              "}" +
             "}");

            var unique = conf.Uniques.Single();
            Assert.AreEqual(obj as JObject, other as JObject);
        }
    }
}
