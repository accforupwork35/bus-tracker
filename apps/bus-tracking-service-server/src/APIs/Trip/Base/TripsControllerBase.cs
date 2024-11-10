using BusTrackingService.APIs;
using BusTrackingService.APIs.Common;
using BusTrackingService.APIs.Dtos;
using BusTrackingService.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace BusTrackingService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class TripsControllerBase : ControllerBase
{
    protected readonly ITripsService _service;

    public TripsControllerBase(ITripsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Trip
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Trip>> CreateTrip(TripCreateInput input)
    {
        var trip = await _service.CreateTrip(input);

        return CreatedAtAction(nameof(Trip), new { id = trip.Id }, trip);
    }

    /// <summary>
    /// Delete one Trip
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteTrip([FromRoute()] TripWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteTrip(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Trips
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Trip>>> Trips([FromQuery()] TripFindManyArgs filter)
    {
        return Ok(await _service.Trips(filter));
    }

    /// <summary>
    /// Meta data about Trip records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> TripsMeta([FromQuery()] TripFindManyArgs filter)
    {
        return Ok(await _service.TripsMeta(filter));
    }

    /// <summary>
    /// Get one Trip
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Trip>> Trip([FromRoute()] TripWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Trip(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Trip
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateTrip(
        [FromRoute()] TripWhereUniqueInput uniqueId,
        [FromQuery()] TripUpdateInput tripUpdateDto
    )
    {
        try
        {
            await _service.UpdateTrip(uniqueId, tripUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
