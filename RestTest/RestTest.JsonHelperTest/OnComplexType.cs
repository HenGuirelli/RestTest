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
            var simpleJsonString = "{ \"key\": { \"innerKeyInt\": 1, \"innerKeyString\": \"plain text\" }  }";
            var json = new Json(simpleJsonString);

            var innerObject = json["key"];

            Assert.AreEqual(1, int.Parse(innerObject["innerKeyInt"].ToString()));
            Assert.AreEqual("plain text", innerObject["innerKeyString"].ToString());
        }

        [TestMethod]
        public void OnValueIsListOfInt()
        {
            var simpleJsonString = "{ \"key\": [ 1, 2, 3 ]  }";
            var json = new Json(simpleJsonString);

            var innerObject = json["key"];

            Assert.AreEqual(1, innerObject.ToList<int>().First());
            Assert.AreEqual(3, innerObject.ToList<int>().Last());
        }
    }
}
