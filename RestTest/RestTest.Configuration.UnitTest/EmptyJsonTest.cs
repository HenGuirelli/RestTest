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
    }
}
