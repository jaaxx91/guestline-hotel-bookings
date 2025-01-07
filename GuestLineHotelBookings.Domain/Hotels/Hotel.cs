using GuestLineHotelBookings.Domain.Hotels.Rooms;
using GuestLineHotelBookings.Domain.Hotels.RoomTypes;

namespace GuestLineHotelBookings.Domain.Hotels
{
    public class Hotel
    {
        public string Id { get; private set; }

        public string Name { get; private set; }

        private readonly List<Room> _rooms;

        private readonly List<RoomType> _roomTypes;

        public IReadOnlyCollection<Room> Rooms => _rooms.AsReadOnly();

        public IReadOnlyCollection<RoomType> RoomTypes => _roomTypes.AsReadOnly();

        private Hotel(
            string id,
            string name,
            List<RoomType> roomTypes,
            List<Room> rooms)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(nameof(id));
            }

            if (!roomTypes?.Any() ?? true)
            {
                throw new ArgumentException(nameof(roomTypes));
            }

            if (!rooms?.Any() ?? true)
            {
                throw new ArgumentException(nameof(rooms));
            }

            Id = id;
            Name = name;
            
            _roomTypes = roomTypes;
            _rooms = rooms;
        }

        public static Hotel CreateHotel(
            string id,
            string name,
            List<RoomType> roomTypes,
            List<Room> rooms)
            => new Hotel(id, name, roomTypes, rooms);
    }
}
