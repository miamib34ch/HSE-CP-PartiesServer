using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace PartiesApi.Services.JWT;

public class JwtService(IOptionsMonitor<JwtConfig> jwtConfig) : IJwtService
{
    public string GenerateToken(string login)
    {
        var claims = new List<Claim> { new(ClaimTypes.Name, login) };

        if (jwtConfig.CurrentValue.Secret == null)
            return string.Empty;

        var key = Encoding.ASCII.GetBytes(jwtConfig.CurrentValue.Secret);

        var jwt = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(20)),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        return encodedJwt;
    }
}