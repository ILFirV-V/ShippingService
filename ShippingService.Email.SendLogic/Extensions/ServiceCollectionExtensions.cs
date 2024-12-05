using Microsoft.Extensions.DependencyInjection;
using ShippingService.Email.SendLogic.Interfaces.Managers;
using ShippingService.Email.SendLogic.Interfaces.Services;
using ShippingService.Email.SendLogic.Managers;
using ShippingService.Email.SendLogic.Services;

namespace ShippingService.Email.SendLogic.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWhatsAppLogic(this IServiceCollection services)
    {
        services.AddScoped<ISendService, SendService>();
        services.AddScoped<ISendManager, SendManager>();

        return services;
    }
}