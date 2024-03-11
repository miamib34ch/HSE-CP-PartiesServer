namespace PartiesApi.DTO.User;

public class UserWithFriendStatusResponse
{
    public Guid Id { get; init; }
    public string Login { get; init; }
    public FriendStatus FriendStatus { get; set; }
}