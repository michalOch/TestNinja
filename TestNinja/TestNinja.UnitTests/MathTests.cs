using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class MathTests
    {
        private Math _math;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            _math = new Math();
        }

        [Test]
        public void Add_WhenCalled_ReturnTheSumOfArguments()
        {
            // Act 
            var result = _math.Add(2, 1);

            // Assert
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Max_FirstArgumentIsGreater_ReturnTheFirstArgument()
        {
            // Act
            var result = _math.Max(2, 1);

            // Assert
            Assert.That(result, Is.EqualTo(2));
        }        
        
        [Test]
        public void Max_SecondArgumentIsGreater_ReturnTheSecondArgument()
        {

            // Act
            var result = _math.Max(1, 2);

            // Assert
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Max_ArgumentsAreEqual_ReturnTheSameArgument()
        {
            // Act
            var result = _math.Max(1, 1);

            // Assert
            Assert.That(result, Is.EqualTo(1));
        }
    }
}
