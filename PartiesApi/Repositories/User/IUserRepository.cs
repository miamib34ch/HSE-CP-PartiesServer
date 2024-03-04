namespace PartiesApi.Repositories.User;

internal interface IUserRepository
{
    Task<Models.User?> GetUserOrDefaultAsync(string userLogin);
    Task<Models.User?> AddUserAsync(Models.User user);
    Task<Models.User?> GetUserOrDefaultAsync(Guid userId);
    Task<IEnumerable<Models.User>> FindUsersAsync(string userLogin);
}