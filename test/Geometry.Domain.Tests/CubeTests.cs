using Geometry.Domain.CubeModel;

namespace Geometry.Domain.Tests;

public class CubeTests
{
    [Fact]
    public void Constructor_WithValidParameters_ShouldCreateCube()
    {
        // Arrange
        var id = Guid.NewGuid();
        var sideLength = 5;

        // Act
        var cube = new Cube(id, sideLength);

        // Assert
        Assert.NotNull(cube);
        Assert.Equal(id, cube.Id);
        Assert.Equal(sideLength, cube.SideLength);
    }

    [Fact]
    public void Constructor_WithZeroSideLength_ShouldThrowArgumentException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var sideLength = 0;

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Cube(id, sideLength));
    }

    [Fact]
    public void Constructor_WithNegativeSideLength_ShouldThrowArgumentException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var sideLength = -5;

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => new Cube(id, sideLength));
        Assert.Contains("SideLength must be greater than 0", exception.Message);
    }

    [Fact]
    public void SideLength_SetValidValue_ShouldUpdateProperty()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cube = new Cube(id, 5);

        // Act
        cube.SideLength = 10;

        // Assert
        Assert.Equal(10, cube.SideLength);
    }

    [Fact]
    public void SideLength_SetZero_ShouldThrowArgumentException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cube = new Cube(id, 5);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => cube.SideLength = 0);
        Assert.Contains("SideLength must be greater than 0", exception.Message);
        Assert.Equal("value", exception.ParamName);
    }

    [Fact]
    public void SideLength_SetNegativeValue_ShouldThrowArgumentException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cube = new Cube(id, 5);

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => cube.SideLength = -10);
        Assert.Contains("SideLength must be greater than 0", exception.Message);
        Assert.Equal("value", exception.ParamName);
    }

    [Fact]
    public void SideLength_SetSameValue_ShouldNotThrowException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var sideLength = 5;
        var cube = new Cube(id, sideLength);

        // Act
        cube.SideLength = sideLength;

        // Assert
        Assert.Equal(sideLength, cube.SideLength);
    }

    [Fact]
    public void Constructor_WithPositiveSideLength_ShouldAcceptValue()
    {
        // Arrange
        var id = Guid.NewGuid();
        var sideLength = 1; // Minimum valid value

        // Act
        var cube = new Cube(id, sideLength);

        // Assert
        Assert.Equal(sideLength, cube.SideLength);
    }

    [Fact]
    public void Id_ShouldBeReadOnlyAfterConstruction()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cube = new Cube(id, 5);

        // Assert
        Assert.Equal(id, cube.Id);
        // Note: Id is protected set in Entity, so we can't change it from outside
        // This test verifies the Id is set correctly during construction
    }
}
