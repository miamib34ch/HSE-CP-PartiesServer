
namespace PartiesApi.Services.JWT;

public interface IJwtService
{
    string GenerateToken(string login);
}