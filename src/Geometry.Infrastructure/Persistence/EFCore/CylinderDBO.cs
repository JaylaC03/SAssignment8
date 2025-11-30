namespace Geometry.Infrastructure.Persistence.EFCore;

/// <summary>
/// Database object representing a Cylinder entity for Entity Framework Core.
/// </summary>
public class CylinderDBO
{
    /// <summary>
    /// Gets or sets the unique identifier for the cylinder.
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