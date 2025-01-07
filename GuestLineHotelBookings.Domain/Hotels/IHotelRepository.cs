namespace GuestLineHotelBookings.Domain.Hotels
{
    public interface IHotelRepository
    {
        Hotel GetHotelById(string hotelId);
    }
}
