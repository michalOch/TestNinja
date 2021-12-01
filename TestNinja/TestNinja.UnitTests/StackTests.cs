using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class StackTests
    {
        private Stack<string> _stack;   

        [SetUp]
        public void SetUp()
        {
            _stack = new Stack<string>();
        }

        [Test]
        public void Push_ObjectIsNull_ThrowsArgumentNullException()
        {
            _stack.Push(null);
            var result = _stack.Peek();

            Assert.That(() => _stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Push_WhenCalled_AddObjectToStack()
        {
            // Arrange
            var obj = "a";

            // Act
            _stack.Push(obj);

            // Assert
            Assert.That(_stack.Count, Is.GreaterThan(0));
            Assert.That(_stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Pop_ThereAreNoObjectsInTheStack_ThrowsInvalidOperationException()
        {
            Assert.That(() => _stack.Pop(), Throws.Exception.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Pop_WhenCalled_ReturnAndRemoveLastObjectFromStack()
        {
            // Arange
            var obj = "a";
            _stack.Push(obj);

            // Act
            var result = _stack.Pop();

            // Assert
            Assert.That(result, Is.EqualTo(obj));
            Assert.That(_stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Peek_ThereAreNoObjectsInTheStack_ThrowsInvalidOperationException()
        {
            Assert.That(() => _stack.Peek(), Throws.Exception.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Peek_WhenCalled_ReturnLastObjectFromTheStack()
        {
            // Arange
            var obj = "a";
            _stack.Push(obj);

            // Act
            var result = _stack.Peek();

            // Assert
            Assert.That(result, Is.EqualTo(obj));
            Assert.That(_stack.Count, Is.EqualTo(1));
        }
    }
}
