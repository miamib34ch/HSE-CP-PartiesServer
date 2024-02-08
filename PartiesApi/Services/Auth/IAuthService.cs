using PartiesApi.DTO.Auth;

namespace PartiesApi.Services.Auth;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(string login, string password);
    Task<AuthResult> LoginAsync(string login, string password);
}