namespace GuestLineHotelBookings.Domain.Hotels.Rooms
{
    public class Room
    {
        public string Id { get; private set; }

        public string Type { get; private set; }

        private Room(string id, string type)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException(nameof(id));
            }

            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentException(nameof(type));
            }

            Id = id;
            Type = type;
        }

        public static Room CreateRoom(string Id, string type)
            => new Room(Id, type);
    }
}
