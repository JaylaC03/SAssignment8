using Geometry.Domain.CylinderModel;

namespace Geometry.Domain.Tests;

/// <summary>
/// Tests for Cylinder domain model.
/// </summary>
public class CylinderTests
{
    /// <summary>
    /// Tests that a cylinder can be created with valid radius and height.
    /// </summary>
    [Fact]
    public void Cylinder_ShouldBeCreated_WithValidRadiusAndHeight()
    {
        // Arrange
        var id = Guid.NewGuid();
        var radius = 5.0;
        var height = 10.0;

        // Act
        var cylinder = new Cylinder(id, radius, height);

        // Assert
        Assert.Equal(id, cylinder.Id);
        Assert.Equal(radius, cylinder.Radius);
        Assert.Equal(height, cylinder.Height);
    }

    /// <summary>
    /// Tests that creating a cylinder with zero radius throws ArgumentException.
    /// </summary>
    [Fact]
    public void Cylinder_ShouldThrowArgumentException_WhenRadiusIsZero()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Cylinder(id, 0, 10.0));
    }

    /// <summary>
    /// Tests that creating a cylinder with negative radius throws ArgumentException.
    /// </summary>
    [Fact]
    public void Cylinder_ShouldThrowArgumentException_WhenRadiusIsNegative()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Cylinder(id, -5.0, 10.0));
    }

    /// <summary>
    /// Tests that creating a cylinder with zero height throws ArgumentException.
    /// </summary>
    [Fact]
    public void Cylinder_ShouldThrowArgumentException_WhenHeightIsZero()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Cylinder(id, 5.0, 0));
    }

    /// <summary>
    /// Tests that creating a cylinder with negative height throws ArgumentException.
    /// </summary>
    [Fact]
    public void Cylinder_ShouldThrowArgumentException_WhenHeightIsNegative()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Cylinder(id, 5.0, -10.0));
    }

    /// <summary>
    /// Tests that setting radius to a valid value updates the property.
    /// </summary>
    [Fact]
    public void Cylinder_ShouldUpdateRadius_WhenValidValueIsSet()
    {
        // Arrange
        var cylinder = new Cylinder(Guid.NewGuid(), 5.0, 10.0);

        // Act
        cylinder.Radius = 7.5;

        // Assert
        Assert.Equal(7.5, cylinder.Radius);
    }

    /// <summary>
    /// Tests that setting height to a valid value updates the property.
    /// </summary>
    [Fact]
    public void Cylinder_ShouldUpdateHeight_WhenValidValueIsSet()
    {
        // Arrange
        var cylinder = new Cylinder(Guid.NewGuid(), 5.0, 10.0);

        // Act
        cylinder.Height = 15.0;

        // Assert
        Assert.Equal(15.0, cylinder.Height);
    }

    /// <summary>
    /// Tests that setting radius to zero throws ArgumentException.
    /// </summary>
    [Fact]
    public void Cylinder_ShouldThrowArgumentException_WhenSettingRadiusToZero()
    {
        // Arrange
        var cylinder = new Cylinder(Guid.NewGuid(), 5.0, 10.0);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => cylinder.Radius = 0);
    }

    /// <summary>
    /// Tests that setting height to zero throws ArgumentException.
    /// </summary>
    [Fact]
    public void Cylinder_ShouldThrowArgumentException_WhenSettingHeightToZero()
    {
        // Arrange
        var cylinder = new Cylinder(Guid.NewGuid(), 5.0, 10.0);

        // Act & Assert
        Assert.Throws<ArgumentException>(() => cylinder.Height = 0);
    }
}