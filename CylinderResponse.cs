namespace Geometry.Presentation.CylinderApi.DTOs;

/// <summary>
/// Data Transfer Object representing a cylinder response.
/// Returned by GET endpoints.
/// </summary>
public class CylinderResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the cylinder.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the radius of the cylinder's base.
    /// </summary>
    public double Radius { get; set; }

    /// <summary>
    /// Gets or sets the height of the cylinder.
    /// </summary>
    public double Height { get; set; }
}