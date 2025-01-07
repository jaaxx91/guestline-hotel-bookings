namespace GuestLineHotelBookings.Domain.Hotels.RoomTypes
{
    public class RoomType
    {
        public string Code { get; private set; }

        public string Description { get; private set; }

        private readonly List<string> _amenities;

        private readonly List<string> _features;

        public IReadOnlyCollection<string> Amenities => _amenities.AsReadOnly();

        public IReadOnlyCollection<string> Features => _features.AsReadOnly();

        private RoomType(
            string code,
            string description,
            string[] amenities = null,
            string[] features = null)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentException(nameof(code));
            }

            Code = code;
            Description = description;
            _amenities = amenities?.ToList() ?? new List<string>();
            _features = features?.ToList() ?? new List<string>();
        }

        public static RoomType CreateRoomType(
            string code,
            string description,
            string[] amenities = null,
            string[] features = null)
            => new RoomType(code, description, amenities, features);
    }
}
