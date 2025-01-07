namespace GuestLineHotelBookings.Domain.Bookings
{
    public interface IBookingRepository
    {
        List<Booking> GetBookedRooms(
            string hotelId,
            string roomType,
            DateTime checkIn,
            DateTime checkOut);
    }
}
