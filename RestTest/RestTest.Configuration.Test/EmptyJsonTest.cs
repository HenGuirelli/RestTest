using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RestTest.Configuration.Test
{
    [TestClass]
    public class EmptyJsonTest
    {
        [TestMethod]
        public void OnEmptyJson()
        {
            try
            {
                var conf = new Configuration("./empty.json");                
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Json is empty", ex.Message);
                return;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void OnTestTypeNotDefined()
        {
            try
            {
                var conf = new Configuration("./no_test_type.json");
            }
            catch(Exception ex)
            {
                Assert.AreEqual("Test 'test_name' need a type. Example: \"type\": \"unique_test\"", ex.Message);
                return;
            }

            Assert.Fail();
        }
    }
}
