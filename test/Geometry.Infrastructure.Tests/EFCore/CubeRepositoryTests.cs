using Geometry.Domain.CubeModel;
using Geometry.Infrastructure.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Geometry.Infrastructure.Tests;

public class CubeRepositoryTests
{
    private GeometryDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<GeometryDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new GeometryDbContext(options);
    }

    [Fact]
    public void Constructor_WithNullContext_ShouldThrowArgumentNullException()
    {
        // Arrange
        GeometryDbContext context = null!;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new CubeRepository(context));
    }

    [Fact]
    public void Constructor_WithValidContext_ShouldCreateInstance()
    {
        // Arrange
        using var context = CreateContext();

        // Act
        var repository = new CubeRepository(context);

        // Assert
        Assert.NotNull(repository);
    }

    [Fact]
    public async Task ReadById_WithExistingCube_ShouldReturnCube()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new CubeRepository(context);
        var id = Guid.NewGuid();
        var cube = new Cube(id, 5);

        await repository.Insert(cube);

        // Act
        var result = await repository.ReadById(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal(5, result.SideLength);
    }

    [Fact]
    public async Task ReadById_WithNonExistentId_ShouldReturnNull()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new CubeRepository(context);
        var nonExistentId = Guid.NewGuid();

        // Act
        var result = await repository.ReadById(nonExistentId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Insert_WithNewCube_ShouldSaveAndReturnId()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new CubeRepository(context);
        var id = Guid.NewGuid();
        var cube = new Cube(id, 10);

        // Act
        var result = await repository.Insert(cube);

        // Assert
        Assert.Equal(id, result);

        // Verify it was saved
        var retrieved = await repository.ReadById(id);
        Assert.NotNull(retrieved);
        Assert.Equal(10, retrieved.SideLength);
    }

    [Fact]
    public async Task Insert_WithNullCube_ShouldThrowArgumentNullException()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new CubeRepository(context);
        Cube cube = null!;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => repository.Insert(cube));
    }

    [Fact]
    public async Task Insert_WithExistingId_ShouldUpdateExistingCube()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new CubeRepository(context);
        var id = Guid.NewGuid();
        var cube1 = new Cube(id, 5);
        var cube2 = new Cube(id, 15);

        await repository.Insert(cube1);

        // Act
        await repository.Insert(cube2);

        // Assert
        var retrieved = await repository.ReadById(id);
        Assert.NotNull(retrieved);
        Assert.Equal(15, retrieved.SideLength); // Should be updated to cube2's value
    }

    [Fact]
    public async Task Insert_MultipleDifferentCubes_ShouldSaveAll()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new CubeRepository(context);
        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();
        var cube1 = new Cube(id1, 5);
        var cube2 = new Cube(id2, 10);

        // Act
        await repository.Insert(cube1);
        await repository.Insert(cube2);

        // Assert
        var retrieved1 = await repository.ReadById(id1);
        var retrieved2 = await repository.ReadById(id2);

        Assert.NotNull(retrieved1);
        Assert.NotNull(retrieved2);
        Assert.Equal(5, retrieved1.SideLength);
        Assert.Equal(10, retrieved2.SideLength);
    }

    [Fact]
    public async Task ReadById_WithEmptyDatabase_ShouldReturnNull()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new CubeRepository(context);

        // Act
        var result = await repository.ReadById(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Insert_ShouldPersistDataAcrossReadOperations()
    {
        // Arrange
        using var context = CreateContext();
        var repository = new CubeRepository(context);
        var id = Guid.NewGuid();
        var cube = new Cube(id, 20);

        // Act
        await repository.Insert(cube);
        var result1 = await repository.ReadById(id);
        var result2 = await repository.ReadById(id);

        // Assert
        Assert.NotNull(result1);
        Assert.NotNull(result2);
        Assert.Equal(result1.Id, result2.Id);
        Assert.Equal(result1.SideLength, result2.SideLength);
    }
}
