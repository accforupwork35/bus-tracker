using BusTrackingService.APIs;
using BusTrackingService.APIs.Common;
using BusTrackingService.APIs.Dtos;
using BusTrackingService.APIs.Errors;
using BusTrackingService.APIs.Extensions;
using BusTrackingService.Infrastructure;
using BusTrackingService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace BusTrackingService.APIs;

public abstract class BusesServiceBase : IBusesService
{
    protected readonly BusTrackingServiceDbContext _context;

    public BusesServiceBase(BusTrackingServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Bus
    /// </summary>
    public async Task<Bus> CreateBus(BusCreateInput createDto)
    {
        var bus = new BusDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            bus.Id = createDto.Id;
        }

        _context.Buses.Add(bus);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<BusDbModel>(bus.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Bus
    /// </summary>
    public async Task DeleteBus(BusWhereUniqueInput uniqueId)
    {
        var bus = await _context.Buses.FindAsync(uniqueId.Id);
        if (bus == null)
        {
            throw new NotFoundException();
        }

        _context.Buses.Remove(bus);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Buses
    /// </summary>
    public async Task<List<Bus>> Buses(BusFindManyArgs findManyArgs)
    {
        var buses = await _context
            .Buses.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return buses.ConvertAll(bus => bus.ToDto());
    }

    /// <summary>
    /// Meta data about Bus records
    /// </summary>
    public async Task<MetadataDto> BusesMeta(BusFindManyArgs findManyArgs)
    {
        var count = await _context.Buses.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Bus
    /// </summary>
    public async Task<Bus> Bus(BusWhereUniqueInput uniqueId)
    {
        var buses = await this.Buses(
            new BusFindManyArgs { Where = new BusWhereInput { Id = uniqueId.Id } }
        );
        var bus = buses.FirstOrDefault();
        if (bus == null)
        {
            throw new NotFoundException();
        }

        return bus;
    }

    /// <summary>
    /// Update one Bus
    /// </summary>
    public async Task UpdateBus(BusWhereUniqueInput uniqueId, BusUpdateInput updateDto)
    {
        var bus = updateDto.ToModel(uniqueId);

        _context.Entry(bus).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Buses.Any(e => e.Id == bus.Id))
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
