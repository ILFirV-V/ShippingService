using ShippingService.Email.Core.Common;
using ShippingService.Email.Identity.Jwt.Models.Request;
using ShippingService.Email.Identity.Jwt.Models.Response;

namespace ShippingService.Email.Identity.Jwt.Interfaces;

public interface IAuthService
{
    public Task<OperationResult<AuthResponse>> AuthenticateAsync(AuthRequest request);
}