using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.Library;

namespace RestTest.Tests
{

    [TestClass]
    public class CasoDeUsoTest
    {
        [TestMethod]
        public void OnErrorMessage()
        {
            var useCase = new UseCase(new { jsonKey1 =  "jsonValue1", jsonKey2 = 3 }, new { Success = true });
            Assert.IsTrue(useCase.CompareResponse(new { Success = true }));
            Assert.IsFalse(useCase.CompareResponse(new { Success = "other value" }));
        }
    }
}
