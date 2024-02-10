using PartiesApi.DTO.User;
using PartiesApi.Models;

namespace PartiesApi.DTO.FriendRequest;

public record SentFriendRequestResponse
{
    public UserResponse ToUser { get; init; }
    public FriendRequestStatus Status { get; init; }
}