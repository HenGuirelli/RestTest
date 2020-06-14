using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.Library.Config;

namespace RestTest.Tests
{
    [TestClass]
    public class TestTypeTest
    {
        [TestMethod]
        public void OnParse_ShouldUppercase()
        {
            var method = TestType.unique_test;
            Assert.AreEqual("UNIQUE", method.Parse());
            Assert.AreEqual("SET", TestType.sequence_test.Parse());
        }

        [TestMethod]
        public void OnToTestType_ShouldUppercase()
        {
            Assert.AreEqual(TestType.unique_test, "UNIQUE".ToTestType());
            Assert.AreEqual(TestType.sequence_test, "SET".ToTestType());
            Assert.AreEqual(default, "".ToTestType());
            Assert.AreEqual(default, (null as string).ToTestType());
        }
    }
}
