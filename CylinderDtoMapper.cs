using Geometry.Domain.CylinderModel;
using Geometry.Presentation.CylinderApi.DTOs;

namespace Geometry.Presentation.CylinderApi.Mappers;

/// <summary>
/// Mapper class for converting between Cylinder DTOs and domain models.
/// </summary>
public static class CylinderDtoMapper
{
    /// <summary>
    /// Converts a CreateCylinderRequest DTO to a Cylinder domain model.
    /// </summary>
    /// <param name="request">The create request DTO.</param>
    /// <returns>A Cylinder domain model instance.</returns>
    public static Cylinder ToDomain(CreateCylinderRequest request)
    {
        return new Cylinder(Guid.NewGuid(), request.Radius, request.Height);
    }

    /// <summary>
    /// Converts an UpdateCylinderRequest DTO to a Cylinder domain model.
    /// </summary>
    /// <param name="request">The update request DTO.</param>
    /// <returns>A Cylinder domain model instance.</returns>
    public static Cylinder ToDomain(UpdateCylinderRequest request)
    {
        return new Cylinder(request.Id, request.Radius, request.Height);
    }

    /// <summary>
    /// Converts a Cylinder domain model to a CylinderResponse DTO.
    /// </summary>
    /// <param name="cylinder">The domain model.</param>
    /// <returns>A CylinderResponse DTO instance.</returns>
    public static CylinderResponse ToDto(Cylinder cylinder)
    {
        return new CylinderResponse
        {
            Id = cylinder.Id,
            Radius = cylinder.Radius,
            Height = cylinder.Height
        };
    }
}