namespace GuestLineHotelBookings.Infrastructure.Data.Models
{
    internal class RoomTypeDto
    {
        public string Code { get; set; }

        public string Description { get; set; }

        public string[] Amenities { get; set; }

        public string[] Features { get; set; }

    }
}
