using GuestLineHotelBookings.Domain.Bookings;
using GuestLineHotelBookings.Infrastructure.Repositories.Bookings;

namespace GuestLineHotelBookings.Infrastructure.UnitTests.Repositories.Bookings
{
    internal class BookingRepositoryTests
    {
        private BookingRepository _bookingRepository;

        [SetUp]
        public void Setup()
        {
            var bookings = new List<Booking>
            {
                    Booking.CreateBooking("H1", DateTime.Now.AddDays(10), DateTime.Now.AddDays(15), "STD"),
                    Booking.CreateBooking("H1", DateTime.Now.AddDays(12), DateTime.Now.AddDays(13), "STD"),
                    Booking.CreateBooking("H1", DateTime.Now.AddDays(1), DateTime.Now.AddDays(5), "STD"),
                    Booking.CreateBooking("H1", DateTime.Now.AddDays(11), DateTime.Now.AddDays(20), "APR"),
                    Booking.CreateBooking("H1", DateTime.Now.AddDays(2), DateTime.Now.AddDays(3), "APR")
            };

            _bookingRepository = new BookingRepository(bookings);
        }

        [TestCase("STD", 11, 13, 2)]
        [TestCase("STD", 0, 6, 1)]
        [TestCase("APR", 0, 11, 2)]
        [TestCase("APR", 20, 25, 0)]
        public void Should_ReturnValidBookings_WhenRequested_Returns(
            string roomType,
            int startDay,
            int endDay,
            int expectedBookedRooms)
        {
            // Act
            var bookedRooms = _bookingRepository.GetBookedRooms(
                "H1",
                roomType,
                DateTime.Now.AddDays(startDay),
                DateTime.Now.AddDays(endDay));

            // Assert
            Assert.That(bookedRooms is not null);
            Assert.That(bookedRooms.Count == expectedBookedRooms);
        }
    }
}
