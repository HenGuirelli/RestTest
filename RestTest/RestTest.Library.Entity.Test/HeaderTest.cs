using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestTest.Library.Entity.Test
{
    [TestClass]
    public class HeaderTest
    {
        [TestMethod]
        public void OnHeaderEquals_ShouldBeCaseInsensitive()
        {
            var header = new Header();
            header.Add("fullname", "Joel Miller");

            var anotherHeader = new Header();
            anotherHeader.Add("FullName", "Joel Miller");

            Assert.IsTrue(header.Equals(anotherHeader));
        }

        [TestMethod]
        public void OnHeaderEquals_ShouldCompareValues()
        {
            var header = new Header();
            header.Add("fullname", "Joel Miller");

            var anotherHeader = new Header();
            anotherHeader.Add("FullName", "Wrong Name");

            Assert.IsFalse(header.Equals(anotherHeader));
        }

        [TestMethod]
        public void OnHeaderEquals_ValuesShouldCaseSensitive()
        {
            var header = new Header();
            header.Add("fullname", "Joel Miller");

            var anotherHeader = new Header();
            anotherHeader.Add("FullName", "joel miller");

            Assert.IsFalse(header.Equals(anotherHeader));
        }

        [TestMethod]
        public void OnHeaderEquals_ShouldIgnoreSpecialWordNumber()
        {
            var header = new Header();
            header.Add("age", "35");

            var anotherHeader = new Header();
            anotherHeader.Add("age", "${NUMBER}");

            Assert.IsTrue(header.Equals(anotherHeader));
        }

        [TestMethod]
        public void OnHeaderEquals_ShouldIgnoreSpecialWordANY()
        {
            var header = new Header();
            header.Add("fullname", "Joel Miller");

            var anotherHeader = new Header();
            anotherHeader.Add("FullName", "${ANY}");

            Assert.IsTrue(header.Equals(anotherHeader));
        }
    }
}
