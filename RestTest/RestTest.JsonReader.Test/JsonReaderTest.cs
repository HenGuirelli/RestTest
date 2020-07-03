using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.NewJsonHelper;
using System.Collections.Generic;

namespace RestTest.JsonReader.Test
{
    [TestClass]
    public class JsonReaderTest
    {
        [TestMethod]
        public void ReadJsonInt()
        {
            var reader = new JsonReader();
            var body = reader.ReadByFile(@".\simple_json_int.json");

            Assert.AreEqual(1, int.Parse(body["int"].GetValue().ToString()));
            Assert.AreEqual(2, int.Parse(body["int2"].GetValue().ToString()));
        }

        [TestMethod]
        public void ReadJsonString()
        {
            var reader = new JsonReader();
            var body = reader.ReadByFile(@".\simple_json_str.json");

            Assert.AreEqual("value", body["key"].GetValue().ToString());
        }

        [TestMethod]
        public void ReadJsonList()
        {
            var reader = new JsonReader();
            var body = reader.ReadByFile(@".\simple_json_list.json");

            Assert.AreEqual(4, (body["key"].GetValue() as List<Json>).Count);
        }

        [TestMethod]
        public void ReadJsonObject()
        {
            var reader = new JsonReader();
            var body = reader.ReadByFile(@".\simple_json_obj.json");

            Assert.AreEqual("Robson", body["person"]["name"].GetValue().ToString());
            Assert.AreEqual(30, int.Parse(body["person"]["age"].GetValue().ToString()));
            Assert.AreEqual("Cleyton", body["person"]["children"]["name"].GetValue().ToString());
            Assert.AreEqual(14, int.Parse(body["person"]["children"]["age"].GetValue().ToString()));
        }
    }
}
