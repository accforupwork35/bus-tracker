using BusTrackingService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace BusTrackingService.Infrastructure;

public class BusTrackingServiceDbContext : DbContext
{
    public BusTrackingServiceDbContext(DbContextOptions<BusTrackingServiceDbContext> options)
        : base(options) { }

    public DbSet<BusDbModel> Buses { get; set; }

    public DbSet<DriverDbModel> Drivers { get; set; }

    public DbSet<TripDbModel> Trips { get; set; }

    public DbSet<RouteDbModel> Routes { get; set; }

    public DbSet<UserDbModel> Users { get; set; }
}
