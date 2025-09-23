using MovementHomeAssignment.DTOs;
using MovementHomeAssignment.Interfaces;
using AutoMapper;

namespace MovementHomeAssignment.Services;

/// <summary>
/// Orchestrates the data retrieval and manipulation logic.
/// </summary>
public class DataService : IDataService
{
    private readonly IDataSourceFactory _dataSourceFactory;
    private readonly IDataRepository _repository; // For write operations
    private readonly IMapper _mapper;
    private readonly ILogger<DataService> _logger;

    public DataService(IDataSourceFactory dataSourceFactory, IDataRepository repository, IMapper mapper, ILogger<DataService> logger)
    {
        _dataSourceFactory = dataSourceFactory;
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<DataDto?> GetDataByIdAsync(string id)
    {
        var cacheSource = _dataSourceFactory.Create(DataSourceType.Cache);
        var fileSource = _dataSourceFactory.Create(DataSourceType.File);
        var dbSource = _dataSourceFactory.Create(DataSourceType.Database);

        var data = await cacheSource.GetAsync(id);
        if (data != null)
        {
            _logger.LogInformation("Cache HIT for ID: {Id}", id);
            return _mapper.Map<DataDto>(data);
        }
        _logger.LogInformation("Cache MISS for ID: {Id}", id);

        data = await fileSource.GetAsync(id);
        if (data != null)
        {
            _logger.LogInformation("File HIT for ID: {Id}", id);
            await cacheSource.SetAsync(data);
            return _mapper.Map<DataDto>(data);
        }
        _logger.LogInformation("File MISS for ID: {Id}", id);

        data = await dbSource.GetAsync(id);
        if (data != null)
        {
            _logger.LogInformation("Database HIT for ID: {Id}", id);
            await fileSource.SetAsync(data); 
            await cacheSource.SetAsync(data);
            return _mapper.Map<DataDto>(data);
        }
        _logger.LogInformation("Database MISS for ID: {Id}. Entity not found.", id);

        return null;
    }

    public async Task<DataDto> CreateDataAsync(CreateDataDto createDataDto)
    {
        var data = _mapper.Map<Data>(createDataDto);
        var newData = await _repository.CreateAsync(data);
        return _mapper.Map<DataDto>(newData);
    }

    public async Task<DataDto?> UpdateDataAsync(string id, UpdateDataDto updateDataDto)
    {
        var dataToUpdate = await _repository.GetByIdAsync(id);
        if (dataToUpdate == null)
        {
            return null;
        }

        _mapper.Map(updateDataDto, dataToUpdate);
        var updatedData = await _repository.UpdateAsync(dataToUpdate);
        
        // Invalidate cache and file after an update
        var cacheSource = _dataSourceFactory.Create(DataSourceType.Cache);
        var fileSource = _dataSourceFactory.Create(DataSourceType.File);
        if(updatedData != null) {
            await cacheSource.SetAsync(updatedData); // Re-set cache with new value
            await fileSource.SetAsync(updatedData); // Re-set file with new value
        }


        return _mapper.Map<DataDto>(updatedData);
    }
}
