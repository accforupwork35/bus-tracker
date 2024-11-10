using BusTrackingService.APIs.Dtos;
using BusTrackingService.Infrastructure.Models;

namespace BusTrackingService.APIs.Extensions;

public static class TripsExtensions
{
    public static Trip ToDto(this TripDbModel model)
    {
        return new Trip
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static TripDbModel ToModel(this TripUpdateInput updateDto, TripWhereUniqueInput uniqueId)
    {
        var trip = new TripDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            trip.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            trip.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return trip;
    }
}
