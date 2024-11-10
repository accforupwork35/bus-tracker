using BusTrackingService.Infrastructure;

namespace BusTrackingService.APIs;

public class TripsService : TripsServiceBase
{
    public TripsService(BusTrackingServiceDbContext context)
        : base(context) { }
}
