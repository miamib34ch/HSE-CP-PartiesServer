
namespace PartiesApi.Services.JWT;

internal interface IJwtService
{
    string GenerateToken(string login);
}