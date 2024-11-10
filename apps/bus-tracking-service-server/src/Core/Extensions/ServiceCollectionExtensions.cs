using BusTrackingService.APIs;

namespace BusTrackingService;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IBusesService, BusesService>();
        services.AddScoped<IDriversService, DriversService>();
        services.AddScoped<IRoutesService, RoutesService>();
        services.AddScoped<ITripsService, TripsService>();
        services.AddScoped<IUsersService, UsersService>();
    }
}
