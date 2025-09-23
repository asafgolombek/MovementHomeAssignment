using MovementHomeAssignment.DTOs;

namespace MovementHomeAssignment.Interfaces;

/// <summary>
/// A generic interface for a data source (Cache, File, DB).
/// </summary>
public interface IDataSource
{
    Task<Data?> GetAsync(string id);
    Task SetAsync(Data data);
}
