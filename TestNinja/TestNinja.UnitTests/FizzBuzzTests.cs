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
    public class FizzBuzzTests
    {
        [Test]
        public void GetOutput_InputIsDivisibleByThreeAndFive_ReturnFizzBuzz()
        {
            // Assert
            var number = 15;

            // Act
            var result = FizzBuzz.GetOutput(number);

            // Assert 
            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }

        [Test]
        public void GetOutput_InputIsDivisibleByThreeOnly_ReturnFizz()
        {
            // Assert
            var number = 3;

            // Act
            var result = FizzBuzz.GetOutput(number);

            // Assert 
            Assert.That(result, Is.EqualTo("Fizz"));
        }

        [Test]
        public void GetOutput_InputIsDivisibleByFiveOnly_ReturnBuzz()
        {
            // Assert
            var number = 5;

            // Act
            var result = FizzBuzz.GetOutput(number);

            // Assert 
            Assert.That(result, Is.EqualTo("Buzz"));
        }

        [Test]
        public void GetOutput_InputIsNotDivisibleByThreeOrFive_ReturnTheSameNumber()
        {
            // Assert
            var number = 2;

            // Act
            var result = FizzBuzz.GetOutput(number);

            // Assert 
            Assert.That(result, Is.EqualTo(number.ToString()));
        }
    }
}
