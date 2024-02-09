using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PartiesApi.Services.JWT;

namespace PartiesApi.Utils;

public class UserIdReader(IOptionsMonitor<JwtConfig> jwtConfig)
{
    public Guid GetUserIdFromAuth(HttpContext context)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        if (jwtConfig.CurrentValue.Secret == null)
            return Guid.Empty;

        var key = Encoding.ASCII.GetBytes(jwtConfig.CurrentValue.Secret);
        var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);

        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        }, out var validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;
        var userId = Guid.Parse(jwtToken.Claims.First().Value);

        return userId;
    }
}