using PartiesApi.DTO.User;

namespace PartiesApi.Services.User;

internal interface IUserService
{
    Task<bool> CheckUserExistenceAsync(string userLogin);
    Task<bool> CheckUserPasswordAsync(string userLogin, string password);
    Task<UserResponse?> CreateUserAsync(string login, string password);
    Task<UserResponse?> GetUserOrDefaultAsync(Guid userId);
    Task<UserResponse?> GetUserOrDefaultAsync(string userLogin);
}