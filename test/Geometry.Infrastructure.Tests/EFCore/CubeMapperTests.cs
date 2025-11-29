using Geometry.Domain.CubeModel;
using Geometry.Infrastructure.Persistence.EFCore;

namespace Geometry.Infrastructure.Tests;

public class CubeMapperTests
{
    [Fact]
    public void ToDBO_WithValidCube_ShouldMapCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();
        var sideLength = 5;
        var cube = new Cube(id, sideLength);

        // Act
        var cubeDBO = CubeMapper.ToDBO(cube);

        // Assert
        Assert.NotNull(cubeDBO);
        Assert.Equal(id, cubeDBO.Id);
        Assert.Equal(sideLength, cubeDBO.SideLength);
    }

    [Fact]
    public void ToDBO_WithNullCube_ShouldThrowArgumentNullException()
    {
        // Arrange
        Cube cube = null!;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => CubeMapper.ToDBO(cube));
    }

    [Fact]
    public void ToDomain_WithValidCubeDBO_ShouldMapCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();
        var sideLength = 10;
        var cubeDBO = new CubeDBO
        {
            Id = id,
            SideLength = sideLength
        };

        // Act
        var cube = CubeMapper.ToDomain(cubeDBO);

        // Assert
        Assert.NotNull(cube);
        Assert.Equal(id, cube.Id);
        Assert.Equal(sideLength, cube.SideLength);
    }

    [Fact]
    public void ToDomain_WithNullCubeDBO_ShouldThrowArgumentNullException()
    {
        // Arrange
        CubeDBO cubeDBO = null!;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => CubeMapper.ToDomain(cubeDBO));
    }

    [Fact]
    public void ToDBO_ThenToDomain_ShouldRoundTripCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();
        var sideLength = 7;
        var originalCube = new Cube(id, sideLength);

        // Act
        var cubeDBO = CubeMapper.ToDBO(originalCube);
        var roundTrippedCube = CubeMapper.ToDomain(cubeDBO);

        // Assert
        Assert.Equal(originalCube.Id, roundTrippedCube.Id);
        Assert.Equal(originalCube.SideLength, roundTrippedCube.SideLength);
    }

    [Fact]
    public void ToDomain_ThenToDBO_ShouldRoundTripCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();
        var sideLength = 12;
        var originalCubeDBO = new CubeDBO
        {
            Id = id,
            SideLength = sideLength
        };

        // Act
        var cube = CubeMapper.ToDomain(originalCubeDBO);
        var roundTrippedCubeDBO = CubeMapper.ToDBO(cube);

        // Assert
        Assert.Equal(originalCubeDBO.Id, roundTrippedCubeDBO.Id);
        Assert.Equal(originalCubeDBO.SideLength, roundTrippedCubeDBO.SideLength);
    }

    [Fact]
    public void ToDBO_WithDifferentSideLengths_ShouldMapCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();
        var sideLengths = new[] { 1, 5, 10, 100, 1000 };

        foreach (var sideLength in sideLengths)
        {
            var cube = new Cube(id, sideLength);

            // Act
            var cubeDBO = CubeMapper.ToDBO(cube);

            // Assert
            Assert.Equal(sideLength, cubeDBO.SideLength);
        }
    }
}
