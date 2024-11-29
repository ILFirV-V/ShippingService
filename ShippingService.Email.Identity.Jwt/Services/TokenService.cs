using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShippingService.Email.Core.Common;
using ShippingService.Email.Domain.Entities.Identity;
using ShippingService.Email.Identity.Jwt.Constants;
using ShippingService.Email.Identity.Jwt.Interfaces;
using ShippingService.Email.Identity.Jwt.Models.Settings;

namespace ShippingService.Email.Identity.Jwt.Services;

internal sealed class TokenService : ITokenService
{
    private readonly JwtTokenSettings jwtSettings;

    public TokenService(IOptions<JwtTokenSettings> jwtSettings)
    {
        this.jwtSettings = jwtSettings.Value;
    }
    
    public OperationResult<string> CreateToken(ApplicationUser user)
    {
        var expiration = DateTime.UtcNow.AddMinutes(TokenServiceConstants.ExpirationMinutes);
        var claimsResult = CreateClaims(user);
        if (claimsResult.IsFail)
        {
            return claimsResult.Error!;
        }
        var claims = claimsResult.Result!;
        
        var token = CreateJwtToken(
            claims,
            CreateSigningCredentials(),
            expiration
        );
        var tokenHandler = new JwtSecurityTokenHandler();
        
        return tokenHandler.WriteToken(token);
    }

    private JwtSecurityToken CreateJwtToken(IEnumerable<Claim> claims, SigningCredentials credentials,
        DateTime expiration) =>
        new(
            jwtSettings.ValidIssuer,
            jwtSettings.ValidAudience,
            claims,
            expires: expiration,
            signingCredentials: credentials
        );

    private OperationResult<IList<Claim>> CreateClaims(ApplicationUser user)
    {
        try
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, jwtSettings.JwtRegisteredClaimNamesSub),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString()),
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Email, user.Email),
            };
            
            return claims;
        }
        catch (Exception e)
        {
            return Error.Internal(e.Message);
        }
    }

    private SigningCredentials CreateSigningCredentials()
    {
        return new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.SymmetricSecurityKey)
            ),
            SecurityAlgorithms.HmacSha256
        );
    }
}