using BusTrackingService.Infrastructure;

namespace BusTrackingService.APIs;

public class DriversService : DriversServiceBase
{
    public DriversService(BusTrackingServiceDbContext context)
        : base(context) { }
}
