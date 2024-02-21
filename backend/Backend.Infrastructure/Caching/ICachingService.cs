namespace Backend.Infrastructure.Caching;

public interface ICachingService
{
    Task<string> GetAsync(string key);
    Task SetAsync(string key, string value);
}