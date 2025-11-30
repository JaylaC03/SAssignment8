using Geometry.Domain.CylinderModel;
using Microsoft.EntityFrameworkCore;

namespace Geometry.Infrastructure.Persistence.EFCore;

/// <summary>
/// Repository implementation for managing Cylinder entities using Entity Framework Core.
/// </summary>
public class CylinderRepository : ICylinderRepository
{
    private readonly GeometryDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="CylinderRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public CylinderRepository(GeometryDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <summary>
    /// Retrieves a Cylinder by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the Cylinder to retrieve.</param>
    /// <returns>The cylinder with the specified identifier, or null if not found.</returns>
    public async Task<Cylinder?> ReadById(Guid id)
    {
        var dbo = await _context.Cylinders.FindAsync(id);
        return dbo == null ? null : CylinderMapper.ToDomain(dbo);
    }

    /// <summary>
    /// Saves a new Cylinder entity in the repository.
    /// </summary>
    /// <param name="cylinder">The Cylinder entity to save.</param>
    /// <returns>The unique identifier of the saved cylinder.</returns>
    public async Task<Guid> Insert(Cylinder cylinder)
    {
        var dbo = CylinderMapper.ToDBO(cylinder);
        await _context.Cylinders.AddAsync(dbo);
        await _context.SaveChangesAsync();
        return dbo.Id;
    }

    /// <summary>
    /// Updates an existing Cylinder entity in the repository.
    /// </summary>
    /// <param name="cylinder">The Cylinder entity to update.</param>
    public async Task Update(Cylinder cylinder)
    {
        var dbo = CylinderMapper.ToDBO(cylinder);
        _context.Cylinders.Update(dbo);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes a Cylinder entity from the repository.
    /// </summary>
    /// <param name="id">The unique identifier of the Cylinder to delete.</param>
    public async Task Delete(Guid id)
    {
        var dbo = await _context.Cylinders.FindAsync(id);
        if (dbo != null)
        {
            _context.Cylinders.Remove(dbo);
            await _context.SaveChangesAsync();
        }
    }
}