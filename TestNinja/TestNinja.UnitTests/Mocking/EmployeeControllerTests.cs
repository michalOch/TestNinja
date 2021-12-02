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
    public class EmployeeControllerTests
    {
        private EmployeeController _controller;
        private Mock<IEmployeeStorage> _employeeStorage;

        [SetUp]
        public void SetUp()
        {
            _employeeStorage = new Mock<IEmployeeStorage>();    
            _controller = new EmployeeController(_employeeStorage.Object);
        }

        [Test]
        public void DeleteEmployee_EmployeeInDatabase_RedirectToActionEmployees()
        {
            // Act
            var result = _controller.DeleteEmployee(1);

            // Assert
            Assert.That(result, Is.TypeOf<RedirectResult>());
        }

        [Test]
        public void DeleteEmployee_WhenCalled_DeleteEmployeeFromDataBase()
        {
            // Arrange
            var id = 1;

            // Act
            _controller.DeleteEmployee(id);

            // Assert
            _employeeStorage.Verify(s => s.DeleteEmployee(id));
        }
    }   
}
