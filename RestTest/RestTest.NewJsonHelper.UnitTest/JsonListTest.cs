using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace RestTest.NewJsonHelper.Test
{
    [TestClass]
    public class JsonListTest
    {
        [TestMethod]
        public void OnAddList()
        {
            var jsonList = new JsonList();
            jsonList.Add(new JsonLong(1), new JsonLong(2), new JsonLong(3));

            Assert.AreEqual(1, int.Parse(jsonList[0].ToString()));
            Assert.AreEqual(2, int.Parse(jsonList[1].ToString()));
            Assert.AreEqual(3, int.Parse(jsonList[2].ToString()));
            Assert.AreEqual(3, (jsonList.GetValue() as List<Json>).Count);
        }

        [TestMethod]
        public void OnEquals_PrimitiveTypes()
        {
            var jsonList1 = new JsonList();
            jsonList1.Add(new JsonLong(1), new JsonLong(2), new JsonLong(3));

            var jsonList2 = new JsonList();
            jsonList2.Add(new JsonLong(1), new JsonLong(2), new JsonLong(3));

            Assert.IsTrue(jsonList1.Equals(jsonList2));
        }

        [TestMethod]
        public void OnEquals_JsonTypes()
        {
            var jsonList1 = new JsonList();
            var jsonObj1 = new JsonObject();
            jsonObj1.Add(new JsonString("key", "value 1"));
            jsonList1.Add(jsonObj1);

            var jsonList2 = new JsonList();
            var jsonObj2 = new JsonObject();
            jsonObj2.Add(new JsonString("key", "value 1"));
            jsonList2.Add(jsonObj2);

            Assert.IsTrue(jsonList1.Equals(jsonList2));

            var jsonList3 = new JsonList();
            var jsonObj3 = new JsonObject();
            jsonObj3.Add(new JsonString("key", "value 2"));
            jsonList3.Add(jsonObj3);
            Assert.IsFalse(jsonList1.Equals(jsonList3));
        }

        [TestMethod]
        public void OnToStringLong()
        {
            var jsonList = new JsonList();
            jsonList.Add(new JsonLong(1), new JsonLong(2), new JsonLong(3));

            Assert.AreEqual("[1, 2, 3]", jsonList.ToString());
        }

        [TestMethod]
        public void OnToStringString()
        {
            var jsonList = new JsonList();
            jsonList.Add(new JsonString("value 1"), new JsonString("value 2"));

            Assert.AreEqual("[\"value 1\", \"value 2\"]", jsonList.ToString());
        }
    }
}
