using BusTrackingService.APIs.Common;
using BusTrackingService.APIs.Dtos;

namespace BusTrackingService.APIs;

public interface ITripsService
{
    /// <summary>
    /// Create one Trip
    /// </summary>
    public Task<Trip> CreateTrip(TripCreateInput trip);

    /// <summary>
    /// Delete one Trip
    /// </summary>
    public Task DeleteTrip(TripWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Trips
    /// </summary>
    public Task<List<Trip>> Trips(TripFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Trip records
    /// </summary>
    public Task<MetadataDto> TripsMeta(TripFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Trip
    /// </summary>
    public Task<Trip> Trip(TripWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Trip
    /// </summary>
    public Task UpdateTrip(TripWhereUniqueInput uniqueId, TripUpdateInput updateDto);
}
