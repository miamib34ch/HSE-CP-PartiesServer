namespace PartiesApi.Models;

internal class UserFriend
{
    public Guid FirstUserId { get; set; }
    public Guid SecondUserId { get; set; }
    public virtual User FirstUser { get; set; }
    public virtual User SecondUser { get; set; }
}