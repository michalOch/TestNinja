using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class OrderServiceTests
    {
        [Test]
        public void PlaceOrder_WhenCalled_ShouldStoreTheOrder()
        {
            // Arrange
            var storage = new Mock<IStorage>();
            var service = new OrderService(storage.Object);

            // Act
            var order = new Order();
            service.PlaceOrder(order);

            // Assert
            storage.Verify(s => s.Store(order));
        }
    }
}
