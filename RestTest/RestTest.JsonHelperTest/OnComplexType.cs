using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.JsonHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestTest.JsonHelperTest
{
    [TestClass]
    public class OnComplexType
    {
        [TestMethod]
        public void OnValueIsObject()
        {
            var jsonString = "{ \"key\": { \"innerKeyInt\": 1, \"innerKeyString\": \"plain text\" }  }";
            var json = new Json(jsonString);

            var innerObject = json["key"];

            Assert.AreEqual(1, int.Parse(innerObject["innerKeyInt"].ToString()));
            Assert.AreEqual("plain text", innerObject["innerKeyString"].ToString());
        }

        [TestMethod]
        public void OnValueIsObject2()
        {
            var jsonString = "{ \"key\": { \"innerKeyInt\": 1, \"innerKeyString\": \"plain text\", \"innerKeyObject\": { \"key\": 3 } }  }";
            var json = new Json(jsonString);

            var innerObject = json["key"];

            Assert.AreEqual(1, int.Parse(innerObject["innerKeyInt"].ToString()));
            Assert.AreEqual("plain text", innerObject["innerKeyString"].ToString());

            var innerObject2 = innerObject["innerKeyObject"];
            Assert.AreEqual(3, int.Parse(innerObject2["key"].ToString()));
        }

        [TestMethod]
        public void OnValueIsListOfInt()
        {
            var jsonString = "{ \"key\": [ 1, 2, 3 ]  }";
            var json = new Json(jsonString);

            var innerObject = json["key"];

            Assert.AreEqual(1, innerObject.ToList<int>().First());
            Assert.AreEqual(3, innerObject.ToList<int>().Last());
        }
    }
}
