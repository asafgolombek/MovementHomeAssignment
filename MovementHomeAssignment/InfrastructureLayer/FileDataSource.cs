using System.Text.Json;
using MovementHomeAssignment.DTOs;
using MovementHomeAssignment.Interfaces;

namespace MovementHomeAssignment.InfrastructureLayer;

public class FileDataSource : IDataSource
{
    private readonly string _storagePath = Path.Combine(AppContext.BaseDirectory, "file_storage");
    private readonly ILogger<FileDataSource> _logger;

    public FileDataSource(ILogger<FileDataSource> logger)
    {
        _logger = logger;
        Directory.CreateDirectory(_storagePath);
    }

    public async Task<Data?> GetAsync(string id)
    {
        // Clean up expired files first
        CleanupExpiredFiles();

        var file = Directory.GetFiles(_storagePath, $"data_{id}_*.json").FirstOrDefault();
        if (file == null) return null;

        var content = await File.ReadAllTextAsync(file);
        return JsonSerializer.Deserialize<Data>(content);
    }

    public async Task SetAsync(Data data)
    {
        // Clean up any old files for this ID before creating a new one
        var oldFiles = Directory.GetFiles(_storagePath, $"data_{data.Id}_*.json");
        foreach(var oldFile in oldFiles)
        {
            File.Delete(oldFile);
        }
        
        var expiration = DateTimeOffset.UtcNow.AddMinutes(30).ToUnixTimeSeconds();
        var filePath = Path.Combine(_storagePath, $"data_{data.Id}_{expiration}.json");
        await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(data));
        _logger.LogInformation("Set data with ID {Id} in file storage.", data.Id);
    }

    private void CleanupExpiredFiles()
    {
        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        var files = Directory.GetFiles(_storagePath, "*.json");

        foreach (var file in files)
        {
            try
            {
                var parts = Path.GetFileNameWithoutExtension(file).Split('_');
                if (parts.Length == 3 && long.TryParse(parts[2], out var expiration))
                {
                    if (now > expiration)
                    {
                        File.Delete(file);
                        _logger.LogInformation("Deleted expired file: {FileName}", Path.GetFileName(file));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Could not process file for expiration cleanup: {FileName}", Path.GetFileName(file));
            }
        }
    }
}