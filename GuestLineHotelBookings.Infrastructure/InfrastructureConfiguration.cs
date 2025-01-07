using GuestLineHotelBookings.Domain.Bookings;
using GuestLineHotelBookings.Domain.Hotels;
using GuestLineHotelBookings.Infrastructure.Repositories.Bookings;
using GuestLineHotelBookings.Infrastructure.Repositories.Hotels;
using Microsoft.Extensions.DependencyInjection;

namespace GuestLineHotelBookings.Infrastructure
{
    public static class InfrastructureConfiguration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();

            return services;
        } 
    }
}
