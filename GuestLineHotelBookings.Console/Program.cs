
using GuestLineHotelBookings.Console;
using GuestLineHotelBookings.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddTransient<Application>();

        serviceCollection.AddServices();
        serviceCollection.AddRepositories();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var app = serviceProvider.GetRequiredService<Application>();

        app.Run();
    }
}