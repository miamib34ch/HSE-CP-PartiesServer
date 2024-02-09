namespace PartiesApi.Models;

internal class FriendRequest
{
    public Guid FromUserId { get; set; }
    public Guid ToUserId { get; set; }
    public virtual User FromUser { get; set; }
    public virtual User ToUser { get; set; }
    public FriendRequestStatus Status { get; set; }
}