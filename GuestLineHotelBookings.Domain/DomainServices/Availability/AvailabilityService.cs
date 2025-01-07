using GuestLineHotelBookings.Domain.Bookings;
using GuestLineHotelBookings.Domain.DomainServices.Results;
using GuestLineHotelBookings.Domain.Hotels;

namespace GuestLineHotelBookings.Domain.DomainServices.Availability
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IBookingRepository _bookingRepository;

        public AvailabilityService(IHotelRepository hotelRepository, IBookingRepository bookingRepository)
        {
            _hotelRepository = hotelRepository;
            _bookingRepository = bookingRepository;
        }

        public RoomAvailabilityResult GetAvailability(
            string hotelId,
            string roomType,
            DateTime checkIn,
            DateTime? checkOut = null)
        {
            if (string.IsNullOrWhiteSpace(hotelId))
            {
                throw new ArgumentException(nameof(hotelId));
            }

            if (string.IsNullOrWhiteSpace(roomType))
            {
                throw new ArgumentException(nameof(roomType));
            }

            var hotel = _hotelRepository.GetHotelById(hotelId);

            if (hotel is null)
            {
                throw new Exception($"Failed to find hotel: {hotelId}");
            }

            var totalRooms = hotel.Rooms.Count(r => r.Type == roomType);

            if (checkOut is null)
            {
                checkOut = checkIn;
            }

            var bookedRooms = _bookingRepository.GetBookedRooms(
                hotelId,
                roomType,
                checkIn,
                checkOut.Value);

            var availableRooms = totalRooms - bookedRooms.Count();

            return RoomAvailabilityResult.CreateRoomAvailabilityResult(
                hotelId,
                roomType,
                availableRooms,
                checkIn,
                (DateTime)checkOut);
        }

        public List<RoomAvailabilityResult> SearchAvailability(
            string hotelId,
            int daysAhead,
            string roomType)
        {
            if (string.IsNullOrWhiteSpace(hotelId))
            {
                throw new ArgumentException(nameof(hotelId));
            }

            if (string.IsNullOrWhiteSpace(roomType))
            {
                throw new ArgumentException(nameof(roomType));
            }

            if (daysAhead <= 0)
            {
                throw new ArgumentException(nameof(daysAhead));
            }

            var hotel = _hotelRepository.GetHotelById(hotelId);

            if (hotel is null)
            {
                throw new Exception($"Failed to find hotel: {hotelId}");
            }

            var totalRooms = hotel.Rooms.Count(r => r.Type == roomType);

            var results = new List<RoomAvailabilityResult>();

            var startDate = DateTime.Today;
            var endDate = startDate.AddDays(daysAhead);

            DateTime? rangeStart = null;
            int currentAvailability = 0;

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var bookedRooms = _bookingRepository.GetBookedRooms(
                    hotelId,
                    roomType,
                    date,
                    date);

                var availability = totalRooms - bookedRooms.Count();

                if (availability > 0)
                {
                    if (!rangeStart.HasValue)
                    {
                        rangeStart = date;
                        currentAvailability = availability;
                    }
                    else if (availability != currentAvailability)
                    {
                        var roomAvailabilityResult = RoomAvailabilityResult.CreateRoomAvailabilityResult(
                            hotelId,
                            roomType,
                            currentAvailability,
                            rangeStart.Value == startDate ? startDate : rangeStart.Value.AddDays(-1),
                            date.AddDays(-1));

                        results.Add(roomAvailabilityResult);
                        rangeStart = date;
                        currentAvailability = availability;
                    }
                }
                else if (rangeStart is not null)
                {
                    var roomAvailabilityResult = RoomAvailabilityResult.CreateRoomAvailabilityResult(
                        hotelId,
                        roomType,
                        currentAvailability,
                        rangeStart.Value.AddDays(-1),
                        date.AddDays(-1));

                    results.Add(roomAvailabilityResult);
                    rangeStart = null;
                }
            }

            if (rangeStart is not null)
            {
                var roomAvailabilityResult = RoomAvailabilityResult.CreateRoomAvailabilityResult(
                    hotelId,
                    roomType,
                    currentAvailability,
                    rangeStart.Value,
                    endDate);

                results.Add(roomAvailabilityResult);
            }

            return results;
        }
    }
}
