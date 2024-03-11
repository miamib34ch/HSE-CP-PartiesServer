using AutoMapper;
using PartiesApi.DTO;
using PartiesApi.DTO.FriendRequest;
using PartiesApi.DTO.User;
using PartiesApi.Models;
using PartiesApi.Repositories.Friend;
using PartiesApi.Repositories.User;

namespace PartiesApi.Services.Friend;

internal class FriendService
    (IFriendRepository friendRepository, IUserRepository userRepository, IMapper mapper) : IFriendService
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
            };

            var friendRequestCreated = await friendRepository.AddFriendRequestAsync(newFriendRequest);

            return new MethodResult(methodName, friendRequestCreated, string.Empty);
        }
        catch (Exception ex)
        {
            return new MethodResult(methodName, false, $"Unknown error");
        }
    }

    public async Task<MethodResult<IEnumerable<ReceivedFriendRequestResponse>>>
        GetReceivedFriendRequestsAsync(Guid userId)
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

    public async Task<MethodResult> DeleteUserFromFriendsAsync(Guid userId, Guid friendId)
    {
        const string methodName = "DeleteUserFromFriends";

        try
        {
            var user = await userRepository.GetUserOrDefaultAsync(userId);

            if (user == null)
                return new MethodResult(methodName, false, "User does not exist");

            var userFriend = user.Friends.FirstOrDefault(friendUser => 
                (friendUser.FirstUserId == userId && friendUser.SecondUserId == friendId) || (friendUser.FirstUserId == friendId && friendUser.SecondUserId == userId));

            if (userFriend == null)
                return new MethodResult(methodName, false, "User is not your friend");

            user.RemoveFriend(userFriend);

            var isUserUpdated = userRepository.UpdateUser(user);

            return new MethodResult<IEnumerable<UserResponse>>(methodName, isUserUpdated, string.Empty);
        }
        catch (Exception ex)
        {
            return new MethodResult<IEnumerable<UserResponse>>(methodName, false, $"Unknown error");
        }
    }

    public async Task<MethodResult> AcceptFriendRequestAsync(Guid userId, Guid fromUserId)
    {
        const string methodName = "AcceptFriendRequest";

        try
        {
            var friendRequests = await friendRepository.GetReceivedFriendRequestsAsync(userId);

            var friendRequest = friendRequests.FirstOrDefault(request =>
                request.ToUserId == userId && request.FromUserId == fromUserId);
            if (friendRequest == null)
                return new MethodResult(methodName, false, "You do not have request from this user");

            var friendUser = await userRepository.GetUserOrDefaultAsync(fromUserId);
            if (friendUser == null)
                return new MethodResult(methodName, false, "friendUser does not exist");

            var user = await userRepository.GetUserOrDefaultAsync(userId);
            if (user == null)
                return new MethodResult(methodName, false, "User does not exist");

            var userFriend = new UserFriend()
            {
                FirstUser = friendUser,
                FirstUserId = fromUserId,
                SecondUser = user,
                SecondUserId = userId
            };

            user.ReceivedFriends.Add(userFriend);

            var friendAdded = userRepository.UpdateUser(user);

            var isFriendRequestRemoved = friendRepository.RemoveFriendRequest(friendRequest);

            return new MethodResult<IEnumerable<UserResponse>>(methodName, friendAdded && isFriendRequestRemoved,
                string.Empty);
        }
        catch (Exception ex)
        {
            return new MethodResult<IEnumerable<UserResponse>>(methodName, false, $"Unknown error");
        }
    }

    public async Task<MethodResult> RejectFriendRequestAsync(Guid userId, Guid fromUserId)
    {
        const string methodName = "RejectFriendRequests";

        try
        {
            var friendRequests = await friendRepository.GetReceivedFriendRequestsAsync(userId);

            var friendRequest = friendRequests.FirstOrDefault(request =>
                request.ToUserId == userId && request.FromUserId == fromUserId);
            if (friendRequest == null)
                return new MethodResult(methodName, false, "You do not have request from this user"); ;

            var isFriendRequestRemoved = friendRepository.RemoveFriendRequest(friendRequest);

            return new MethodResult<IEnumerable<UserResponse>>(methodName, isFriendRequestRemoved,
                string.Empty);
        }
        catch (Exception ex)
        {
            return new MethodResult<IEnumerable<UserResponse>>(methodName, false, $"Unknown error");
        }
    }
}