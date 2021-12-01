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
    public  class CustomerControllerTests
    {
        [Test]
        public void GetCustomer_IdIsZero_ReturnNotFound()
        {
            var controller = new CustomerController();
            var id = 0;

            var result = controller.GetCustomer(id);
            
            // Test if result is exactly the type of object (NotFound)
            Assert.That(result, Is.TypeOf<NotFound>());

            // Test if result is NotFound object and one of it's derivatives (NotFound/Ok/ActionResult)
            Assert.That(result, Is.InstanceOf<NotFound>());
        }

        [Test]
        public void GetCustomer_IdIsNotZero_ReturnOk()
        {
            var controller = new CustomerController();
            var id = 1;

            var result = controller.GetCustomer(id);

            Assert.That(result, Is.TypeOf<Ok>());
        }
    }
}
