using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestTest.Configuration.Test
{
    [TestClass]
    public class FullConfigurationFileTest
    {
        [TestMethod]
        public void OnReadFullConfig()
        {
            // Not should throw exception
            var conf = new Configuration("./full_config.json");

            Assert.AreEqual(1, conf.Sequences.Count());
            Assert.AreEqual(3, conf.Sequences.Single().Sequence.Count());

            Assert.AreEqual(1, conf.Uniques.Count());
        }

        [TestMethod]
        public void OnDuplicatedKeys()
        {
            try
            {
                var conf = new Configuration("./duplicated_keys_config.json");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("config duplicated names 'dupname'", ex.Message);
                return;
            }
            Assert.Fail();
        }
    }
}
