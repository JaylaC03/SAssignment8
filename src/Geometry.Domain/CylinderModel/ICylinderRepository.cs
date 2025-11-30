namespace Geometry.Domain.CylinderModel;

/// <summary>
/// Repository interface for managing Cylinder entities.
/// Provides methods for retrieving, persisting, updating, and deleting Cylinder instances.
/// </summary>
public interface ICylinderRepository
{
    /// <summary>
    /// Retrieves a Cylinder by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Cylinder to retrieve.</param>
    /// <returns>The cylinder with the specified identifier, or null if not found.</returns>
    Task<Cylinder?> ReadById(Guid id);

    /// <summary>
    /// Saves a new Cylinder entity in the repository.
    /// </summary>
    /// <param name="cylinder">The Cylinder entity to save.</param>
    /// <returns>The unique identifier of the saved cylinder.</returns>
    Task<Guid> Insert(Cylinder cylinder);

    /// <summary>
    /// Updates an existing Cylinder entity in the repository.
    /// </summary>
    /// <param name="cylinder">The Cylinder entity to update.</param>
    Task Update(Cylinder cylinder);

    /// <summary>
    /// Deletes a Cylinder entity from the repository.
    /// </summary>
    /// <param name="id">The unique identifier of the Cylinder to delete.</param>
    Task Delete(Guid id);
}