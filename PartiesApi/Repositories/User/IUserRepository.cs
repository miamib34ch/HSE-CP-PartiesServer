namespace PartiesApi.Repositories.User;

internal interface IUserRepository
{
    Task<Models.User?> GetUserOrDefaultAsync(string userLogin);
    Task<bool> AddUserAsync(Models.User user);
    Task<Models.User?> GetUserOrDefaultAsync(Guid userId);
}