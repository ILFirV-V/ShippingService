namespace ShippingService.Email.Core.Modules;

public interface IModuleInitializer
{
    public void WarnUp(IServiceProvider serviceProvider);
}