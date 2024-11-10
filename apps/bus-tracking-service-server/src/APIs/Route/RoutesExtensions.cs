using BusTrackingService.APIs.Dtos;
using BusTrackingService.Infrastructure.Models;

namespace BusTrackingService.APIs.Extensions;

public static class RoutesExtensions
{
    public static Route ToDto(this RouteDbModel model)
    {
        return new Route
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static RouteDbModel ToModel(
        this RouteUpdateInput updateDto,
        RouteWhereUniqueInput uniqueId
    )
    {
        var route = new RouteDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            route.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            route.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return route;
    }
}
