using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.JsonHelper;

namespace RestTest.JsonHelperTest
{
    [TestClass]
    public class SimpleJsonConverter
    {
        [TestMethod]
        public void OnSimpleString()
        {
            var simpleJsonString = "{ \"key\": \"plain text\"  }";
            var json = new Json(simpleJsonString);
            Assert.AreEqual("plain text", json["key"].ToString());
        }

        [TestMethod]
        public void OnSimpleInt()
        {
            var simpleJsonInt = "{ \"key\": 1  }";
            var json = new Json(simpleJsonInt);
            Assert.AreEqual(1, int.Parse(json["key"].ToString()));
        }

        [TestMethod]
        public void OnSimpleFloat()
        {
            var simpleJsonString = "{ \"key\": 1.5  }";
            var json = new Json(simpleJsonString);
            Assert.AreEqual(1.5, float.Parse(json["key"].ToString()));
        }

        [TestMethod]
        public void OnEmptyJson()
        {
            var json = new Json("{}");
            Assert.IsTrue(json.Compare("{}"));

            var jsonEmpty = new Json(string.Empty);
            Assert.IsTrue(jsonEmpty.Compare("{}"));
        }
    }
}
