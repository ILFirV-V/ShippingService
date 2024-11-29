namespace ShippingService.Email.Identity.Jwt.Models.Request;

public record AuthRequest
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}