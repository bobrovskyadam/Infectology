using Infectology.Database.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infectology.WebAPI.Builders;

public static class DatabaseBuilder
{
    public static IServiceCollection AddDatabaseContexts(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<UserContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(nameof(UserContext)));
        });

        services.AddDbContext<ContentContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString(nameof(ContentContext)));
        });

        return services;
    }
}