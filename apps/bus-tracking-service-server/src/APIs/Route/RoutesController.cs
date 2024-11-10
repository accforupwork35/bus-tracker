using Microsoft.AspNetCore.Mvc;

namespace BusTrackingService.APIs;

[ApiController()]
public class RoutesController : RoutesControllerBase
{
    public RoutesController(IRoutesService service)
        : base(service) { }
}
