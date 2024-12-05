using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShippingService.Email.Controllers.Base;
using ShippingService.Email.Core.Common;
using ShippingService.Email.Extensions;
using ShippingService.Email.Identity.Jwt.Interfaces;
using ShippingService.Email.Identity.Jwt.Models.Request;
using ShippingService.Email.Identity.Jwt.Models.Response;

namespace ShippingService.Email.Controllers;

public class UsersController : ApiControllerBase
{
    private readonly IAuthService authService;

    public UsersController(IAuthService authService)
    {
        this.authService = authService;
    }
    
    [AllowAnonymous]
    [HttpPost(Routes.Routes.Login)]
    [ProducesDefaultResponseType(typeof(OperationResult))]
    [ProducesResponseType<OperationResult>(StatusCodes.Status200OK)]
    [ProducesResponseType<OperationResult>(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
    {
        var authResponseResult = await authService.AuthenticateAsync(request);
        return authResponseResult.ToActionResult();
    }
}