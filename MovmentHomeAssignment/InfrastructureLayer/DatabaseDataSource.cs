using MovementHomeAssignment.DTOs;
using MovementHomeAssignment.Interfaces;

namespace MovementHomeAssignment.InfrastructureLayer;

public class DatabaseDataSource : IDataSource
{
    private readonly IDataRepository _repository;

    public DatabaseDataSource(IDataRepository repository)
    {
        _repository = repository;
    }

    public Task<Data?> GetAsync(string id) => _repository.GetByIdAsync(id);
    
    // SetAsync is a write operation, which goes directly through the repository in the service layer,
    // so this implementation is not strictly needed for the read pipeline.
    public Task SetAsync(Data data) => Task.CompletedTask;
}
