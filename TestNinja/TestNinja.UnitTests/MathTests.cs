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

        [Test]
        public void Add_WhenCalled_ReturnTheSumOfArguments()
        {
            // Arrange        
            var math = new Math();
            int a = 1, b = 2;

            // Act 
            var result = math.Add(a, b);

            // Assert
            Assert.That(result, Is.EqualTo(a+b));
        }

        [Test]
        public void Max_FirstArgumentIsGreater_ReturnTheFirstArgument()
        {
            // Arrange
            int a = 2, b = 1;
            var math = new Math();

            // Act
            var result = math.Max(a, b);

            // Assert
            Assert.That(result, Is.EqualTo(a));
        }        
        
        [Test]
        public void Max_SecondArgumentIsGreater_ReturnTheSecondArgument()
        {
            // Arrange
            int a = 1, b = 2;
            var math = new Math();

            // Act
            var result = math.Max(a, b);

            // Assert
            Assert.That(result, Is.EqualTo(b));
        }

        [Test]
        public void Max_ArgumentsAreEqual_ReturnTheSameArgument()
        {
            // Arrange
            int a = 1, b = 1;
            var math = new Math();

            // Act
            var result = math.Max(a, b);

            // Assert
            Assert.That(result, Is.EqualTo(a));
        }
    }
}
