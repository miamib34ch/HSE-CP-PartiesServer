using PartiesApi.Models;

namespace PartiesApi.Repositories.Friend;

internal interface IFriendRepository
{
    Task<FriendRequest?> GetFriendRequestOrDefaultAsync(Guid userId, Guid friendRequestId);
    Task<bool> AddFriendRequestAsync(FriendRequest friendRequest);
    Task<IEnumerable<FriendRequest>> GetSentFriendRequestsAsync(Guid userId);
    Task<IEnumerable<FriendRequest>> GetReceivedFriendRequestsAsync(Guid userId);
    Task<bool> ChangeFriendRequestStatusAsync(Guid userId, Guid fromUserId, FriendRequestStatus friendRequestStatus);
    Task<IEnumerable<Models.User>> GetFriendsAsync(Guid userId);
}