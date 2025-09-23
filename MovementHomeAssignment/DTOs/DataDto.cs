namespace MovementHomeAssignment.DTOs;

/// <summary>
/// DTO for returning data to the client.
/// </summary>
public class DataDto
{
    public string Id { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}

/// <summary>
/// DTO for creating a new data item.
/// </summary>
public class CreateDataDto
{
    public string Content { get; set; } = string.Empty;
}

/// <summary>
/// DTO for updating an existing data item.
/// </summary>
public class UpdateDataDto
{
    public string Content { get; set; } = string.Empty;
}
