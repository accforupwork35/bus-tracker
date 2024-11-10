using BusTrackingService.APIs;
using BusTrackingService.APIs.Common;
using BusTrackingService.APIs.Dtos;
using BusTrackingService.APIs.Errors;
using BusTrackingService.APIs.Extensions;
using BusTrackingService.Infrastructure;
using BusTrackingService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace BusTrackingService.APIs;

public abstract class TripsServiceBase : ITripsService
{
    protected readonly BusTrackingServiceDbContext _context;

    public TripsServiceBase(BusTrackingServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Trip
    /// </summary>
    public async Task<Trip> CreateTrip(TripCreateInput createDto)
    {
        var trip = new TripDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            trip.Id = createDto.Id;
        }

        _context.Trips.Add(trip);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<TripDbModel>(trip.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Trip
    /// </summary>
    public async Task DeleteTrip(TripWhereUniqueInput uniqueId)
    {
        var trip = await _context.Trips.FindAsync(uniqueId.Id);
        if (trip == null)
        {
            throw new NotFoundException();
        }

        _context.Trips.Remove(trip);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Trips
    /// </summary>
    public async Task<List<Trip>> Trips(TripFindManyArgs findManyArgs)
    {
        var trips = await _context
            .Trips.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return trips.ConvertAll(trip => trip.ToDto());
    }

    /// <summary>
    /// Meta data about Trip records
    /// </summary>
    public async Task<MetadataDto> TripsMeta(TripFindManyArgs findManyArgs)
    {
        var count = await _context.Trips.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Trip
    /// </summary>
    public async Task<Trip> Trip(TripWhereUniqueInput uniqueId)
    {
        var trips = await this.Trips(
            new TripFindManyArgs { Where = new TripWhereInput { Id = uniqueId.Id } }
        );
        var trip = trips.FirstOrDefault();
        if (trip == null)
        {
            throw new NotFoundException();
        }

        return trip;
    }

    /// <summary>
    /// Update one Trip
    /// </summary>
    public async Task UpdateTrip(TripWhereUniqueInput uniqueId, TripUpdateInput updateDto)
    {
        var trip = updateDto.ToModel(uniqueId);

        _context.Entry(trip).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Trips.Any(e => e.Id == trip.Id))
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
