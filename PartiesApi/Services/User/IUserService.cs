using PartiesApi.DTO;
using PartiesApi.DTO.User;

namespace PartiesApi.Services.User;

public interface IUserService
{
    Task<bool> CheckUserExistenceAsync(string userLogin);
    Task<bool> CheckUserPasswordAsync(string userLogin, string password);
    Task<UserResponse?> CreateUserAsync(string login, string password);
    Task<UserResponse?> GetUserOrDefaultAsync(Guid userId);
    Task<UserResponse?> GetUserOrDefaultAsync(string userLogin);
    Task<MethodResult<IEnumerable<UserResponse>>> FindUsersAsync(string userLogin);
}