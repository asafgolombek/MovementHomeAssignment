namespace MovementHomeAssignment.DTOs;

/// <summary>
/// Represents the core data entity in the domain.
/// </summary>
public class Data
{
    /// <summary>
    /// The unique identifier for the data item.
    /// It's a string to allow for GUIDs or other flexible key formats.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The actual content of the data item.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// The timestamp of when the data was last modified or created.
    /// </summary>
    public DateTimeOffset LastModified { get; set; } = DateTimeOffset.UtcNow;
}
