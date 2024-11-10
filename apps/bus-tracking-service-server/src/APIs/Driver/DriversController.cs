using Microsoft.AspNetCore.Mvc;

namespace BusTrackingService.APIs;

[ApiController()]
public class DriversController : DriversControllerBase
{
    public DriversController(IDriversService service)
        : base(service) { }
}
