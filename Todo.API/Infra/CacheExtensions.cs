namespace Todo.API.Infra;

public static class CacheExtensions
{
    public static void InjectCache(
        this IServiceCollection services,
        string connectionString
    ) => services.AddStackExchangeRedisCache(e =>
    {
        e.Configuration = connectionString;
    });
}