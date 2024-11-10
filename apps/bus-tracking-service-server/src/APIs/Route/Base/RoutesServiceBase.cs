using BusTrackingService.APIs;
using BusTrackingService.APIs.Common;
using BusTrackingService.APIs.Dtos;
using BusTrackingService.APIs.Errors;
using BusTrackingService.APIs.Extensions;
using BusTrackingService.Infrastructure;
using BusTrackingService.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace BusTrackingService.APIs;

public abstract class RoutesServiceBase : IRoutesService
{
    protected readonly BusTrackingServiceDbContext _context;

    public RoutesServiceBase(BusTrackingServiceDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Route
    /// </summary>
    public async Task<Route> CreateRoute(RouteCreateInput createDto)
    {
        var route = new RouteDbModel
        {
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            route.Id = createDto.Id;
        }

        _context.Routes.Add(route);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<RouteDbModel>(route.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Route
    /// </summary>
    public async Task DeleteRoute(RouteWhereUniqueInput uniqueId)
    {
        var route = await _context.Routes.FindAsync(uniqueId.Id);
        if (route == null)
        {
            throw new NotFoundException();
        }

        _context.Routes.Remove(route);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Routes
    /// </summary>
    public async Task<List<Route>> Routes(RouteFindManyArgs findManyArgs)
    {
        var routes = await _context
            .Routes.ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return routes.ConvertAll(route => route.ToDto());
    }

    /// <summary>
    /// Meta data about Route records
    /// </summary>
    public async Task<MetadataDto> RoutesMeta(RouteFindManyArgs findManyArgs)
    {
        var count = await _context.Routes.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Route
    /// </summary>
    public async Task<Route> Route(RouteWhereUniqueInput uniqueId)
    {
        var routes = await this.Routes(
            new RouteFindManyArgs { Where = new RouteWhereInput { Id = uniqueId.Id } }
        );
        var route = routes.FirstOrDefault();
        if (route == null)
        {
            throw new NotFoundException();
        }

        return route;
    }

    /// <summary>
    /// Update one Route
    /// </summary>
    public async Task UpdateRoute(RouteWhereUniqueInput uniqueId, RouteUpdateInput updateDto)
    {
        var route = updateDto.ToModel(uniqueId);

        _context.Entry(route).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Routes.Any(e => e.Id == route.Id))
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
