namespace GuestLineHotelBookings.Infrastructure.Data.Models
{
    internal class BookingDto
    {
        public string HotelId { get; set; }

        public string Arrival { get; set; }

        public string Departure { get; set; }

        public string RoomType { get; set; }

        public string RoomRate { get; set; }
    }
}
