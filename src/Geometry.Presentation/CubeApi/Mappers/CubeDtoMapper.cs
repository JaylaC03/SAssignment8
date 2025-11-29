using Geometry.Domain.CubeModel;
using Geometry.Presentation.CubeApi.DTOs;

namespace Geometry.Presentation.CubeApi.Mappers;

/// <summary>
/// Mapper class for converting between Cube domain entities and DTOs.
/// Provides bidirectional mapping between domain objects and data transfer objects.
/// </summary>
public static class CubeDtoMapper
{
    /// <summary>
    /// Converts a Cube domain entity to a CubeResponse DTO.
    /// </summary>
    /// <param name="cube">The Cube domain entity to convert. Cannot be null.</param>
    /// <returns>A CubeResponse DTO containing the cube's Id and SideLength.</returns>
    /// <exception cref="ArgumentNullException">Thrown when cube is null.</exception>
    public static CubeResponse ToDto(Cube cube)
    {
        if (cube == null)
        {
            throw new ArgumentNullException(nameof(cube));
        }

        return new CubeResponse
        {
            Id = cube.Id,
            SideLength = cube.SideLength
        };
    }

    /// <summary>
    /// Converts a CreateCubeRequest DTO to a Cube domain entity.
    /// Generates a new GUID for the cube's Id.
    /// </summary>
    /// <param name="request">The CreateCubeRequest DTO to convert. Cannot be null.</param>
    /// <returns>A new Cube domain entity with the specified side length.</returns>
    /// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
    /// <exception cref="ArgumentException">Thrown when SideLength is less than or equal to 0.</exception>
    public static Cube ToDomain(CreateCubeRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        return new Cube(Guid.NewGuid(), request.SideLength);
    }
}
