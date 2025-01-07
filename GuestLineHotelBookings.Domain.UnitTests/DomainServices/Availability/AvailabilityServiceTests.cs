using GuestLineHotelBookings.Domain.Bookings;
using GuestLineHotelBookings.Domain.DomainServices.Availability;
using GuestLineHotelBookings.Domain.Hotels;
using GuestLineHotelBookings.Domain.Hotels.Rooms;
using GuestLineHotelBookings.Domain.Hotels.RoomTypes;
using Moq;

namespace GuestLineHotelBookings.Domain.UnitTests.DomainServices.Availability
{
    internal class AvailabilityServiceTests
    {
        private Mock<IBookingRepository> _mockedBookingRepository;
        private Mock<IHotelRepository> _mockedHotelRepository;

        private AvailabilityService _availabilityService;

        [SetUp]
        public void Setup()
        {
            _mockedBookingRepository = new Mock<IBookingRepository>();
            _mockedHotelRepository = new Mock<IHotelRepository>();

            _availabilityService = new AvailabilityService(
                _mockedHotelRepository.Object,
                _mockedBookingRepository.Object);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Should_Throw_WhenGetAvailabilityAndHotelIdNullOrWhitespace_Throws(string hotelId)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _availabilityService.GetAvailability(hotelId, "SGL", DateTime.Now, DateTime.Now.AddDays(5)));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Should_Throw_WhenGetAvailabilityAndRoomTypeNullOrWhitespace_Throws(string roomType)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _availabilityService.GetAvailability("H1", roomType, DateTime.Now, DateTime.Now.AddDays(5)));
        }

        [Test]
        public void Should_Throw_WhenGetAvailabilityAndHotelDoesntExist_Throws()
        {
            // Arrange
            _mockedHotelRepository.Setup(x => x.GetHotelById(It.IsAny<string>()))
                .Returns((Hotel)null);

            // Act & Assert
            Assert.Throws<Exception>(() => _availabilityService.GetAvailability("H1", "SGL", DateTime.Now, DateTime.Now.AddDays(5)));
        }

        [Test]
        public void Should_ReturnValidAvailability_WhenRequestingOnlyChekIn_Returns()
        {
            // Arrange
            var rooms = new List<Room>
            {
                Room.CreateRoom("R1", "STD"),
                Room.CreateRoom("R2", "STD"),
                Room.CreateRoom("R3", "STD"),
                Room.CreateRoom("R4", "APR"),
                Room.CreateRoom("R5", "APR")
            };

            var roomTypes = new List<RoomType>
            {
                RoomType.CreateRoomType("STD", "Standard"),
                RoomType.CreateRoomType("APR", "Apartment")
            };

            var hotel = Hotel.CreateHotel("H1", "Hotel 1", roomTypes, rooms);

            _mockedHotelRepository.Setup(x => x.GetHotelById(It.IsAny<string>()))
                .Returns(hotel);

            _mockedBookingRepository.Setup(x => x.GetBookedRooms(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(new List<Booking>
                {
                    Booking.CreateBooking(hotel.Id, DateTime.Now.AddDays(10), DateTime.Now.AddDays(15), "STD"),
                    Booking.CreateBooking(hotel.Id, DateTime.Now.AddDays(12), DateTime.Now.AddDays(13), "STD")
                });

            // Act
            var result = _availabilityService.GetAvailability(hotel.Id, "STD", DateTime.Now.AddDays(11));

            // Assert
            Assert.That(result is not null);
            Assert.That(result.AvailableRooms == 1);
        }

        [Test]
        public void Should_ReturnValidAvailability_WhenRequested_Returns()
        {
            // Arrange
            var rooms = new List<Room>
            {
                Room.CreateRoom("R1", "STD"),
                Room.CreateRoom("R2", "STD"),
                Room.CreateRoom("R3", "STD"),
                Room.CreateRoom("R4", "APR"),
                Room.CreateRoom("R5", "APR")
            };

            var roomTypes = new List<RoomType>
            {
                RoomType.CreateRoomType("STD", "Standard"),
                RoomType.CreateRoomType("APR", "Apartment")
            };

            var hotel = Hotel.CreateHotel("H1", "Hotel 1", roomTypes, rooms);

            _mockedHotelRepository.Setup(x => x.GetHotelById(It.IsAny<string>()))
                .Returns(hotel);

            _mockedBookingRepository.Setup(x => x.GetBookedRooms(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(new List<Booking>
                {
                    Booking.CreateBooking(hotel.Id, DateTime.Now.AddDays(10), DateTime.Now.AddDays(15), "STD"),
                    Booking.CreateBooking(hotel.Id, DateTime.Now.AddDays(12), DateTime.Now.AddDays(13), "STD")
                });

            // Act
            var result = _availabilityService.GetAvailability(hotel.Id, "STD", DateTime.Now.AddDays(11), DateTime.Now.AddDays(12));

            // Assert
            Assert.That(result is not null);
            Assert.That(result.AvailableRooms == 1);
        }

        [Test]
        public void Should_ReturnNoAvailability_WhenThereAreNoRooms_Returns()
        {
            // Arrange
            var rooms = new List<Room>
            {
                Room.CreateRoom("R1", "STD"),
                Room.CreateRoom("R2", "STD"),
                Room.CreateRoom("R3", "STD"),
                Room.CreateRoom("R4", "APR"),
                Room.CreateRoom("R5", "APR")
            };

            var roomTypes = new List<RoomType>
            {
                RoomType.CreateRoomType("STD", "Standard"),
                RoomType.CreateRoomType("APR", "Apartment")
            };

            var hotel = Hotel.CreateHotel("H1", "Hotel 1", roomTypes, rooms);

            _mockedHotelRepository.Setup(x => x.GetHotelById(It.IsAny<string>()))
                .Returns(hotel);

            _mockedBookingRepository.Setup(x => x.GetBookedRooms(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(new List<Booking>
                {
                    Booking.CreateBooking(hotel.Id, DateTime.Now.AddDays(10), DateTime.Now.AddDays(15), "STD"),
                    Booking.CreateBooking(hotel.Id, DateTime.Now.AddDays(12), DateTime.Now.AddDays(13), "STD"),
                    Booking.CreateBooking(hotel.Id, DateTime.Now.AddDays(10), DateTime.Now.AddDays(13), "STD")
                });

            // Act
            var result = _availabilityService.GetAvailability(hotel.Id, "STD", DateTime.Now.AddDays(11), DateTime.Now.AddDays(12));

            // Assert
            Assert.That(result is not null);
            Assert.That(result.AvailableRooms == 0);
        }

        [Test]
        public void Should_ReturnNoOverbooked_WhenMoreRoomsBookedThanAvailable_Returns()
        {
            // Arrange
            var rooms = new List<Room>
            {
                Room.CreateRoom("R1", "STD"),
                Room.CreateRoom("R2", "STD"),
                Room.CreateRoom("R3", "STD"),
                Room.CreateRoom("R4", "APR"),
                Room.CreateRoom("R5", "APR")
            };

            var roomTypes = new List<RoomType>
            {
                RoomType.CreateRoomType("STD", "Standard"),
                RoomType.CreateRoomType("APR", "Apartment")
            };

            var hotel = Hotel.CreateHotel("H1", "Hotel 1", roomTypes, rooms);

            _mockedHotelRepository.Setup(x => x.GetHotelById(It.IsAny<string>()))
                .Returns(hotel);

            _mockedBookingRepository.Setup(x => x.GetBookedRooms(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns(new List<Booking>
                {
                    Booking.CreateBooking(hotel.Id, DateTime.Now.AddDays(10), DateTime.Now.AddDays(15), "STD"),
                    Booking.CreateBooking(hotel.Id, DateTime.Now.AddDays(12), DateTime.Now.AddDays(13), "STD"),
                    Booking.CreateBooking(hotel.Id, DateTime.Now.AddDays(10), DateTime.Now.AddDays(13), "STD"),
                    Booking.CreateBooking(hotel.Id, DateTime.Now.AddDays(10), DateTime.Now.AddDays(13), "STD")
                });

            // Act
            var result = _availabilityService.GetAvailability(hotel.Id, "STD", DateTime.Now.AddDays(11), DateTime.Now.AddDays(12));

            // Assert
            Assert.That(result is not null);
            Assert.That(result.AvailableRooms == -1);
            Assert.That(result.IsOverbooked);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Should_Throw_WhenSearchAvailabilityAndHotelIdNullOrWhitespace_Throws(string hotelId)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _availabilityService.SearchAvailability(hotelId, 365, "SGL"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void Should_Throw_WhenSearchAvailabilityAndRoomTypeNullOrWhitespace_Throws(string roomType)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _availabilityService.SearchAvailability("H1", 365, roomType));
        }

        [TestCase(0)]
        [TestCase(-14)]
        public void Should_Throw_WhenSearchAvailabilityAndRoomTypeNullOrWhitespace_Throws(int daysAhead)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => _availabilityService.SearchAvailability("H1", daysAhead, "SGL"));
        }

        [Test]
        public void Should_Throw_WhenSearchAvailabilityAndHotelDoesntExist_Throws()
        {
            // Arrange
            _mockedHotelRepository.Setup(x => x.GetHotelById(It.IsAny<string>()))
                .Returns((Hotel)null);

            // Act & Assert
            Assert.Throws<Exception>(() => _availabilityService.SearchAvailability("H1", 365, "SGL"));
        }

        [Test]
        public void Should_ReturnValidAvailabilites_WhenRequested_Returns()
        {
            // Arrange
            var rooms = new List<Room>
            {
                Room.CreateRoom("R1", "STD"),
                Room.CreateRoom("R2", "STD"),
                Room.CreateRoom("R3", "STD"),
                Room.CreateRoom("R4", "APR"),
                Room.CreateRoom("R5", "APR")
            };

            var roomTypes = new List<RoomType>
            {
                RoomType.CreateRoomType("STD", "Standard"),
                RoomType.CreateRoomType("APR", "Apartment")
            };

            var hotel = Hotel.CreateHotel("H1", "Hotel 1", roomTypes, rooms);

            _mockedHotelRepository.Setup(x => x.GetHotelById(It.IsAny<string>()))
                .Returns(hotel);

            var bookings = new List<Booking>
            {
                Booking.CreateBooking(hotel.Id, DateTime.Now.AddDays(10), DateTime.Now.AddDays(15), "STD"),
                Booking.CreateBooking(hotel.Id, DateTime.Now.AddDays(95), DateTime.Now.AddDays(105), "STD"),
                Booking.CreateBooking(hotel.Id, DateTime.Now.AddDays(205), DateTime.Now.AddDays(211), "STD"),
                Booking.CreateBooking(hotel.Id, DateTime.Now.AddDays(205), DateTime.Now.AddDays(211), "STD"),
                Booking.CreateBooking(hotel.Id, DateTime.Now.AddDays(205), DateTime.Now.AddDays(211), "STD")
            };

            _mockedBookingRepository.Setup(x => x.GetBookedRooms(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns((string hotelId, string roomType, DateTime checkIn, DateTime checkOut) =>
                {
                    return bookings.Where(b =>
                        b.CheckIn <= checkOut &&
                        b.CheckOut > checkIn)
                    .ToList();
                });

            // Act
            var result = _availabilityService.SearchAvailability(hotel.Id, 365, "STD");

            // Assert
            Assert.That(result is not null);
            Assert.That(result.Count == 6);
        }
    }
}
