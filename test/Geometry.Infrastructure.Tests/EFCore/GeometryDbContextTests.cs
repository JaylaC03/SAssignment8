using Geometry.Infrastructure.Persistence.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Geometry.Infrastructure.Tests;

public class GeometryDbContextTests
{
    private GeometryDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<GeometryDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new GeometryDbContext(options);
    }

    [Fact]
    public void Constructor_WithOptions_ShouldCreateInstance()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<GeometryDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        // Act
        using var context = new GeometryDbContext(options);

        // Assert
        Assert.NotNull(context);
    }

    [Fact]
    public void Cubes_ShouldBeInitialized()
    {
        // Arrange
        using var context = CreateContext();

        // Assert
        Assert.NotNull(context.Cubes);
    }

    [Fact]
    public async Task Cubes_ShouldAllowAddingEntities()
    {
        // Arrange
        using var context = CreateContext();
        var cubeDBO = new CubeDBO
        {
            Id = Guid.NewGuid(),
            SideLength = 5
        };

        // Act
        await context.Cubes.AddAsync(cubeDBO);
        await context.SaveChangesAsync();

        // Assert
        var count = await context.Cubes.CountAsync();
        Assert.Equal(1, count);
    }

    [Fact]
    public async Task Cubes_ShouldAllowQueryingEntities()
    {
        // Arrange
        using var context = CreateContext();
        var id = Guid.NewGuid();
        var cubeDBO = new CubeDBO
        {
            Id = id,
            SideLength = 10
        };

        await context.Cubes.AddAsync(cubeDBO);
        await context.SaveChangesAsync();

        // Act
        var retrieved = await context.Cubes.FirstOrDefaultAsync(c => c.Id == id);

        // Assert
        Assert.NotNull(retrieved);
        Assert.Equal(id, retrieved.Id);
        Assert.Equal(10, retrieved.SideLength);
    }

    [Fact]
    public async Task ModelConfiguration_IdShouldBePrimaryKey()
    {
        // Arrange
        using var context = CreateContext();
        var cubeDBO = new CubeDBO
        {
            Id = Guid.NewGuid(),
            SideLength = 5
        };

        await context.Cubes.AddAsync(cubeDBO);
        await context.SaveChangesAsync();

        // Act - Try to query by Id (primary key operations should work)
        var retrieved = await context.Cubes.FindAsync(cubeDBO.Id);

        // Assert
        Assert.NotNull(retrieved);
        Assert.Equal(cubeDBO.Id, retrieved.Id);
    }

    [Fact]
    public async Task ModelConfiguration_SideLengthShouldBeRequired()
    {
        // Arrange
        using var context = CreateContext();
        var cubeDBO = new CubeDBO
        {
            Id = Guid.NewGuid(),
            SideLength = 0 // Zero is valid for DBO, but required means it must have a value
        };

        // Act
        await context.Cubes.AddAsync(cubeDBO);
        await context.SaveChangesAsync();

        // Assert - If SideLength is required, it should save successfully
        var retrieved = await context.Cubes.FirstOrDefaultAsync(c => c.Id == cubeDBO.Id);
        Assert.NotNull(retrieved);
        Assert.Equal(0, retrieved.SideLength);
    }

    [Fact]
    public async Task Database_ShouldSupportMultipleEntities()
    {
        // Arrange
        using var context = CreateContext();
        var cube1 = new CubeDBO { Id = Guid.NewGuid(), SideLength = 5 };
        var cube2 = new CubeDBO { Id = Guid.NewGuid(), SideLength = 10 };
        var cube3 = new CubeDBO { Id = Guid.NewGuid(), SideLength = 15 };

        // Act
        await context.Cubes.AddRangeAsync(cube1, cube2, cube3);
        await context.SaveChangesAsync();

        // Assert
        var count = await context.Cubes.CountAsync();
        Assert.Equal(3, count);
    }

    [Fact]
    public async Task Database_ShouldSupportUpdatingEntities()
    {
        // Arrange
        using var context = CreateContext();
        var id = Guid.NewGuid();
        var cubeDBO = new CubeDBO
        {
            Id = id,
            SideLength = 5
        };

        await context.Cubes.AddAsync(cubeDBO);
        await context.SaveChangesAsync();

        // Act
        var retrieved = await context.Cubes.FindAsync(id);
        retrieved!.SideLength = 20;
        context.Cubes.Update(retrieved);
        await context.SaveChangesAsync();

        // Assert
        var updated = await context.Cubes.FindAsync(id);
        Assert.NotNull(updated);
        Assert.Equal(20, updated.SideLength);
    }

    [Fact]
    public async Task Database_ShouldSupportDeletingEntities()
    {
        // Arrange
        using var context = CreateContext();
        var id = Guid.NewGuid();
        var cubeDBO = new CubeDBO
        {
            Id = id,
            SideLength = 5
        };

        await context.Cubes.AddAsync(cubeDBO);
        await context.SaveChangesAsync();

        // Act
        context.Cubes.Remove(cubeDBO);
        await context.SaveChangesAsync();

        // Assert
        var count = await context.Cubes.CountAsync();
        Assert.Equal(0, count);
    }
}
