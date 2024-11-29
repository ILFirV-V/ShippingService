using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShippingService.Email.Persistence.Configs;
using ShippingService.Email.Persistence.Context;

namespace ShippingService.Email.Persistence.Module;

public static class ServiceCollectionExtensions
{
    private const string DefaultSchema = "Email";
    private const string DefaultConnectionString = "DefaultConnection";

    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(DefaultConnectionString) ??
                               throw new InvalidOperationException("Cannot to get connection string");
        services.AddSingleton(new DatabaseOptions(connectionString, DefaultSchema));
        services.AddDbContext<DataContext>(opt => opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        
        return services;
    }
}