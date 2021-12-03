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
    public class BookingHelper_OverlappingBookingsExistTests
    {
        // Scenario
        // New bookin finish in the middle of existing booking - overlap -> return reference to the existing booking
        // New booking finishing after the exisiting booking - overlapt -> return reference to the existing booking
        // New booking can start in the middle od exisiting booking and finish after - overlap -> return reference to the existing booking
        // New booking start and finishing after the existing booking - no overlap -> return empty string

        // Assuming we have overlap we have two more scenarios
        // The existing booking is cancelled, return empty string, we don't have overlap
        // The new booking is cancelled, return empty string becouse we don't have any overlap

        // Scenarios
        // New booking starts and finish after any other booking - no overraping -> return empty string
        [Test]
        public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString() 
        {
            // Arrange
            var repository = new Mock<IBookingRepository>();
            repository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>()
            {
                new Booking() 
                {
                    Id = 2, 
                    ArrivalDate = new DateTime(2017, 1, 15, 14, 0 ,0),
                    DepartureDate = new DateTime(2017, 1, 20, 10, 0 ,0),
                    Reference = "a"
                }
            }.AsQueryable());

            // Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = new DateTime(2017, 1, 10, 14, 0, 0),
                DepartureDate = new DateTime(2017, 1, 14, 10, 0, 0),
                Reference = "a"
            }, repository.Object);

            // Assert
            Assert.That(result, Is.EqualTo(""));
            Assert.That(result, Is.Empty);
        }

    }
}
