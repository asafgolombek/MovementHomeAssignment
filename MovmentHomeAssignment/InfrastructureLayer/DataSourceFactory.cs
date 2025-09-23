using MovementHomeAssignment.DTOs;
using MovementHomeAssignment.Interfaces;

namespace MovementHomeAssignment.InfrastructureLayer;

public class DataSourceFactory : IDataSourceFactory
{
    private readonly IServiceProvider _serviceProvider;

    public DataSourceFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IDataSource Create(DataSourceType type)
    {
        return type switch
        {
            DataSourceType.Cache => _serviceProvider.GetRequiredService<CacheDataSource>(),
            DataSourceType.File => _serviceProvider.GetRequiredService<FileDataSource>(),
            DataSourceType.Database => _serviceProvider.GetRequiredService<DatabaseDataSource>(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), "Unsupported data source type.")
        };
    }
}
