using Geometry.Domain.CubeModel;

namespace Geometry.Infrastructure.Persistence.EFCore;

/// <summary>
/// Mapper class for converting between Cube domain entities and CubeDBO database objects.
/// Provides bidirectional mapping functionality for persistence operations.
/// </summary>
public static class CubeMapper
{
    /// <summary>
    /// Maps a Cube domain entity to a CubeDBO database object.
    /// </summary>
    /// <param name="cube">The Cube domain entity to map.</param>
    /// <returns>A new CubeDBO instance with properties mapped from the domain entity.</returns>
    /// <exception cref="ArgumentNullException">Thrown when cube is null.</exception>
    public static CubeDBO ToDBO(Cube cube)
    {
        if (cube == null)
        {
            throw new ArgumentNullException(nameof(cube));
        }

        return new CubeDBO
        {
            Id = cube.Id,
            SideLength = cube.SideLength
        };
    }

    /// <summary>
    /// Maps a CubeDBO database object to a Cube domain entity.
    /// </summary>
    /// <param name="cubeDBO">The CubeDBO database object to map.</param>
    /// <returns>A new Cube domain entity instance with properties mapped from the database object.</returns>
    /// <exception cref="ArgumentNullException">Thrown when cubeDBO is null.</exception>
    public static Cube ToDomain(CubeDBO cubeDBO)
    {
        if (cubeDBO == null)
        {
            throw new ArgumentNullException(nameof(cubeDBO));
        }

        return new Cube(cubeDBO.Id, cubeDBO.SideLength);
    }
}
