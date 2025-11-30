namespace Geometry.Presentation.CylinderApi.DTOs;

/// <summary>
/// Data Transfer Object for creating a new cylinder.
/// Represents the request payload for the POST /cylinder endpoint.
/// </summary>
public class CreateCylinderRequest
{
    /// <summary>
    /// Gets or sets the radius of the cylinder's base.
    /// Must be greater than 0.
    /// </summary>
    /// <example>5.0</example>
    public double Radius { get; set; }

    /// <summary>
    /// Gets or sets the height of the cylinder.
    /// Must be greater than 0.
    /// </summary>
    /// <example>10.0</example>
    public double Height { get; set; }
}