using System.Text.Json;
using Core.Interfaces;
using StackExchange.Redis;

namespace Infrastructure.Services;

public class ResponseCacheService : IResponseCacheService
{
    private readonly IDatabase _database;

    public ResponseCacheService(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task CacheResponseAsync(string cacheKey, object? response, TimeSpan timeToLive)
    {
        if (response == null)
        {
            return;
        }

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        var serializeResponse = JsonSerializer.Serialize(response, options);

        await _database.StringSetAsync(cacheKey, serializeResponse, timeToLive);
    }

    public async Task<string?> GetCachedResponseAsync(string cacheKey)
    {
        var cacheResponse = await _database.StringGetAsync(cacheKey);
        if (cacheResponse.IsNullOrEmpty)
        {
            return null;
        }
        return cacheResponse;
    }
}
