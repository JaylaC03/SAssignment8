using Geometry.Domain.CylinderModel;

namespace Geometry.Application;

/// <summary>
/// Service class for managing cylinder business logic operations.
/// </summary>
public class CylinderService
{
    private readonly ICylinderRepository _cylinderRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="CylinderService"/> class.
    /// </summary>
    /// <param name="cylinderRepository">The cylinder repository.</param>
    public CylinderService(ICylinderRepository cylinderRepository)
    {
        _cylinderRepository = cylinderRepository;
    }

    /// <summary>
    /// Inserts a new cylinder into the repository.
    /// </summary>
    /// <param name="cylinder">The cylinder to insert.</param>
    /// <returns>The unique identifier of the inserted cylinder.</returns>
    public async Task<Guid> Insert(Cylinder cylinder)
    {
        return await _cylinderRepository.Insert(cylinder);
    }

    /// <summary>
    /// Retrieves a cylinder by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the cylinder.</param>
    /// <returns>The cylinder if found, otherwise null.</returns>
    public async Task<Cylinder?> ReadById(Guid id)
    {
        return await _cylinderRepository.ReadById(id);
    }

    /// <summary>
    /// Updates an existing cylinder in the repository.
    /// </summary>
    /// <param name="cylinder">The cylinder to update.</param>
    public async Task Update(Cylinder cylinder)
    {
        await _cylinderRepository.Update(cylinder);
    }

    /// <summary>
    /// Deletes a cylinder by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the cylinder to delete.</param>
    public async Task Delete(Guid id)
    {
        await _cylinderRepository.Delete(id);
    }
}