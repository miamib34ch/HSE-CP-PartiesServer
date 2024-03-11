using AutoMapper;
using PartiesApi.DTO;
using PartiesApi.DTO.User;
using PartiesApi.Repositories.User;
using PartiesApi.Utils;

namespace PartiesApi.Services.User;

internal class UserService(IUserRepository userRepository, IMapper mapper) : IUserService
{
    public async Task<bool> CheckUserExistenceAsync(string userLogin)
    {
        var user = await userRepository.GetUserOrDefaultAsync(userLogin);

        return user != null;
    }

    public async Task<bool> CheckUserPasswordAsync(string userLogin, string password)
    {
        var user = await userRepository.GetUserOrDefaultAsync(userLogin);
        var passwordHash = StringHasher.GetSha256Hash(password);

        return user != null && passwordHash == user.PasswordHash;
    }

    public async Task<UserResponse?> CreateUserAsync(string login, string password)
    {
        var passwordHash = StringHasher.GetSha256Hash(password);

        var user = new Models.User()
        {
            Login = login,
            PasswordHash = passwordHash
        };

        var createdUser = await userRepository.AddUserAsync(user);

        if (createdUser == null)
            return null;

        var userResponse = mapper.Map<Models.User, UserResponse>(createdUser);

        return userResponse;
    }

    public async Task<UserResponse?> GetUserOrDefaultAsync(Guid userId)
    {
        var user = await userRepository.GetUserOrDefaultAsync(userId);

        if (user == null)
            return null;

        var userResponse = mapper.Map<Models.User, UserResponse>(user);

        return userResponse;
    }

    public async Task<UserResponse?> GetUserOrDefaultAsync(string userLogin)
    {
        var user = await userRepository.GetUserOrDefaultAsync(userLogin);

        if (user == null)
            return null;

        var userResponse = mapper.Map<Models.User, UserResponse>(user);

        return userResponse;
    }

    public async Task<MethodResult<IEnumerable<UserWithFriendStatusResponse>>> FindUsersAsync(Guid userId,
        string userLogin)
    {
        const string methodName = "FindUsers";

        try
        {
            var userResponses = new List<UserWithFriendStatusResponse>();
            var foundUsers = await userRepository.FindUsersAsync(userLogin);
            var user = await userRepository.GetUserOrDefaultAsync(userId);

            foreach (var foundUser in foundUsers)
            {
                var userResponse = new UserWithFriendStatusResponse()
                {
                    Id = foundUser.Id,
                    Login = foundUser.Login,
                    FriendStatus = FriendStatus.NotFriend
                };

                var isFoundUserFriend = user != null && user.Friends.Any(friend =>
                    friend.FirstUserId == foundUser.Id || friend.SecondUserId == foundUser.Id);
                if (isFoundUserFriend)
                    userResponse.FriendStatus = FriendStatus.Friend;

                var isRequestSent = user != null &&
                                    user.SentRequests.Any(friendRequest => friendRequest.ToUserId == foundUser.Id);
                if (isRequestSent)
                    userResponse.FriendStatus = FriendStatus.RequestSent;

                var isRequestReceived = user != null &&
                                        user.ReceivedRequests.Any(friendRequest =>
                                            friendRequest.FromUserId == foundUser.Id);
                if (isRequestReceived)
                    userResponse.FriendStatus = FriendStatus.RequestReceived;

                userResponses.Add(userResponse);
            }

            return new MethodResult<IEnumerable<UserWithFriendStatusResponse>>(methodName, true,
                string.Empty, userResponses);
        }
        catch (Exception ex)
        {
            return new MethodResult<IEnumerable<UserWithFriendStatusResponse>>(methodName, false, $"Unknown error");
        }
    }
}