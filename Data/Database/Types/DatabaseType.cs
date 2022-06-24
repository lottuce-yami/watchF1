namespace F1Project.Data.Database.Types;

/// <summary>
/// An abstraction for a type
/// that can be fully serialized into a database.
/// </summary>
public abstract class DatabaseType
{
    /// <value>Property <c>Id</c> represents an unique string identifier of an object.</value>
    public abstract string Id { get; init; }
}