using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTest.JsonReader;
using System.IO;

namespace RestTest.Library.Entity.Test
{
    [TestClass]
    public class BodyTest
    {
        [TestMethod]
        public void SimpleBodyShouldBeEquals()
        {
            var jsonReader = new JsonReader.JsonReaderBody();

            var jsonBody1 = File.ReadAllText("./simple_body.json");
            var jsonBody2 = File.ReadAllText("./simple_body.json");

            Assert.IsTrue(jsonReader.Read(jsonBody1).Equals(jsonReader.Read(jsonBody2)));
            Assert.IsTrue(jsonReader.Read(jsonBody2).Equals(jsonReader.Read(jsonBody1)));
        }  
        
        [TestMethod]
        public void SimpleBodyShouldNotBeEquals()
        {
            var jsonReader = new JsonReader.JsonReaderBody();

            var jsonBody1 = File.ReadAllText("./simple_body.json");
            var jsonBody2 = File.ReadAllText("./simple_body2.json");

            Assert.IsFalse(jsonReader.Read(jsonBody1).Equals(jsonReader.Read(jsonBody2)));
            Assert.IsFalse(jsonReader.Read(jsonBody2).Equals(jsonReader.Read(jsonBody1)));
        }

        [TestMethod]
        public void ReservedWord_Any_BodyShouldtBeEquals()
        {
            var jsonReader = new JsonReader.JsonReaderBody();

            var jsonBody1 = File.ReadAllText("./simple_body.json");
            var jsonBody2 = File.ReadAllText("./simple_body_any.json");

            Assert.IsTrue(jsonReader.Read(jsonBody1).Equals(jsonReader.Read(jsonBody2)));
            Assert.IsTrue(jsonReader.Read(jsonBody2).Equals(jsonReader.Read(jsonBody1)));
        }

        [TestMethod]
        public void ReservedWord_NUMBER_BodyShouldtBeEquals()
        {
            var jsonReader = new JsonReader.JsonReaderBody();

            var jsonBody1 = File.ReadAllText("./simple_body.json");
            var jsonBody2 = File.ReadAllText("./simple_body_number.json");

            Assert.IsTrue(jsonReader.Read(jsonBody1).Equals(jsonReader.Read(jsonBody2)));
            Assert.IsTrue(jsonReader.Read(jsonBody2).Equals(jsonReader.Read(jsonBody1)));


            var jsonBody3 = File.ReadAllText("./simple_body_item1_str.json");
            var jsonBody4 = File.ReadAllText("./simple_body_item1_$number.json");
            Assert.IsFalse(jsonReader.Read(jsonBody3).Equals(jsonReader.Read(jsonBody4)));
            Assert.IsFalse(jsonReader.Read(jsonBody4).Equals(jsonReader.Read(jsonBody3)));
        }

        [TestMethod]
        public void ReservedWord_Regex_BodyShouldtBeEquals()
        {
            var jsonReader = new JsonReader.JsonReaderBody();
            var jsonBody1 = File.ReadAllText("./simple_body_item1_number.json");
            var jsonBody2 = File.ReadAllText("./simple_body_item1_regex.json");
            Assert.IsTrue(jsonReader.Read(jsonBody1).Equals(jsonReader.Read(jsonBody2)));
            Assert.IsTrue(jsonReader.Read(jsonBody2).Equals(jsonReader.Read(jsonBody1)));

            var jsonBody3 = File.ReadAllText("./simple_body_item1_str.json");
            Assert.IsFalse(jsonReader.Read(jsonBody2).Equals(jsonReader.Read(jsonBody3)));
        }

        [TestMethod]
        public void ReservedWord_ComplexRegex_BodyShouldtBeEquals()
        {
            var jsonReader = new JsonReaderBody();
            var jsonBody1 = File.ReadAllText("./body_age.json");
            var jsonBody2 = File.ReadAllText("./body_age_regex.json");

            var body1 = jsonReader.Read(jsonBody1);
            var body2 = jsonReader.Read(jsonBody2);

            Assert.IsTrue(body1["item1"].Equals(body2["item1"]));
            Assert.IsTrue(body2["item1"].Equals(body1["item1"]));

            Assert.IsTrue(body1["item2"].Equals(body2["item1"]));
            Assert.IsTrue(body2["item1"].Equals(body1["item2"]));

            Assert.IsFalse(body1["item3"].Equals(body2["item1"]));
            Assert.IsFalse(body2["item1"].Equals(body1["item3"]));
        }
    }
}
