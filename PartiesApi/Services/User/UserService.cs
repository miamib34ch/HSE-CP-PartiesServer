using PartiesApi.Repositories.User;
using PartiesApi.Utils;

namespace PartiesApi.Services.User;

internal class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<bool> CheckUserExistenceAsync(string userLogin)
    {
        var user = await userRepository.GetUserOrDefaultAsync(userLogin);

        return user != null;
    }

    public async Task<bool> CheckUserPasswordAsync(string userLogin, string password)
    {
        var user = await userRepository.GetUserOrDefaultAsync(userLogin);
        var passwordHash = StringHasher.GetSha256Hash(password);

        return user != null && passwordHash == user.PasswordHash;
    }

    public async Task<Models.User?> CreateUserAsync(string login, string password)
    {
        var passwordHash = StringHasher.GetSha256Hash(password);

        var user = new Models.User()
        {
            Login = login,
            PasswordHash = passwordHash
        };

        var createdUser = await userRepository.AddUserAsync(user);

        return createdUser;
    }

    public async Task<Models.User?> GetUserOrDefaultAsync(Guid userId)
    {
        var user = await userRepository.GetUserOrDefaultAsync(userId);

        return user;
    }

    public async Task<Models.User?> GetUserOrDefaultAsync(string userLogin)
    {
        var user = await userRepository.GetUserOrDefaultAsync(userLogin);

        return user;
    }
}