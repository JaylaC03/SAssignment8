using Geometry.Domain.CylinderModel;
using Geometry.Infrastructure.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Geometry.Infrastructure.Tests.EFCore;

/// <summary>
/// Tests for CylinderRepository class.
/// </summary>
public class CylinderRepositoryTests
{
    private GeometryDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<GeometryDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new GeometryDbContext(options);
    }

    /// <summary>
    /// Tests that Insert method successfully adds a cylinder to the repository.
    /// </summary>
    [Fact]
    public async Task Insert_ShouldAddCylinderToDatabase()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new CylinderRepository(context);
        var cylinder = new Cylinder(Guid.NewGuid(), 5.0, 10.0);

        // Act
        var id = await repository.Insert(cylinder);

        // Assert
        Assert.Equal(cylinder.Id, id);
        var savedCylinder = await context.Cylinders.FindAsync(id);
        Assert.NotNull(savedCylinder);
        Assert.Equal(5.0, savedCylinder.Radius);
        Assert.Equal(10.0, savedCylinder.Height);
    }

    /// <summary>
    /// Tests that ReadById returns the correct cylinder when it exists.
    /// </summary>
    [Fact]
    public async Task ReadById_ShouldReturnCylinder_WhenCylinderExists()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new CylinderRepository(context);
        var cylinder = new Cylinder(Guid.NewGuid(), 3.5, 7.5);
        await repository.Insert(cylinder);

        // Act
        var result = await repository.ReadById(cylinder.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(cylinder.Id, result.Id);
        Assert.Equal(3.5, result.Radius);
        Assert.Equal(7.5, result.Height);
    }

    /// <summary>
    /// Tests that ReadById returns null when the cylinder does not exist.
    /// </summary>
    [Fact]
    public async Task ReadById_ShouldReturnNull_WhenCylinderDoesNotExist()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new CylinderRepository(context);
        var nonExistentId = Guid.NewGuid();

        // Act
        var result = await repository.ReadById(nonExistentId);

        // Assert
        Assert.Null(result);
    }

    /// <summary>
    /// Tests that Update method successfully modifies an existing cylinder.
    /// </summary>
    [Fact]
    public async Task Update_ShouldModifyCylinder_WhenCylinderExists()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new CylinderRepository(context);
        var cylinder = new Cylinder(Guid.NewGuid(), 4.0, 8.0);
        await repository.Insert(cylinder);

        // Act
        var updatedCylinder = new Cylinder(cylinder.Id, 6.0, 12.0);
        await repository.Update(updatedCylinder);

        // Assert
        var result = await repository.ReadById(cylinder.Id);
        Assert.NotNull(result);
        Assert.Equal(6.0, result.Radius);
        Assert.Equal(12.0, result.Height);
    }

    /// <summary>
    /// Tests that Delete method removes a cylinder from the repository.
    /// </summary>
    [Fact]
    public async Task Delete_ShouldRemoveCylinder_WhenCylinderExists()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new CylinderRepository(context);
        var cylinder = new Cylinder(Guid.NewGuid(), 2.5, 5.0);
        await repository.Insert(cylinder);

        // Act
        await repository.Delete(cylinder.Id);

        // Assert
        var result = await repository.ReadById(cylinder.Id);
        Assert.Null(result);
    }

    /// <summary>
    /// Tests that Delete method does not throw when cylinder does not exist.
    /// </summary>
    [Fact]
    public async Task Delete_ShouldNotThrow_WhenCylinderDoesNotExist()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new CylinderRepository(context);
        var nonExistentId = Guid.NewGuid();

        // Act & Assert
        await repository.Delete(nonExistentId); // Should not throw
    }
}