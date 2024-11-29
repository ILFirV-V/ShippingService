namespace ShippingService.Email.Core.Configuration;

public interface IConfigurationSettings
{
    public static string Section { get; } = "";

    public bool IsWhole();
}
