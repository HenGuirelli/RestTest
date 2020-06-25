using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace RestTest.Library.Entity.Test
{
    [TestClass]
    public class BodyTest
    {
        [TestMethod]
        public void SimpleBodyShouldBeEquals()
        {
            var jsonBody1 = File.ReadAllText("./simple_body.json");
            var jsonBody2 = File.ReadAllText("./simple_body.json");

            Assert.IsTrue(new Body(jsonBody1).Equals(new Body(jsonBody2)));
        }  
        
        [TestMethod]
        public void SimpleBodyShouldNotBeEquals()
        {
            var jsonBody1 = File.ReadAllText("./simple_body.json");
            var jsonBody2 = File.ReadAllText("./simple_body2.json");

            Assert.IsFalse(new Body(jsonBody1).Equals(new Body(jsonBody2)));
        }

        [TestMethod]
        public void ReservedWord_Any_BodyShouldtBeEquals()
        {
            var jsonBody1 = File.ReadAllText("./simple_body.json");
            var jsonBody2 = File.ReadAllText("./simple_body_any.json");

            Assert.IsTrue(new Body(jsonBody1).Equals(new Body(jsonBody2)));
        }

        [TestMethod]
        public void ReservedWord_NUMBER_BodyShouldtBeEquals()
        {
            var jsonBody1 = File.ReadAllText("./simple_body.json");
            var jsonBody2 = File.ReadAllText("./simple_body_number.json");

            Assert.IsTrue(new Body(jsonBody1).Equals(new Body(jsonBody2)));
        }
    }
}
