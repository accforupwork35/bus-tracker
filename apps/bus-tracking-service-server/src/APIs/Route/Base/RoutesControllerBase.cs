using BusTrackingService.APIs;
using BusTrackingService.APIs.Common;
using BusTrackingService.APIs.Dtos;
using BusTrackingService.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace BusTrackingService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class RoutesControllerBase : ControllerBase
{
    protected readonly IRoutesService _service;

    public RoutesControllerBase(IRoutesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Route
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Route>> CreateRoute(RouteCreateInput input)
    {
        var route = await _service.CreateRoute(input);

        return CreatedAtAction(nameof(Route), new { id = route.Id }, route);
    }

    /// <summary>
    /// Delete one Route
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteRoute([FromRoute()] RouteWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteRoute(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Routes
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Route>>> Routes([FromQuery()] RouteFindManyArgs filter)
    {
        return Ok(await _service.Routes(filter));
    }

    /// <summary>
    /// Meta data about Route records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> RoutesMeta([FromQuery()] RouteFindManyArgs filter)
    {
        return Ok(await _service.RoutesMeta(filter));
    }

    /// <summary>
    /// Get one Route
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Route>> Route([FromRoute()] RouteWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Route(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Route
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateRoute(
        [FromRoute()] RouteWhereUniqueInput uniqueId,
        [FromQuery()] RouteUpdateInput routeUpdateDto
    )
    {
        try
        {
            await _service.UpdateRoute(uniqueId, routeUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
