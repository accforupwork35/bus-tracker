using BusTrackingService.APIs;
using BusTrackingService.APIs.Common;
using BusTrackingService.APIs.Dtos;
using BusTrackingService.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace BusTrackingService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class BusesControllerBase : ControllerBase
{
    protected readonly IBusesService _service;

    public BusesControllerBase(IBusesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Bus
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Bus>> CreateBus(BusCreateInput input)
    {
        var bus = await _service.CreateBus(input);

        return CreatedAtAction(nameof(Bus), new { id = bus.Id }, bus);
    }

    /// <summary>
    /// Delete one Bus
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteBus([FromRoute()] BusWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteBus(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Buses
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Bus>>> Buses([FromQuery()] BusFindManyArgs filter)
    {
        return Ok(await _service.Buses(filter));
    }

    /// <summary>
    /// Meta data about Bus records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> BusesMeta([FromQuery()] BusFindManyArgs filter)
    {
        return Ok(await _service.BusesMeta(filter));
    }

    /// <summary>
    /// Get one Bus
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Bus>> Bus([FromRoute()] BusWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Bus(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Bus
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateBus(
        [FromRoute()] BusWhereUniqueInput uniqueId,
        [FromQuery()] BusUpdateInput busUpdateDto
    )
    {
        try
        {
            await _service.UpdateBus(uniqueId, busUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
