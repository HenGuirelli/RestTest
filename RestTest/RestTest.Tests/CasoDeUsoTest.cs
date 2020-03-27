using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.Library;

namespace RestTest.Tests
{

    [TestClass]
    public class CasoDeUsoTest
    {
        [TestMethod]
        public void OnCompareResponse_ShouldCompareTypes()
        {
            var useCase = new UseCase(null, new { Success = true });
            Assert.IsTrue(useCase.CompareResponse(new { Success = true }));
            Assert.IsFalse(useCase.CompareResponse(new { Success = 1 }));
            Assert.IsFalse(useCase.CompareResponse(new { Success = 1m }));
            Assert.IsFalse(useCase.CompareResponse(new { Success = "non empty string" }));
        }

        [TestMethod]
        public void OnCompareResponse_ShouldCompareComplexValues()
        {
            var useCase1 = new UseCase(null, new { Int = 1 });
            Assert.IsTrue(useCase1.CompareResponse(new { Int = 1 }));
            Assert.IsFalse(useCase1.CompareResponse(new { Int = 2 }));
            Assert.IsFalse(useCase1.CompareResponse(new { Int = -1 }));
            Assert.IsFalse(useCase1.CompareResponse(new { Int = int.MaxValue }));
            
            var useCase2 = new UseCase(null, new { Bool = true });
            Assert.IsTrue(useCase2.CompareResponse(new { Bool = true }));
            Assert.IsFalse(useCase2.CompareResponse(new { Bool = false }));
        }

        [TestMethod]
        public void OnCompareResponse_ShouldComparePrimitiveValues()
        {
            var useCase1 = new UseCase(null, "raw text");
            Assert.IsTrue(useCase1.CompareResponse("raw text"));
            Assert.IsFalse(useCase1.CompareResponse("other raw text"));
            Assert.IsFalse(useCase1.CompareResponse(10));
            Assert.IsFalse(useCase1.CompareResponse(true));

            var useCase2 = new UseCase(null, 10);
            Assert.IsTrue(useCase2.CompareResponse(10));
            Assert.IsFalse(useCase2.CompareResponse(0));
            Assert.IsFalse(useCase2.CompareResponse(-1));
            Assert.IsFalse(useCase2.CompareResponse(1));
        }

        [TestMethod]
        public void OnCompareResponse_NotHaveResponse()
        {
            var useCase1 = new UseCase(null, "raw text");
            Assert.IsFalse(useCase1.CompareResponse(null));

            var useCase2 = new UseCase(null, null);
            Assert.IsTrue(useCase2.CompareResponse(null));
            Assert.IsFalse(useCase2.CompareResponse("any response wrong"));
        }
    }
}
