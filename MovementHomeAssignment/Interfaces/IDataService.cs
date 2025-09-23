using MovementHomeAssignment.DTOs;

namespace MovementHomeAssignment.Interfaces;

/// <summary>
/// Defines the contract for the main data service.
/// This is the primary entry point for the application logic.
/// </summary>
public interface IDataService
{
    Task<DataDto?> GetDataByIdAsync(string id);
    Task<DataDto> CreateDataAsync(CreateDataDto createDataDto);
    Task<DataDto?> UpdateDataAsync(string id, UpdateDataDto updateDataDto);
}
