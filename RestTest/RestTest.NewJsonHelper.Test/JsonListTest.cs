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
    }
}
