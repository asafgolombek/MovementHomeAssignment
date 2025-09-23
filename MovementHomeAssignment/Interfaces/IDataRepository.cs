using MovementHomeAssignment.DTOs;

namespace MovementHomeAssignment.Interfaces;

/// <summary>
/// Defines the contract for the data repository, abstracting database operations.
/// </summary>
public interface IDataRepository
{
    Task<Data?> GetByIdAsync(string id);
    Task<Data> CreateAsync(Data data);
    Task<Data?> UpdateAsync(Data data);
}