using PartiesApi.DTO.Auth;
using PartiesApi.Exceptions;
using PartiesApi.Services.JWT;
using PartiesApi.Services.User;

namespace PartiesApi.Services.Auth;

internal class AuthService(IUserService userService, IJwtService jwtService) : IAuthService
{
    public async Task<AuthResult> RegisterAsync(string login, string password)
    {
        var isUserExist = await userService.CheckUserExistenceAsync(login);
        if (isUserExist)
            return new AuthResult()
            {
                IsSuccess = false,
                Error = "User with this login is already registered"
            };

        var token = jwtService.GenerateToken(login);

        var userCreated = await userService.CreateUserAsync(login, password);

        if (!userCreated)
            throw new UserNotCreatedException();

        var successAuthResult = new AuthResult()
        {
            IsSuccess = true,
            AccessToken = token
        };

        return successAuthResult;
    }

    public async Task<AuthResult> LoginAsync(string login, string password)
    {
        var isUserExist = await userService.CheckUserExistenceAsync(login);
        if (!isUserExist)
            return new AuthResult()
            {
                IsSuccess = false,
                Error = "User with this login is not found. Please register first"
            };

        var isPasswordValid = await userService.CheckUserPasswordAsync(login, password);
        if (!isPasswordValid)
            return new AuthResult()
            {
                IsSuccess = false,
                Error = "Login or password is incorrect"
            };

        var token = jwtService.GenerateToken(login);

        var successAuthResult = new AuthResult()
        {
            IsSuccess = true,
            AccessToken = token
        };

        return successAuthResult;
    }
}