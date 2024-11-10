using Microsoft.AspNetCore.Mvc;

namespace BusTrackingService.APIs;

[ApiController()]
public class BusesController : BusesControllerBase
{
    public BusesController(IBusesService service)
        : base(service) { }
}
