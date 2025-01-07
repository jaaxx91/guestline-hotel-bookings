namespace GuestLineHotelBookings.Domain.Bookings
{
    public class Booking
    {
        public string HotelId => Id.HotelId;

        public DateTime CheckIn => Id.CheckIn;

        public DateTime CheckOut => Id.CheckOut;

        public string RoomType => Id.RoomType;

        public string RoomRate { get; private set; }

        private BookingId Id { get; }

        private Booking(
            string hotelId,
            DateTime checkIn,
            DateTime checkOut,
            string roomType,
            string roomRate = null)
        {
            if (string.IsNullOrWhiteSpace(hotelId))
            {
                throw new ArgumentException(nameof(hotelId));
            }

            if (string.IsNullOrWhiteSpace(roomType))
            {
                throw new ArgumentException(nameof(roomType));
            }

            Id = new BookingId(hotelId, checkIn, checkOut, roomType);

            RoomRate = roomRate;
        }

        public static Booking CreateBooking(
            string hotelId,
            DateTime checkIn,
            DateTime checkOut,
            string roomType,
            string roomRate = null)
            => new Booking(hotelId, checkIn, checkOut, roomType, roomRate);
    }
}
