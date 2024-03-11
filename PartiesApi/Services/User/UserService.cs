using AutoMapper;
using PartiesApi.DTO;
using PartiesApi.DTO.User;
using PartiesApi.Repositories.User;
using PartiesApi.Utils;

namespace PartiesApi.Services.User;

internal class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
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

    public async Task<UserResponse?> CreateUserAsync(string login, string password)
    {
        var passwordHash = StringHasher.GetSha256Hash(password);

        var user = new Models.User()
        {
            Login = login,
            PasswordHash = passwordHash
        };

        var createdUser = await userRepository.AddUserAsync(user);

        if (createdUser == null)
            return null;
            
        var userResponse = mapper.Map<Models.User, UserResponse>(createdUser);

        return userResponse;
    }

    public async Task<UserResponse?> GetUserOrDefaultAsync(Guid userId)
    {
        var user = await userRepository.GetUserOrDefaultAsync(userId);

        if (user == null)
            return null;
            
        var userResponse = mapper.Map<Models.User, UserResponse>(user);

        return userResponse;
    }

    public async Task<UserResponse?> GetUserOrDefaultAsync(string userLogin)
    {
        var user = await userRepository.GetUserOrDefaultAsync(userLogin);

        if (user == null)
            return null;
            
        var userResponse = mapper.Map<Models.User, UserResponse>(user);

        return userResponse;
    }

    public async Task<MethodResult<IEnumerable<UserResponse>>> FindUsersAsync(string userLogin)
    {
        const string methodName = "FindUsers";

        try
        {
            var users = await userRepository.FindUsersAsync(userLogin);

            var userRequests =
                users.Select(mapper.Map<Models.User, UserResponse>).ToList();
            
            return new MethodResult<IEnumerable<UserResponse>>(methodName, true,
                string.Empty, userRequests);
        }
        catch (Exception ex)
        {
            return new MethodResult<IEnumerable<UserResponse>>(methodName, false, $"Unknown error");
        }
    }
}