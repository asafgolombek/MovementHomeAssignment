using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using MovementHomeAssignment.DTOs;
using MovementHomeAssignment.Interfaces;

namespace MovementHomeAssignment.InfrastructureLayer;


public class CacheDataSource : IDataSource
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<CacheDataSource> _logger;

    public CacheDataSource(IDistributedCache cache, ILogger<CacheDataSource> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task<Data?> GetAsync(string id)
    {
        var cachedData = await _cache.GetStringAsync(id);
        return cachedData == null ? null : JsonSerializer.Deserialize<Data>(cachedData);
    }

    public async Task SetAsync(Data data)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        };
        await _cache.SetStringAsync(data.Id, JsonSerializer.Serialize(data), options);
        _logger.LogInformation("Set data with ID {Id} in cache.", data.Id);
    }
}



