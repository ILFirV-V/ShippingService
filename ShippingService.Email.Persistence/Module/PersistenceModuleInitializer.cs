using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShippingService.Email.Core.Modules;
using ShippingService.Email.Persistence.Context;

namespace ShippingService.Email.Persistence.Module;

public class PersistenceModuleInitializer : IModuleInitializer
{
    public void WarnUp(IServiceProvider serviceProvider)
    {
        var dataContext = serviceProvider.GetRequiredService<DataContext>();
        dataContext.Database.Migrate();
    }
}