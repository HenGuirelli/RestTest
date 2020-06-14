using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.Library.Config;

namespace RestTest.Tests
{
    [TestClass]
    public class MethodTest
    {
        [TestMethod]
        public void OnParse_ShouldUppercase()
        {
            var method = Method.Get;
            Assert.AreEqual("GET", method.Parse());
            Assert.AreEqual("POST", Method.Post.Parse());
        }

        [TestMethod]
        public void OnToMethod_ShouldUppercase()
        {
            Assert.AreEqual(Method.Get, "GET".ToMethod());
            Assert.AreEqual(Method.Post, "POST".ToMethod());
            Assert.AreEqual(default, "".ToMethod());
            Assert.AreEqual(default, (null as string).ToMethod());
        }
    }
}
