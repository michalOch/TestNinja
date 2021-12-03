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

        private Booking _existingBooking;
        private Mock<IBookingRepository> _repository;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            _existingBooking = new Booking()
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 15),
                DepartureDate = DepartOn(2017, 1, 20),
                Reference = "a"
            };

            _repository = new Mock<IBookingRepository>();
            _repository.Setup(r => r.GetActiveBookings(1)).Returns(new List<Booking>()
            {
                _existingBooking
            }.AsQueryable());
        }

        [Test]
        public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString() 
        {
            // Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate,days: 2),
                DepartureDate = Before(_existingBooking.ArrivalDate)
            }, _repository.Object);

            // Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingStartsBeforeAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBookingsReference()
        {
            // Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.ArrivalDate)
            }, _repository.Object);

            // Assert
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsBeforeAndFinishesAfterAnExistingBooking_ReturnExistingBookingsReference()
        {
            // Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = Before(_existingBooking.ArrivalDate),
                DepartureDate = After(_existingBooking.DepartureDate)
            }, _repository.Object);

            // Assert
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBookingsReference()
        {
            // Act
            var result = BookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = After(_existingBooking.ArrivalDate,2),
                DepartureDate = Before(_existingBooking.DepartureDate,2)
            }, _repository.Object);

            // Assert
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0 ,0);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }
    }
}
