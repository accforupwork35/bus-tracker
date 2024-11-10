using BusTrackingService.APIs;
using BusTrackingService.APIs.Common;
using BusTrackingService.APIs.Dtos;
using BusTrackingService.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace BusTrackingService.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class DriversControllerBase : ControllerBase
{
    protected readonly IDriversService _service;

    public DriversControllerBase(IDriversService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Driver
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<Driver>> CreateDriver(DriverCreateInput input)
    {
        var driver = await _service.CreateDriver(input);

        return CreatedAtAction(nameof(Driver), new { id = driver.Id }, driver);
    }

    /// <summary>
    /// Delete one Driver
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteDriver([FromRoute()] DriverWhereUniqueInput uniqueId)
    {
        try
        {
            await _service.DeleteDriver(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Drivers
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<Driver>>> Drivers([FromQuery()] DriverFindManyArgs filter)
    {
        return Ok(await _service.Drivers(filter));
    }

    /// <summary>
    /// Meta data about Driver records
    /// </summary>
    [HttpPost("meta")]
    public async Task<ActionResult<MetadataDto>> DriversMeta(
        [FromQuery()] DriverFindManyArgs filter
    )
    {
        return Ok(await _service.DriversMeta(filter));
    }

    /// <summary>
    /// Get one Driver
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<Driver>> Driver([FromRoute()] DriverWhereUniqueInput uniqueId)
    {
        try
        {
            return await _service.Driver(uniqueId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Driver
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateDriver(
        [FromRoute()] DriverWhereUniqueInput uniqueId,
        [FromQuery()] DriverUpdateInput driverUpdateDto
    )
    {
        try
        {
            await _service.UpdateDriver(uniqueId, driverUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
