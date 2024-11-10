using BusTrackingService.APIs.Common;
using BusTrackingService.APIs.Dtos;

namespace BusTrackingService.APIs;

public interface IRoutesService
{
    /// <summary>
    /// Create one Route
    /// </summary>
    public Task<Route> CreateRoute(RouteCreateInput route);

    /// <summary>
    /// Delete one Route
    /// </summary>
    public Task DeleteRoute(RouteWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Routes
    /// </summary>
    public Task<List<Route>> Routes(RouteFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Route records
    /// </summary>
    public Task<MetadataDto> RoutesMeta(RouteFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Route
    /// </summary>
    public Task<Route> Route(RouteWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Route
    /// </summary>
    public Task UpdateRoute(RouteWhereUniqueInput uniqueId, RouteUpdateInput updateDto);
}
