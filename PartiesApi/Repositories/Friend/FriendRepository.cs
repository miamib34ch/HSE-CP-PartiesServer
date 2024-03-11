using Microsoft.EntityFrameworkCore;
using PartiesApi.Database;
using PartiesApi.Models;

namespace PartiesApi.Repositories.Friend;

internal class FriendRepository(ApplicationDbContext dbContext) : IFriendRepository
{
    public async Task<FriendRequest?> GetFriendRequestOrDefaultAsync(Guid userId, Guid friendRequestId)
    {
        try
        {
            var friendRequest = await dbContext.FriendRequests
                .Where(request => request.FromUserId == userId && request.ToUserId == friendRequestId)
                .Include(request => request.FromUser)
                .Include(request => request.ToUser)
                .FirstOrDefaultAsync();

            return friendRequest;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<bool> AddFriendRequestAsync(FriendRequest friendRequest)
    {
        try
        {
            var createdFriendRequest = await dbContext.FriendRequests.AddAsync(friendRequest);

            return createdFriendRequest.State == EntityState.Added;
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

    public async Task<IEnumerable<FriendRequest>> GetSentFriendRequestsAsync(Guid userId)
    {
        try
        {
            var sentFriendRequests = await dbContext.FriendRequests
                .Where(request => request.FromUserId == userId)
                .Include(request => request.ToUser)
                .ToListAsync();

            return sentFriendRequests;
        }
        catch (Exception e)
        {
            return new List<FriendRequest>();
        }
    }

    public async Task<IEnumerable<FriendRequest>> GetReceivedFriendRequestsAsync(Guid userId)
    {
        try
        {
            var receivedRequestsAsync = await dbContext.FriendRequests
                .Where(request => request.ToUserId == userId)
                .Include(request => request.FromUser)
                .Include(request => request.ToUser)
                .ToListAsync();

            return receivedRequestsAsync;
        }
        catch (Exception e)
        {
            return new List<FriendRequest>();
        }
    }


    public async Task<IEnumerable<Models.User>> GetFriendsAsync(Guid userId)
    {
        try
        {
            var user = await dbContext.Users
                .Where(user => user.Id == userId)
                .Include(user => user.ReceivedRequests)
                .ThenInclude(friendRequest => friendRequest.FromUser)
                .Include(user => user.SentRequests)
                .ThenInclude(friendRequest => friendRequest.ToUser)
                .Include(user => user.SentFriends)
                .Include(user => user.ReceivedFriends)
                .FirstOrDefaultAsync();

            if (user == null)
                return new List<Models.User>();

            var sentFriends = user.SentFriends
                .Where(userFriend => userFriend.FirstUserId == userId)
                .Select(userFriend => userFriend.SecondUser);
            
            var receivedFriends = user.ReceivedFriends
                .Where(userFriend => userFriend.SecondUserId == userId)
                .Select(userFriend => userFriend.FirstUser);

            return sentFriends.Union(receivedFriends);
        }
        catch (Exception e)
        {
            return new List<Models.User>();
        }
    }

    public bool RemoveFriendRequest(FriendRequest friendRequest)
    {
        try
        {
            var removedEntity = dbContext.FriendRequests.Remove(friendRequest);

            return removedEntity.State == EntityState.Deleted;
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