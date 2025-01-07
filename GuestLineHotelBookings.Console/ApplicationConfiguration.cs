using GuestLineHotelBookings.Domain.DomainServices.Availability;
using Microsoft.Extensions.DependencyInjection;

namespace GuestLineHotelBookings.Console
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAvailabilityService, AvailabilityService>();

            return services;
        }
    }
}
