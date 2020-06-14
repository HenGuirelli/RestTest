using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace RestTest.Configuration.Test
{
    [TestClass]
    public class ConfigurationSequenceTest
    {
        [TestMethod]
        public void OnSequenceTest()
        {
            var conf = new Configuration("./sequence_test.json");

            Assert.AreEqual(1, conf.Sequences.Count());
            var test = conf.Sequences.Single();
            Assert.AreEqual(test.Name, "sequence_test_name");

            Assert.AreEqual(3, conf.Sequences.Single().Sequence.Count());
        }
    }
}
