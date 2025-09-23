using MovementHomeAssignment.DTOs;

namespace MovementHomeAssignment.Interfaces;
/// <summary>
/// Defines the contract for the factory that creates data source instances.
/// </summary>
public interface IDataSourceFactory
{
    IDataSource Create(DataSourceType type);
}

