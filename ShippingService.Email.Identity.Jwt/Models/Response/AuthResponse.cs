namespace ShippingService.Email.Identity.Jwt.Models.Response;

public record AuthResponse
{
    public string Username { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Token { get; init; } = string.Empty;
}