using Geometry.Domain.CubeModel;

namespace Geometry.Application.Tests;

/// <summary>
/// Mock implementation of ICubeRepository for testing purposes.
/// </summary>
public class MockCubeRepository : ICubeRepository
{
    private readonly Dictionary<Guid, Cube> _cubes = new();
    public int ReadByIdCallCount { get; private set; }
    public int InsertCallCount { get; private set; }
    public Guid? LastReadByIdParameter { get; private set; }
    public Cube? LastInsertParameter { get; private set; }

    public Task<Cube?> ReadById(Guid id)
    {
        ReadByIdCallCount++;
        LastReadByIdParameter = id;
        _cubes.TryGetValue(id, out var cube);
        return Task.FromResult<Cube?>(cube);
    }

    public Task<Guid> Insert(Cube cube)
    {
        InsertCallCount++;
        LastInsertParameter = cube;
        if (cube != null)
        {
            _cubes[cube.Id] = cube;
            return Task.FromResult(cube.Id);
        }
        throw new ArgumentNullException(nameof(cube));
    }

    public void Reset()
    {
        _cubes.Clear();
        ReadByIdCallCount = 0;
        InsertCallCount = 0;
        LastReadByIdParameter = null;
        LastInsertParameter = null;
    }
}

public class CubeServiceTests
{
    [Fact]
    public void Constructor_WithNullRepository_ShouldCreateInstance()
    {
        // Arrange
        ICubeRepository repository = null!;

        // Act
        var service = new CubeService(repository);

        // Assert
        Assert.NotNull(service);
        // Note: Methods will throw NullReferenceException if repository is null
    }

    [Fact]
    public async Task Insert_WithNullRepository_ShouldThrowNullReferenceException()
    {
        // Arrange
        ICubeRepository repository = null!;
        var service = new CubeService(repository);
        var cube = new Cube(Guid.NewGuid(), 5);

        // Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(() => service.Insert(cube));
    }

    [Fact]
    public async Task ReadById_WithNullRepository_ShouldThrowNullReferenceException()
    {
        // Arrange
        ICubeRepository repository = null!;
        var service = new CubeService(repository);
        var id = Guid.NewGuid();

        // Act & Assert
        await Assert.ThrowsAsync<NullReferenceException>(() => service.ReadById(id));
    }

    [Fact]
    public void Constructor_WithValidRepository_ShouldCreateInstance()
    {
        // Arrange
        var repository = new MockCubeRepository();

        // Act
        var service = new CubeService(repository);

        // Assert
        Assert.NotNull(service);
    }

    [Fact]
    public async Task Insert_WithValidCube_ShouldDelegateToRepository()
    {
        // Arrange
        var repository = new MockCubeRepository();
        var service = new CubeService(repository);
        var id = Guid.NewGuid();
        var cube = new Cube(id, 5);

        // Act
        var result = await service.Insert(cube);

        // Assert
        Assert.Equal(id, result);
        Assert.Equal(1, repository.InsertCallCount);
        Assert.Equal(cube, repository.LastInsertParameter);
    }

    [Fact]
    public async Task Insert_WithValidCube_ShouldReturnCubeId()
    {
        // Arrange
        var repository = new MockCubeRepository();
        var service = new CubeService(repository);
        var id = Guid.NewGuid();
        var cube = new Cube(id, 10);

        // Act
        var result = await service.Insert(cube);

        // Assert
        Assert.Equal(id, result);
    }

