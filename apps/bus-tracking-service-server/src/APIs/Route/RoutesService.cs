using BusTrackingService.Infrastructure;

namespace BusTrackingService.APIs;

public class RoutesService : RoutesServiceBase
{
    public RoutesService(BusTrackingServiceDbContext context)
        : base(context) { }
}
