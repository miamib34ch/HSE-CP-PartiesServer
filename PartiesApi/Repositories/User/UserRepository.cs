using Microsoft.EntityFrameworkCore;
using PartiesApi.Database;

namespace PartiesApi.Repositories.User;

public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
{
    public async Task<Models.User?> GetUserOrDefaultAsync(string userLogin)
    {
        try
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(user => user.Login == userLogin);

            return user;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<bool> AddUserAsync(Models.User user)
    {
        try
        {
            var createdUser = await dbContext.Users.AddAsync(user);

            return createdUser.State == EntityState.Added;
        }
        catch (Exception e)
        {
            return false;
        }
        finally
        {
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<Models.User?> GetUserOrDefaultAsync(Guid userId)
    {
        try
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId);

            return user;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}