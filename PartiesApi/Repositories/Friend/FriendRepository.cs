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
                .Where(request => request.FromUserId == userId && request.Status != FriendRequestStatus.Approved)
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
                .Where(request => request.ToUserId == userId && request.Status != FriendRequestStatus.Approved)
                .Include(request => request.FromUser)
                .ToListAsync();

            return receivedRequestsAsync;
        }
        catch (Exception e)
        {
            return new List<FriendRequest>();
        }
    }

    public async Task<bool> ChangeFriendRequestStatusAsync(Guid userId, Guid fromUserId,
        FriendRequestStatus friendRequestStatus)
    {
        try
        {
            var friendRequest = await dbContext.FriendRequests
                .Where(request => request.FromUserId == fromUserId && request.ToUserId == userId)
                .FirstOrDefaultAsync();

            if (friendRequest == null)
                return false;

            friendRequest.Status = friendRequestStatus;

            var updatedRequest = dbContext.FriendRequests.Update(friendRequest);

            return updatedRequest.State == EntityState.Modified;
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
                .FirstOrDefaultAsync();

            return user != null ? user.Friends : new List<Models.User>();
        }
        catch (Exception e)
        {
            return new List<Models.User>();
        }
    }
}