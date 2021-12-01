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
        private ErrorLogger _logger;

        [SetUp]
        public void SetUp()
        {
            _logger = new ErrorLogger();
        }

        [Test]
        public void Log_WhenCalled_SetLastErrorProperty()
        {
            // Arrange
            var error = "error message";

            // Act
            _logger.Log(error);

            // Assert
            Assert.That(_logger.LastError, Is.EqualTo(error));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_InvalidError_ThrowArgumentNullException(string error)
        {
            Assert.That(() => _logger.Log(error), Throws.ArgumentNullException);
            //Assert.That(() => _logger.Log(error), Throws.TypeOf<DivideByZeroException>());
        }

        [Test]
        public void Log_ValidError_RaisedErrorLoggedEvent()
        {
            // Arrange
            var errorMessage = "a";
            var id = Guid.Empty;
            // Subscribe to tested event before acting
            _logger.ErrorLogged += (sender, args) =>{ id = args;};

            // Act
            _logger.Log(errorMessage);

            // Assert
            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }
    }
}
