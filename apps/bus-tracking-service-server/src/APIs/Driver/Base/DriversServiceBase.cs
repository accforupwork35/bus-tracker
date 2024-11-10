using BusTrackingService.APIs;
using BusTrackingService.APIs.Common;
using BusTrackingService.APIs.Dtos;
using BusTrackingService.APIs.Errors;
using BusTrackingService.APIs.Extensions;
using BusTrackingService.Infrastructure;
using BusTrackingService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace BusTrackingService.APIs;

public abstract class DriversServiceBase : IDriversService
{
    protected readonly BusTrackingServiceDbContext _context;

    public DriversServiceBase(BusTrackingServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Driver
    /// </summary>
    public async Task<Driver> CreateDriver(DriverCreateInput createDto)
    {
        var driver = new DriverDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            driver.Id = createDto.Id;
        }

        _context.Drivers.Add(driver);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<DriverDbModel>(driver.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Driver
    /// </summary>
    public async Task DeleteDriver(DriverWhereUniqueInput uniqueId)
    {
        var driver = await _context.Drivers.FindAsync(uniqueId.Id);
        if (driver == null)
        {
            throw new NotFoundException();
        }

        _context.Drivers.Remove(driver);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Drivers
    /// </summary>
    public async Task<List<Driver>> Drivers(DriverFindManyArgs findManyArgs)
    {
        var drivers = await _context
            .Drivers.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return drivers.ConvertAll(driver => driver.ToDto());
    }

    /// <summary>
    /// Meta data about Driver records
    /// </summary>
    public async Task<MetadataDto> DriversMeta(DriverFindManyArgs findManyArgs)
    {
        var count = await _context.Drivers.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Driver
    /// </summary>
    public async Task<Driver> Driver(DriverWhereUniqueInput uniqueId)
    {
        var drivers = await this.Drivers(
            new DriverFindManyArgs { Where = new DriverWhereInput { Id = uniqueId.Id } }
        );
        var driver = drivers.FirstOrDefault();
        if (driver == null)
        {
            throw new NotFoundException();
        }

        return driver;
    }

    /// <summary>
    /// Update one Driver
    /// </summary>
    public async Task UpdateDriver(DriverWhereUniqueInput uniqueId, DriverUpdateInput updateDto)
    {
        var driver = updateDto.ToModel(uniqueId);

        _context.Entry(driver).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Drivers.Any(e => e.Id == driver.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }
}
