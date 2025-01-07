using GuestLineHotelBookings.Domain.Hotels;
using GuestLineHotelBookings.Domain.Hotels.Rooms;
using GuestLineHotelBookings.Domain.Hotels.RoomTypes;
using GuestLineHotelBookings.Infrastructure.Data.Models;

namespace GuestLineHotelBookings.Infrastructure.Data.Mappers
{
    internal static class HotelMapper
    {
        public static Hotel ToDomain(HotelDto dto)
        {
            var roomTypes = dto.RoomTypes
                .Select(rt => RoomType.CreateRoomType(rt.Code, rt.Description, rt.Amenities, rt.Features))
                .ToList();

            var rooms = dto.Rooms
                .Select(r => Room.CreateRoom(r.RoomId, r.RoomType))
                .ToList();

            return Hotel.CreateHotel(dto.Id, dto.Name, roomTypes, rooms);
        }

        public static List<Hotel> ToDomainList(IEnumerable<HotelDto> dtos)
            => dtos.Select(ToDomain).ToList();
    }
}
