namespace Infectology.WebAPI.Builders;

public static class CorsBuilder
{
    public const string FrontendPolicy = "FrontendPolicy";

    public static IServiceCollection AddFrontendCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(FrontendPolicy, policy =>
            {
                var origins = configuration.GetSection("Cors").Get<string[]>() ?? [];
                policy
                    .WithOrigins(origins)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }
}