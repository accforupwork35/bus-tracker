using Microsoft.AspNetCore.Mvc;

namespace BusTrackingService.APIs;

[ApiController()]
public class TripsController : TripsControllerBase
{
    public TripsController(ITripsService service)
        : base(service) { }
}