    [Fact]
    public async Task Insert_WithNullCube_ShouldPropagateException()
    {
        // Arrange
        var repository = new MockCubeRepository();
        var service = new CubeService(repository);
        Cube cube = null!;

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.Insert(cube));
    }

    [Fact]
    public async Task ReadById_WithExistingCube_ShouldDelegateToRepository()
    {
        // Arrange
        var repository = new MockCubeRepository();
        var service = new CubeService(repository);
        var id = Guid.NewGuid();
        var cube = new Cube(id, 5);

        // First insert the cube
        await repository.Insert(cube);

        // Act
        var result = await service.ReadById(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal(5, result.SideLength);
        Assert.Equal(1, repository.ReadByIdCallCount);
        Assert.Equal(id, repository.LastReadByIdParameter);
    }

    [Fact]
    public async Task ReadById_WithNonExistentId_ShouldReturnNull()
    {
        // Arrange
        var repository = new MockCubeRepository();
        var service = new CubeService(repository);
        var nonExistentId = Guid.NewGuid();

        // Act
        var result = await service.ReadById(nonExistentId);

        // Assert
        Assert.Null(result);
        Assert.Equal(1, repository.ReadByIdCallCount);
        Assert.Equal(nonExistentId, repository.LastReadByIdParameter);
    }

    [Fact]
    public async Task Insert_ThenReadById_ShouldReturnInsertedCube()
    {
        // Arrange
        var repository = new MockCubeRepository();
        var service = new CubeService(repository);
        var id = Guid.NewGuid();
        var cube = new Cube(id, 7);

        // Act
        await service.Insert(cube);
        var result = await service.ReadById(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(id, result.Id);
        Assert.Equal(7, result.SideLength);
    }

    [Fact]
    public async Task MultipleInserts_ShouldDelegateToRepositoryEachTime()
    {
        // Arrange
        var repository = new MockCubeRepository();
        var service = new CubeService(repository);
        var cube1 = new Cube(Guid.NewGuid(), 5);
        var cube2 = new Cube(Guid.NewGuid(), 10);
        var cube3 = new Cube(Guid.NewGuid(), 15);

        // Act
        await service.Insert(cube1);
        await service.Insert(cube2);
        await service.Insert(cube3);

        // Assert
        Assert.Equal(3, repository.InsertCallCount);
        Assert.Equal(cube3, repository.LastInsertParameter);
    }

    [Fact]
    public async Task MultipleReads_ShouldDelegateToRepositoryEachTime()
    {
        // Arrange
        var repository = new MockCubeRepository();
        var service = new CubeService(repository);
        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();
        var cube1 = new Cube(id1, 5);
        var cube2 = new Cube(id2, 10);

        await repository.Insert(cube1);
        await repository.Insert(cube2);

        // Act
        var result1 = await service.ReadById(id1);
        var result2 = await service.ReadById(id2);
        var result3 = await service.ReadById(Guid.NewGuid());

        // Assert
        Assert.NotNull(result1);
        Assert.NotNull(result2);
        Assert.Null(result3);
        Assert.Equal(3, repository.ReadByIdCallCount);
    }

    [Fact]
    public async Task Insert_WithDifferentSideLengths_ShouldWorkCorrectly()
    {
        // Arrange
        var repository = new MockCubeRepository();
        var service = new CubeService(repository);
        var sideLengths = new[] { 1, 5, 10, 100, 1000 };

        // Act & Assert
        foreach (var sideLength in sideLengths)
        {
            var id = Guid.NewGuid();
            var cube = new Cube(id, sideLength);
            var result = await service.Insert(cube);

            Assert.Equal(id, result);
            var retrieved = await service.ReadById(id);
            Assert.NotNull(retrieved);
            Assert.Equal(sideLength, retrieved.SideLength);
        }
    }

    [Fact]
    public async Task Service_ShouldMaintainRepositoryReference()
    {
        // Arrange
        var repository = new MockCubeRepository();
        var service = new CubeService(repository);
        var cube = new Cube(Guid.NewGuid(), 5);

        // Act
        await service.Insert(cube);
        var retrieved = await service.ReadById(cube.Id);

        // Assert
        Assert.NotNull(retrieved);
        // Verify that the same repository instance was used
        Assert.Equal(1, repository.InsertCallCount);
        Assert.Equal(1, repository.ReadByIdCallCount);
    }
}
