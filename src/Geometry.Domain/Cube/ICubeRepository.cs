namespace Geometry.Domain.CubeModel;

/// <summary>
/// Repository interface for managing Cube entities.
/// Provides methods for retrieving and persisting Cube instances.
/// </summary>
public interface ICubeRepository
{
    /// <summary>
    /// Retrieves a Cube by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Cube to retrieve.</param>
    /// <returns>The cube with the specified identifier, or null if not found.</returns>
    Task<Cube?> ReadById(Guid id);

    /// <summary>
    /// Saves or updates a Cube entity in the repository.
    /// </summary>
    /// <param name="cube">The Cube entity to save or update.</param>
    Task<Guid> Insert(Cube cube);
}
