namespace EventHorizon.Cache
{
    public interface CacheKey
    {
        string CacheKeyPrefix { get; }
        string CacheKey { get; }
    }
}
