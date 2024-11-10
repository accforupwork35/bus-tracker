using BusTrackingService.APIs.Common;
using BusTrackingService.APIs.Dtos;

namespace BusTrackingService.APIs;

public interface IBusesService
{
    /// <summary>
    /// Create one Bus
    /// </summary>
    public Task<Bus> CreateBus(BusCreateInput bus);

    /// <summary>
    /// Delete one Bus
    /// </summary>
    public Task DeleteBus(BusWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Buses
    /// </summary>
    public Task<List<Bus>> Buses(BusFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Bus records
    /// </summary>
    public Task<MetadataDto> BusesMeta(BusFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Bus
    /// </summary>
    public Task<Bus> Bus(BusWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Bus
    /// </summary>
    public Task UpdateBus(BusWhereUniqueInput uniqueId, BusUpdateInput updateDto);
}
