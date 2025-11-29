namespace Geometry.Domain.CubeModel;

/// <summary>
/// Represents a cube geometric entity with a specified side length.
/// </summary>
public class Cube : Entity
{
    private int _sideLength;

    /// <summary>
    /// The length of one side of the cube
    /// </summary>
    public int SideLength
    {
        get => _sideLength;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("SideLength must be greater than 0.", nameof(value));
            }
            _sideLength = value;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Cube"/> class.
    /// </summary>
    /// <param name="id">The unique identifier for the cube.</param>
    /// <param name="sideLength">The length of one side of the cube. Must be greater than 0.</param>
    /// <exception cref="ArgumentException">Thrown when sideLength is less than or equal to 0.</exception>
    public Cube(Guid id, int sideLength) : base(id)
    {
        SideLength = sideLength;
    }
}
