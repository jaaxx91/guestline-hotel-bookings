namespace GuestLineHotelBookings.Domain.DomainServices.Results
{
    public class RoomAvailabilityResult
    {
        public string HotelId { get; private set; }

        public string RoomType { get; private set; }

        public int AvailableRooms { get; private set; }

        public DateTime CheckIn { get; private set; }

        public DateTime CheckOut { get; private set; }

        public bool IsOverbooked => AvailableRooms < 0;

        public RoomAvailabilityResult(
            string hotelId,
            string roomType,
            int availableRooms,
            DateTime checkIn,
            DateTime checkOut)
        {
            HotelId = string.IsNullOrWhiteSpace(hotelId) ? throw new ArgumentException(nameof(hotelId)) : hotelId;
            RoomType = string.IsNullOrWhiteSpace(roomType) ? throw new ArgumentException(nameof(roomType)) : roomType;
            AvailableRooms = availableRooms;
            CheckIn = checkIn;
            CheckOut = checkOut;
        }

        public static RoomAvailabilityResult CreateRoomAvailabilityResult(
            string hotelId,
            string roomType,
            int availableRooms,
            DateTime checkIn,
            DateTime checkOut)
            => new RoomAvailabilityResult(
                hotelId,
                roomType,
                availableRooms,
                checkIn,
                checkOut);

        public override string ToString()
        {
            var overbooked = IsOverbooked ? " - Overbooked" : null;
            var dates = CheckIn == CheckOut ? $"on {CheckIn.ToString("d")}" : $"{CheckIn.ToString("d")} - {CheckOut.ToString("d")}";

            return $"{HotelId} Available {RoomType} {dates}: {AvailableRooms}{overbooked}";
        }
    }
}
