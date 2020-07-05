using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RestTest.Library.Entity.Test
{
    [TestClass]
    public class TextFormatterTest
    {
        [TestMethod]
        public void OnRemoveNewLine()
        {
            var text = "one line\nother line";
            var textFormatter = new TextFormatter(text)
                                    .RemoveNewLine();

            Assert.AreEqual("one line other line", textFormatter.Value);
        }

        [TestMethod]
        public void OnRemoveEspecialCharacters()
        {
            var text = "one line\r\r\tother line";
            var textFormatter = new TextFormatter(text)
                                    .RemoveEspecialCharacters();

            Assert.AreEqual("one line other line", textFormatter.Value);
        }

        [TestMethod]
        public void OnRemoveMulipleSpaces()
        {
            var text = "one     line    other   line";
            var textFormatter = new TextFormatter(text)
                                    .RemoveMultipleSpaces();

            Assert.AreEqual("one line other line", textFormatter.Value);
        }

        [TestMethod]
        public void OnTrim()
        {
            var text = "   one line other line   ";
            var textFormatter = new TextFormatter(text)
                                    .Trim();

            Assert.AreEqual("one line other line", textFormatter.Value);
        }
    }
}
