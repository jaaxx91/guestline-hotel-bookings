using GuestLineHotelBookings.Domain.Bookings;
using GuestLineHotelBookings.Infrastructure.Data.Mappers;
using GuestLineHotelBookings.Infrastructure.Data.Models;
using System.Text.Json;

namespace GuestLineHotelBookings.Infrastructure.Repositories.Bookings
{
    public class BookingRepository : IBookingRepository
    {
        private readonly List<Booking> _bookings;

        public BookingRepository(List<Booking> bookings = null)
        {
            if (bookings is null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Data\\bookings.json");

                string jsonInput = File.ReadAllText(path);

                var dtos = JsonSerializer.Deserialize<List<BookingDto>>(jsonInput, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                _bookings = BookingMapper.ToDomainList(dtos);
            }
            else
            {
                _bookings = bookings;
            }
        }

        public List<Booking> GetBookedRooms(
            string hotelId,
            string roomType,
            DateTime checkIn,
            DateTime checkOut)
            => _bookings.Where(b =>
                b.HotelId == hotelId &&
                b.RoomType == roomType &&
                b.CheckIn <= checkOut &&
                b.CheckOut > checkIn).ToList();
    }
}
