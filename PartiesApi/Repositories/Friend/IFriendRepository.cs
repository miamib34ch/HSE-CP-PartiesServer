using PartiesApi.Models;

namespace PartiesApi.Repositories.Friend;

internal interface IFriendRepository
{
    Task<FriendRequest?> GetFriendRequestOrDefaultAsync(Guid userId, Guid friendRequestId);
    Task<bool> AddFriendRequestAsync(FriendRequest friendRequest);
    Task<IEnumerable<FriendRequest>> GetReceivedFriendRequestsAsync(Guid userId);
    Task<IEnumerable<Models.User>> GetFriendsAsync(Guid userId);
    bool RemoveFriendRequest(FriendRequest friendRequest);
}