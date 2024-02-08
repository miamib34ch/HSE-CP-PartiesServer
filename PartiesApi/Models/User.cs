using System.ComponentModel.DataAnnotations.Schema;

namespace PartiesApi.Models;

public class User
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public string? PhoneNumber { get; set; }
    public virtual ICollection<Party> MemberParties { get; set; }
    public virtual ICollection<FriendRequest> SentRequests { get; set; }
    public virtual ICollection<FriendRequest> ReceivedRequests { get; set; }

    [NotMapped]
    public virtual ICollection<User> Friends
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