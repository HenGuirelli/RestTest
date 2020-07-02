using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace RestTest.NewJsonHelper.Test
{
    [TestClass]
    public class JsonTest
    {
        [TestMethod]
        public void OnAdd()
        {
            var jsonObject = new JsonObject();
            jsonObject.Add(new JsonLong("age", 30));
            jsonObject.Add(new JsonString("name", "Cleyton"));

            Assert.AreEqual(2, jsonObject.Keys.Count());
            Assert.AreEqual(30, int.Parse(jsonObject["age"].ToString()));
            Assert.AreEqual("Cleyton", jsonObject["name"].ToString());
        }

        [TestMethod]
        public void OnRemove()
        {
            var jsonObject = new JsonObject();
            jsonObject.Add(new JsonLong("age", 30));
            jsonObject.Add(new JsonString("name", "Cleyton"));

            jsonObject.Remove("age");
            jsonObject.Remove("name");

            Assert.AreEqual(0, jsonObject.Keys.Count());
        }
    }
}
