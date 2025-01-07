namespace GuestLineHotelBookings.Domain.Bookings
{
    internal record BookingId(string HotelId, DateTime CheckIn, DateTime CheckOut, string RoomType);
}
