using BusTrackingService.APIs.Dtos;
using BusTrackingService.Infrastructure.Models;

namespace BusTrackingService.APIs.Extensions;

public static class DriversExtensions
{
    public static Driver ToDto(this DriverDbModel model)
    {
        return new Driver
        {
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static DriverDbModel ToModel(
        this DriverUpdateInput updateDto,
        DriverWhereUniqueInput uniqueId
    )
    {
        var driver = new DriverDbModel { Id = uniqueId.Id };

        if (updateDto.CreatedAt != null)
        {
            driver.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            driver.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return driver;
    }
}
