using GuestLineHotelBookings.Domain.DomainServices.Results;

namespace GuestLineHotelBookings.Domain.DomainServices.Availability
{
    public interface IAvailabilityService
    {
        RoomAvailabilityResult GetAvailability(
            string hotelId,
            string roomType,
            DateTime checkIn,
            DateTime? checkOut = null);

        List<RoomAvailabilityResult> SearchAvailability(
            string hotelId,
            int daysAhead,
            string roomType);
    }
}
