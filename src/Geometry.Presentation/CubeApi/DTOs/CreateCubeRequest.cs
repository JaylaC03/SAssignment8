namespace Geometry.Presentation.CubeApi.DTOs;

/// <summary>
/// Data Transfer Object for creating a new cube.
/// Represents the request payload for the POST /cube endpoint.
/// </summary>
public class CreateCubeRequest
{
    /// <summary>
    /// Gets or sets the length of one side of the cube.
    /// Must be greater than 0.
    /// </summary>
    /// <example>5</example>
    public int SideLength { get; set; }
}
