using ShippingService.Email.Core.Common;
using ShippingService.Email.Domain.Entities.Identity;

namespace ShippingService.Email.Identity.Jwt.Interfaces;

public interface ITokenService
{
    public OperationResult<string> CreateToken(ApplicationUser user);
}