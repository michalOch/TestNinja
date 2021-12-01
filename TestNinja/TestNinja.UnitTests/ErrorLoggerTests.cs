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
    public class ErrorLoggerTests
    {
        [Test]
        public void Log_WhenCalled_SetLastErrorProperty()
        {
            // Arrange
            var logger = new ErrorLogger();
            var error = "error message";

            // Act
            logger.Log(error);

            // Assert
            Assert.That(logger.LastError, Is.EqualTo(error));
        }
    }
}
