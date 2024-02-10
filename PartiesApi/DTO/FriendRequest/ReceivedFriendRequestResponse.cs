using PartiesApi.DTO.User;
using PartiesApi.Models;

namespace PartiesApi.DTO.FriendRequest;

public record ReceivedFriendRequestResponse
{
    public UserResponse FromUser { get; init; }
    public FriendRequestStatus Status { get; init; }
}