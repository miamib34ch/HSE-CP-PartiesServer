using Microsoft.EntityFrameworkCore;
using PartiesApi.Database;

namespace PartiesApi.Repositories.User;

internal class UserRepository(ApplicationDbContext dbContext) : IUserRepository
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

    public async Task<Models.User?> AddUserAsync(Models.User user)
    {
        try
        {
            var createdUser = await dbContext.Users.AddAsync(user);

            return createdUser.Entity;
        }
        catch (Exception e)
        {
            return null;
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
            var user = await dbContext.Users
                .Include(user => user.SentFriends)
                .Include(user => user.ReceivedFriends)
                .Include(user => user.SentRequests)
                .Include(user => user.ReceivedRequests)
                .FirstOrDefaultAsync(user => user.Id == userId);

            return user;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<IEnumerable<Models.User>> FindUsersAsync(string userLogin)
    {
        try
        {
            var user = await dbContext.Users.Where(user => user.Login.Contains(userLogin)).ToListAsync();

            return user;
        }
        catch (Exception e)
        {
            return Enumerable.Empty<Models.User>();
        }
    }

    public bool UpdateUser(Models.User user)
    {
        try
        {
            var updatedUser = dbContext.Users.Update(user);

            return updatedUser.State == EntityState.Modified;
        }
        catch (Exception e)
        {
            return false;
        }
        finally
        {
            dbContext.SaveChanges();
        }
    }
}