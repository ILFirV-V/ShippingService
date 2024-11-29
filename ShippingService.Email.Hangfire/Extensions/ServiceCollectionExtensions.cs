using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShippingService.Email.Hangfire.Configs;

namespace ShippingService.Email.Hangfire.Extensions;

public static class ServiceCollectionExtensions
{
    private const string DefaultHangfireConnectionString = "HangfireConnection";
    
    public static void AddHangfire(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(DefaultHangfireConnectionString) ??
                               throw new InvalidOperationException("Cannot to get connection string");
        services.AddSingleton(new HangfireDatabaseOptions(connectionString));
        
        services.AddHangfire(config =>
        {
            config.UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(options =>
                {
                    options.UseNpgsqlConnection(connectionString);
                });
        });
        services.AddHangfireServer();
    }
}