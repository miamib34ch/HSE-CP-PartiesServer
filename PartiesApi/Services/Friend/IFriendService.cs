using PartiesApi.DTO;
using PartiesApi.DTO.FriendRequest;
using PartiesApi.DTO.User;
using PartiesApi.Models;

namespace PartiesApi.Services.Friend;

public interface IFriendService
{
    Task<MethodResult> SendFriendRequestAsync(Guid userId, Guid friendRequestId);
    Task<MethodResult<IEnumerable<ReceivedFriendRequestResponse>>> GetReceivedFriendRequestsAsync(Guid userId);
    Task<MethodResult<IEnumerable<UserResponse>>> GetFriendsAsync(Guid userId);
    Task<MethodResult> DeleteUserFromFriendsAsync(Guid userId, Guid friendId);
    Task<MethodResult> AcceptFriendRequestAsync(Guid userId, Guid fromUserId);
    Task<MethodResult> RejectFriendRequestsAsync(Guid userId, Guid fromUserId);
}