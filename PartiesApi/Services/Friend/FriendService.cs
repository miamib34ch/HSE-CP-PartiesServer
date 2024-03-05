using AutoMapper;
using PartiesApi.DTO;
using PartiesApi.DTO.FriendRequest;
using PartiesApi.DTO.User;
using PartiesApi.Models;
using PartiesApi.Repositories.Friend;

namespace PartiesApi.Services.Friend;

internal class FriendService(IFriendRepository friendRepository, IMapper mapper) : IFriendService
{
    public async Task<MethodResult> SendFriendRequestAsync(Guid userId, Guid friendRequestId)
    {
        const string methodName = "SendFriendRequest";

        try
        {
            var existingRequest = await friendRepository.GetFriendRequestOrDefaultAsync(userId, friendRequestId);
            if (existingRequest != null)
                return new MethodResult(methodName, true, string.Empty);

            var newFriendRequest = new FriendRequest()
            {
                FromUserId = userId,
                ToUserId = friendRequestId,
                Status = FriendRequestStatus.None
            };

            var friendRequestCreated = await friendRepository.AddFriendRequestAsync(newFriendRequest);

            return new MethodResult(methodName, friendRequestCreated, string.Empty);
        }
        catch (Exception ex)
        {
            return new MethodResult(methodName, false, $"Unknown error");
        }
    }

    public async Task<MethodResult<IEnumerable<SentFriendRequestResponse>>> GetSentFriendRequestsAsync(Guid userId)
    {
        const string methodName = "SendFriendRequest";

        try
        {
            var sentRequests = await friendRepository.GetSentFriendRequestsAsync(userId);

            var sentRequestResponses =
                sentRequests.Select(mapper.Map<FriendRequest, SentFriendRequestResponse>).ToList();

            return new MethodResult<IEnumerable<SentFriendRequestResponse>>(methodName, true,
                string.Empty, sentRequestResponses);
        }
        catch (Exception ex)
        {
            return new MethodResult<IEnumerable<SentFriendRequestResponse>>(methodName, false, $"Unknown error");
        }
    }

    public async Task<MethodResult<IEnumerable<ReceivedFriendRequestResponse>>> GetReceivedFriendRequestsAsync(Guid userId)
    {
        const string methodName = "ReceivedFriendRequest";

        try
        {
            var receivedRequests = await friendRepository.GetReceivedFriendRequestsAsync(userId);

            var receivedRequestResponses =
                receivedRequests.Select(mapper.Map<FriendRequest, ReceivedFriendRequestResponse>).ToList();

            return new MethodResult<IEnumerable<ReceivedFriendRequestResponse>>(methodName, true,
                string.Empty, receivedRequestResponses);
        }
        catch (Exception ex)
        {
            return new MethodResult<IEnumerable<ReceivedFriendRequestResponse>>(methodName, false, $"Unknown error");
        }
    }

    public async Task<MethodResult> ChangeFriendRequestStatusAsync(Guid userId, Guid fromUserId,
        FriendRequestStatus friendRequestStatus)
    {
        const string methodName = "ChangeFriendRequestStatus";

        try
        {
            var existingRequest = await friendRepository.GetFriendRequestOrDefaultAsync(fromUserId, userId);
            if (existingRequest == null)
                return new MethodResult(methodName, false, $"You don't have request from user with Id {fromUserId}");
            
            var requestAccepted = await friendRepository.ChangeFriendRequestStatusAsync(userId, fromUserId, friendRequestStatus);
            
            return new MethodResult(methodName, requestAccepted, string.Empty);
        }
        catch (Exception ex)
        {
            return new MethodResult(methodName, false, $"Unknown error");
        }
    }

    public async Task<MethodResult<IEnumerable<UserResponse>>> GetFriendsAsync(Guid userId)
    {
        const string methodName = "GetFriendsAsync";

        try
        {
            var friends = await friendRepository.GetFriendsAsync(userId);

            var friendResponses = friends.Select(mapper.Map<Models.User, UserResponse>).ToList();

            return new MethodResult<IEnumerable<UserResponse>>(methodName, true, string.Empty, friendResponses);
        }
        catch (Exception ex)
        {
            return new MethodResult<IEnumerable<UserResponse>>(methodName, false, $"Unknown error");
        }
    }
}