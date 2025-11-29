namespace Geometry.Infrastructure.Persistence.EFCore;

/// <summary>
/// Database object representation of a Cube entity for Entity Framework Core persistence.
/// This class maps to the database table storing cube information.
/// </summary>
public class CubeDBO
{
    /// <summary>
    /// Gets or sets the unique identifier of the cube.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the length of one side of the cube.
    /// </summary>
    public int SideLength { get; set; }
}
