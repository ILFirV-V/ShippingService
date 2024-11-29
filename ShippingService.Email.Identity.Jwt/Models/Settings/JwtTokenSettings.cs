namespace ShippingService.Email.Identity.Jwt.Models.Settings;

public class JwtTokenSettings
{
    public string ValidIssuer { get; init; } = null!; 
    public string ValidAudience { get; init; } = null!;
    public string SymmetricSecurityKey { get; init; } = null!;
    public string JwtRegisteredClaimNamesSub { get; init; } = null!;
}