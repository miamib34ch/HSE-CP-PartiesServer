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

    [NotMapped]
    public virtual IList<User> Friends
    {
        get
        {
            var friends = SentRequests.Where(r => r.Status == FriendRequestStatus.Approved).Select(r => r.ToUser)
                .ToList();
            friends.AddRange(ReceivedRequests.Where(r => r.Status == FriendRequestStatus.Approved)
                .Select(r => r.FromUser));
            return friends;
        }
    }
}