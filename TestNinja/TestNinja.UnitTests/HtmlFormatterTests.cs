using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class HtmlFormatterTests
    {
        [Test]
        public void FormatAsBold_WhenCalled_ShouldEncloseTheStringWithStrongElement()
        {
            // Arrange
            var formatter = new HtmlFormatter();
            var content = "abc";

            // Act
            var result = formatter.FormatAsBold(content);

            // Specific assertion
            Assert.That(result, Is.EqualTo($"<strong>{content}</strong>"));

            // More general assertion
            Assert.That(result, Does.StartWith("<strong>"));
            Assert.That(result, Does.Contain(content));
            Assert.That(result, Does.EndWith("</strong>"));
        }
    }
}
