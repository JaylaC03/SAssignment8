/// Entities are any root elements that can be uniquely identified.
public abstract class Entity
{
    /// <summary>
    /// The Id of the Entity. Anything can read it, but only the entity can
    /// modify it.
    /// </summary>
    public Guid Id { get; protected set; }

    /// <summary>
    /// Default constructor for the entity.
    /// </summary>
    /// <param name="id">the Guid to set for the new id.</param>
    public Entity(Guid id)
    {
        Id = id;
    }
}
