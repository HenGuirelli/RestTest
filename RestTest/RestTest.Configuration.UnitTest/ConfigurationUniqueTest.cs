using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.NewJsonHelper;
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

            Assert.AreEqual(2, test.Validation.Body.Keys.Count());
            Assert.AreEqual("client_status", test.Validation.Body.Keys.First());
            Assert.AreEqual("authenticated", test.Validation.Body["client_status"].GetValue().ToString());
            Assert.AreEqual("id", test.Validation.Body.Keys.Last());
            Assert.AreEqual("${NUMBER}", test.Validation.Body["id"].GetValue().ToString());
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
                Assert.AreEqual("configuration file ./404file.json not found", ex.Message);
                return;
            }

            Assert.Fail("Not throw Exception, expected throw");
        }

        [TestMethod]
        public void OnComplexBody()
        {
            var conf = new Configuration("./unique_test_complex_body.json");
            var unique = conf.Uniques.Single();

            Assert.AreEqual(1, int.Parse(unique.Body["attrObj"]["innerAttr1"].GetValue().ToString()));
            Assert.AreEqual(2, int.Parse(unique.Body["attrObj"]["innerAttr2"].GetValue().ToString()));
            var list = unique.Body["attrObj"]["innerAttr3WithOtherObject"]["innerAttr1List"].GetValue() as List<Json>;
            Assert.AreEqual(4, int.Parse(list.Last().GetValue().ToString()));
        }
    }
}
