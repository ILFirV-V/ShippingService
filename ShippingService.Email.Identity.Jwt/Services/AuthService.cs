using Microsoft.AspNetCore.Identity;
using ShippingService.Email.Core.Common;
using ShippingService.Email.Domain.Entities.Identity;
using ShippingService.Email.Identity.Jwt.Interfaces;
using ShippingService.Email.Identity.Jwt.Models.Request;
using ShippingService.Email.Identity.Jwt.Models.Response;

namespace ShippingService.Email.Identity.Jwt.Services;

internal sealed class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ITokenService tokenService;

    public AuthService(UserManager<ApplicationUser> userManager, ITokenService tokenService)
    {
        this.userManager = userManager;
        this.tokenService = tokenService;
    }

    public async Task<OperationResult<AuthResponse>> AuthenticateAsync(AuthRequest request)
    {
        var managedUser = await userManager.FindByNameAsync(request.Email!);
        if (managedUser == null || !await userManager.CheckPasswordAsync(managedUser, request.Password!))
        {
            return Error.Unauthorized("Bad credentials");
        }

        var accessTokenResult = tokenService.CreateToken(managedUser);
        if (accessTokenResult.IsFail)
        {
            return accessTokenResult.Error!;
        }

        return new AuthResponse
        {
            Username = managedUser.UserName ?? "Anonymous",
            Email = managedUser.Email ?? "Anonymous",
            Token = accessTokenResult.Result!,
        };
    }
}