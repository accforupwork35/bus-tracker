using BusTrackingService.Infrastructure;

namespace BusTrackingService.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(BusTrackingServiceDbContext context)
        : base(context) { }
}
