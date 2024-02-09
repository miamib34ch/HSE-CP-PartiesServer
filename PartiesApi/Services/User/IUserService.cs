namespace PartiesApi.Services.User;

internal interface IUserService
{
    Task<bool> CheckUserExistenceAsync(string userLogin);
    Task<bool> CheckUserPasswordAsync(string userLogin, string password);
    Task<bool> CreateUserAsync(string login, string password);
    Task<Models.User?> GetUserOrDefaultAsync(Guid userId);
}