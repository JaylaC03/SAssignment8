using Geometry.Infrastructure.Persistence.EFCore;

namespace Geometry.Infrastructure.Tests;

public class CubeDBOTests
{
    [Fact]
    public void Id_ShouldBeSetAndRetrieved()
    {
        // Arrange
        var cubeDBO = new CubeDBO();
        var id = Guid.NewGuid();

        // Act
        cubeDBO.Id = id;

        // Assert
        Assert.Equal(id, cubeDBO.Id);
    }

    [Fact]
    public void SideLength_ShouldBeSetAndRetrieved()
    {
        // Arrange
        var cubeDBO = new CubeDBO();
        var sideLength = 5;

        // Act
        cubeDBO.SideLength = sideLength;

        // Assert
        Assert.Equal(sideLength, cubeDBO.SideLength);
    }

    [Fact]
    public void Constructor_ShouldCreateInstanceWithDefaultValues()
    {
        // Arrange & Act
        var cubeDBO = new CubeDBO();

        // Assert
        Assert.NotNull(cubeDBO);
        Assert.Equal(Guid.Empty, cubeDBO.Id);
        Assert.Equal(0, cubeDBO.SideLength);
    }

    [Fact]
    public void Properties_ShouldBeMutable()
    {
        // Arrange
        var cubeDBO = new CubeDBO
        {
            Id = Guid.NewGuid(),
            SideLength = 10
        };

        // Act
        var newId = Guid.NewGuid();
        cubeDBO.Id = newId;
        cubeDBO.SideLength = 20;

        // Assert
        Assert.Equal(newId, cubeDBO.Id);
        Assert.Equal(20, cubeDBO.SideLength);
    }
}
