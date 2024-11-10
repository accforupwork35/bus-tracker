using BusTrackingService.APIs.Common;
using BusTrackingService.APIs.Dtos;

namespace BusTrackingService.APIs;

public interface IDriversService
{
    /// <summary>
    /// Create one Driver
    /// </summary>
    public Task<Driver> CreateDriver(DriverCreateInput driver);

    /// <summary>
    /// Delete one Driver
    /// </summary>
    public Task DeleteDriver(DriverWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Drivers
    /// </summary>
    public Task<List<Driver>> Drivers(DriverFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Driver records
    /// </summary>
    public Task<MetadataDto> DriversMeta(DriverFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Driver
    /// </summary>
    public Task<Driver> Driver(DriverWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Driver
    /// </summary>
    public Task UpdateDriver(DriverWhereUniqueInput uniqueId, DriverUpdateInput updateDto);
}
