using Geometry.Application;
using Geometry.Presentation.CylinderApi.DTOs;
using Geometry.Presentation.CylinderApi.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Geometry.Presentation.CylinderApi.Controllers;

/// <summary>
/// Controller for managing cylinder operations via REST API.
/// Provides endpoints for creating, reading, updating, and deleting cylinder entities.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CylinderController : ControllerBase
{
    private readonly CylinderService _cylinderService;
    private readonly ILogger<CylinderController> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="CylinderController"/> class.
    /// </summary>
    /// <param name="cylinderService">The cylinder service for business logic operations.</param>
    /// <param name="logger">The logger instance for logging operations.</param>
    public CylinderController(CylinderService cylinderService, ILogger<CylinderController> logger)
    {
        _cylinderService = cylinderService ?? throw new ArgumentNullException(nameof(cylinderService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Creates a new cylinder with the specified radius and height.
    /// </summary>
    /// <param name="request">The cylinder creation request containing the radius and height.</param>
    /// <returns>
    /// Created (201) with the created cylinder's identifier and location header,
    /// or BadRequest (400) if the request is invalid.
    /// </returns>
    /// <response code="201">Returns the created cylinder identifier</response>
    /// <response code="400">If the request is invalid</response>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Guid>> CreateCylinder([FromBody] CreateCylinderRequest request)
    {
        if (request == null)
        {
            _logger.LogWarning("CreateCylinder called with null request");
            return BadRequest("Request body cannot be null.");
        }

        if (request.Radius <= 0)
        {
            _logger.LogWarning("CreateCylinder called with invalid radius: {Radius}", request.Radius);
            return BadRequest("Radius must be greater than 0.");
        }

        if (request.Height <= 0)
        {
            _logger.LogWarning("CreateCylinder called with invalid height: {Height}", request.Height);
            return BadRequest("Height must be greater than 0.");
        }

        try
        {
            var cylinder = CylinderDtoMapper.ToDomain(request);
            var id = await _cylinderService.Insert(cylinder);

            _logger.LogInformation("Cylinder created successfully with Id: {CylinderId}, Radius: {Radius}, Height: {Height}",
                id, request.Radius, request.Height);

            return CreatedAtAction(
                nameof(GetCylinderById),
                new { id = id },
                id);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "ArgumentException occurred while creating cylinder");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while creating cylinder");
            return StatusCode(500, "An error occurred while creating the cylinder.");
        }
    }

    /// <summary>
    /// Retrieves a cylinder by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the cylinder to retrieve.</param>
    /// <returns>
    /// OK (200) with the cylinder data if found,
    /// or NotFound (404) if the cylinder does not exist.
    /// </returns>
    /// <response code="200">Returns the cylinder data</response>
    /// <response code="404">If the cylinder is not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CylinderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CylinderResponse>> GetCylinderById(Guid id)
    {
        try
        {
            var cylinder = await _cylinderService.ReadById(id);

            if (cylinder == null)
            {
                _logger.LogWarning("Cylinder with Id {CylinderId} not found", id);
                return NotFound($"Cylinder with Id {id} was not found.");
            }

            _logger.LogInformation("Cylinder retrieved successfully with Id: {CylinderId}", id);
            var response = CylinderDtoMapper.ToDto(cylinder);
            return Ok(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while retrieving cylinder with Id: {CylinderId}", id);
            return StatusCode(500, "An error occurred while retrieving the cylinder.");
        }
    }

    /// <summary>
    /// Updates an existing cylinder with new radius and height values.
    /// </summary>
    /// <param name="id">The unique identifier of the cylinder to update.</param>
    /// <param name="request">The cylinder update request containing the new radius and height.</param>
    /// <returns>
    /// NoContent (204) if the update was successful,
    /// BadRequest (400) if the request is invalid,
    /// or NotFound (404) if the cylinder does not exist.
    /// </returns>
    /// <response code="204">If the cylinder was updated successfully</response>
    /// <response code="400">If the request is invalid</response>
    /// <response code="404">If the cylinder is not found</response>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCylinder(Guid id, [FromBody] UpdateCylinderRequest request)
    {
        if (request == null)
        {
            _logger.LogWarning("UpdateCylinder called with null request");
            return BadRequest("Request body cannot be null.");
        }

        if (id != request.Id)
        {
            _logger.LogWarning("UpdateCylinder called with mismatched Id. URL Id: {UrlId}, Request Id: {RequestId}", id, request.Id);
            return BadRequest("The Id in the URL does not match the Id in the request body.");
        }

        if (request.Radius <= 0)
        {
            _logger.LogWarning("UpdateCylinder called with invalid radius: {Radius}", request.Radius);
            return BadRequest("Radius must be greater than 0.");
        }

        if (request.Height <= 0)
        {
            _logger.LogWarning("UpdateCylinder called with invalid height: {Height}", request.Height);
            return BadRequest("Height must be greater than 0.");
        }

        try
        {
            var existingCylinder = await _cylinderService.ReadById(id);
            if (existingCylinder == null)
            {
                _logger.LogWarning("Cylinder with Id {CylinderId} not found for update", id);
                return NotFound($"Cylinder with Id {id} was not found.");
            }

            var cylinder = CylinderDtoMapper.ToDomain(request);
            await _cylinderService.Update(cylinder);

            _logger.LogInformation("Cylinder updated successfully with Id: {CylinderId}", id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "ArgumentException occurred while updating cylinder");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while updating cylinder with Id: {CylinderId}", id);
            return StatusCode(500, "An error occurred while updating the cylinder.");
        }
    }

    /// <summary>
    /// Deletes a cylinder by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the cylinder to delete.</param>
    /// <returns>
    /// NoContent (204) if the deletion was successful,
    /// or NotFound (404) if the cylinder does not exist.
    /// </returns>
    /// <response code="204">If the cylinder was deleted successfully</response>
    /// <response code="404">If the cylinder is not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCylinder(Guid id)
    {
        try
        {
            var existingCylinder = await _cylinderService.ReadById(id);
            if (existingCylinder == null)
            {
                _logger.LogWarning("Cylinder with Id {CylinderId} not found for deletion", id);
                return NotFound($"Cylinder with Id {id} was not found.");
            }

            await _cylinderService.Delete(id);

            _logger.LogInformation("Cylinder deleted successfully with Id: {CylinderId}", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error occurred while deleting cylinder with Id: {CylinderId}", id);
            return StatusCode(500, "An error occurred while deleting the cylinder.");
        }
    }
}