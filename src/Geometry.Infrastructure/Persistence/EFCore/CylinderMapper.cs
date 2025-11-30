using Geometry.Domain.CylinderModel;

namespace Geometry.Infrastructure.Persistence.EFCore;

/// <summary>
/// Mapper class for converting between Cylinder domain models and database objects.
/// </summary>
public static class CylinderMapper
{
    /// <summary>
    /// Converts a Cylinder domain model to a database object.
    /// </summary>
    /// <param name="cylinder">The domain model to convert.</param>
    /// <returns>A CylinderDBO instance.</returns>
    public static CylinderDBO ToDBO(Cylinder cylinder)
    {
        return new CylinderDBO
        {
            Id = cylinder.Id,
            Radius = cylinder.Radius,
            Height = cylinder.Height
        };
    }

    /// <summary>
    /// Converts a database object to a Cylinder domain model.
    /// </summary>
    /// <param name="dbo">The database object to convert.</param>
    /// <returns>A Cylinder domain model instance.</returns>
    public static Cylinder ToDomain(CylinderDBO dbo)
    {
        return new Cylinder(dbo.Id, dbo.Radius, dbo.Height);
    }
}