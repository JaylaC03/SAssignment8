using Geometry.Domain.CubeModel;

namespace Geometry.Domain.Tests;

/// <summary>
/// In-memory implementation of ICubeRepository for testing purposes.
/// </summary>
public class InMemoryCubeRepository : ICubeRepository
{
    private readonly Dictionary<Guid, Cube> _cubes = new();

    public Task<Cube?> ReadById(Guid id)
    {
        _cubes.TryGetValue(id, out var cube);
        return Task.FromResult<Cube?>(cube);
    }

    public Task<Guid> Insert(Cube cube)
    {
        if (cube == null)
        {
            throw new ArgumentNullException(nameof(cube));
        }

        _cubes[cube.Id] = cube;
        return Task.FromResult(cube.Id);
    }

    public void Clear()
    {
        _cubes.Clear();
    }

    public int Count => _cubes.Count;
}

public class ICubeRepositoryTests
{
    [Fact]
    public async Task ReadById_WithExistingCube_ShouldReturnCube()
    {
        // Arrange
        var repository = new InMemoryCubeRepository();
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
        var repository = new InMemoryCubeRepository();
        var nonExistentId = Guid.NewGuid();

        // Act
        var result = await repository.ReadById(nonExistentId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Insert_WithValidCube_ShouldReturnCubeId()
    {
        // Arrange
        var repository = new InMemoryCubeRepository();
        var id = Guid.NewGuid();
        var cube = new Cube(id, 10);

        // Act
        var result = await repository.Insert(cube);

        // Assert
        Assert.Equal(id, result);
    }

    [Fact]
    public async Task Insert_WithValidCube_ShouldStoreCube()
    {
        // Arrange
        var repository = new InMemoryCubeRepository();
        var id = Guid.NewGuid();
        var cube = new Cube(id, 10);

        // Act
        await repository.Insert(cube);

        // Assert
        var retrieved = await repository.ReadById(id);
        Assert.NotNull(retrieved);
        Assert.Equal(id, retrieved.Id);
        Assert.Equal(10, retrieved.SideLength);
    }

    [Fact]
    public async Task Insert_WithNullCube_ShouldThrowArgumentNullException()
    {
        // Arrange
        var repository = new InMemoryCubeRepository();

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => repository.Insert(null!));
    }

    [Fact]
    public async Task Insert_WithSameId_ShouldUpdateExistingCube()
    {
        // Arrange
        var repository = new InMemoryCubeRepository();
        var id = Guid.NewGuid();
        var cube1 = new Cube(id, 5);
        var cube2 = new Cube(id, 10);

        // Act
        await repository.Insert(cube1);
        await repository.Insert(cube2);

        // Assert
        var retrieved = await repository.ReadById(id);
        Assert.NotNull(retrieved);
        Assert.Equal(10, retrieved.SideLength); // Should be updated to cube2's value
    }

    [Fact]
    public async Task ReadById_WithEmptyRepository_ShouldReturnNull()
    {
        // Arrange
        var repository = new InMemoryCubeRepository();

        // Act
        var result = await repository.ReadById(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task Insert_MultipleCubes_ShouldStoreAllCubes()
    {
        // Arrange
        var repository = new InMemoryCubeRepository();
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
}
