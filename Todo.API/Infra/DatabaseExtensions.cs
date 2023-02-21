using Microsoft.EntityFrameworkCore;

namespace Todo.API.Infra;

public static class DatabaseExtensions
{
    public static void InjectDatabase(
        this IServiceCollection services,
        string connectionString
    ) => services.AddDbContext<DatabaseContext>(e => e.UseNpgsql(connectionString));

    public static void ConfigureDatabase(this WebApplication app)
    {
        using var serviceScope = app.Services
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();
        
        var context = serviceScope.ServiceProvider.GetService<DatabaseContext>();
        context.Database.EnsureCreated();
    }
}