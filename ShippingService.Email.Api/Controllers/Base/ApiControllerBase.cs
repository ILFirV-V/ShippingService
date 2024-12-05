using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShippingService.Email.Controllers.Base;

[ApiController]
[Authorize]
public class ApiControllerBase : ControllerBase
{
}