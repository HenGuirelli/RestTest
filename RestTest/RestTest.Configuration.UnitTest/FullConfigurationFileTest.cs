using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RestTest.Configuration.Test
{
    [TestClass]
    public class FullConfigurationFileTest
    {
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

        [TestMethod]
        public void OnDuplicatedKeyNoName_ShouldIgnoreTests()
        {
            try
            {
                var conf = new Configuration("./duplicated_key_no_names.json");
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}
