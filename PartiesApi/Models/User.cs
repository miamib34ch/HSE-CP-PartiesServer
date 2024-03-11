using System.ComponentModel.DataAnnotations.Schema;

namespace PartiesApi.Models;

internal class User
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public string? PhoneNumber { get; set; }
    public virtual IList<Party> MemberParties { get; set; }
    public virtual IList<Party> EditorParties { get; set; }
    public virtual IList<FriendRequest> SentRequests { get; set; }
    public virtual IList<FriendRequest> ReceivedRequests { get; set; }
    public virtual IList<UserFriend> SentFriends { get; set; }
    public virtual IList<UserFriend> ReceivedFriends { get; set; }

    [NotMapped]
    public IEnumerable<UserFriend> Friends => SentFriends.Union(ReceivedFriends);

    public void RemoveFriend(UserFriend userFriend)
    {
        SentFriends.Remove(userFriend);
        ReceivedFriends.Remove(userFriend);
    }
}