using GuestLineHotelBookings.Domain.Bookings;
using GuestLineHotelBookings.Infrastructure.Data.Models;

namespace GuestLineHotelBookings.Infrastructure.Data.Mappers
{
    internal class BookingMapper
    {
        private const string DateTimeFormat = "yyyyMMdd";

        public static Booking ToDomain(BookingDto dto)
            => Booking.CreateBooking(
                dto.HotelId,
                DateTime.ParseExact(dto.Arrival, DateTimeFormat, null),
                DateTime.ParseExact(dto.Departure, DateTimeFormat, null),
                dto.RoomType,
                dto.RoomRate);

        public static List<Booking> ToDomainList(IEnumerable<BookingDto> dtos)
            => dtos.Select(ToDomain).ToList();
    }
}
