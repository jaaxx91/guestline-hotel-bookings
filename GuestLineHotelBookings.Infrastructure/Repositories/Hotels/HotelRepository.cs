using GuestLineHotelBookings.Domain.Hotels;
using GuestLineHotelBookings.Infrastructure.Data.Mappers;
using GuestLineHotelBookings.Infrastructure.Data.Models;
using System.Text.Json;

namespace GuestLineHotelBookings.Infrastructure.Repositories.Hotels
{
    public class HotelRepository : IHotelRepository
    {
        private readonly List<Hotel> _hotels;

        public HotelRepository()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Data\\hotels.json");

            string jsonInput = File.ReadAllText(path);

            var dtos = JsonSerializer.Deserialize<List<HotelDto>>(jsonInput, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            _hotels = HotelMapper.ToDomainList(dtos);
        }

        public Hotel GetHotelById(string hotelId)
            => _hotels.FirstOrDefault(h => h.Id == hotelId);
    }
}
