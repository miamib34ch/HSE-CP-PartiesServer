using PartiesApi.DTO;
using PartiesApi.DTO.FriendRequest;
using PartiesApi.DTO.User;
using PartiesApi.Models;

namespace PartiesApi.Services.Friend;

public interface IFriendService
{
    Task<MethodResult> SendFriendRequestAsync(Guid userId, Guid friendRequestId);
    Task<MethodResult<IEnumerable<SentFriendRequestResponse>>> GetSentFriendRequestsAsync(Guid userId);
    Task<MethodResult<IEnumerable<ReceivedFriendRequestResponse>>> GetReceivedFriendRequestsAsync(Guid userId);
    Task<MethodResult> ChangeFriendRequestStatusAsync(Guid userId, Guid fromUserId,
        FriendRequestStatus friendRequestStatus);
    Task<MethodResult<IEnumerable<UserResponse>>> GetFriendsAsync(Guid userId);
}