using BusTrackingService.APIs.Dtos;
using BusTrackingService.Infrastructure.Models;

namespace BusTrackingService.APIs.Extensions;

public static class BusesExtensions
{
    public static Bus ToDto(this BusDbModel model)
    {
        return new Bus
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static BusDbModel ToModel(this BusUpdateInput updateDto, BusWhereUniqueInput uniqueId)
    {
        var bus = new BusDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            bus.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            bus.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return bus;
    }
}
