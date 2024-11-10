using BusTrackingService.Infrastructure;

namespace BusTrackingService.APIs;

public class BusesService : BusesServiceBase
{
    public BusesService(BusTrackingServiceDbContext context)
        : base(context) { }
}
