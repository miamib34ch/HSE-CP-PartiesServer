using System.ComponentModel.DataAnnotations;

namespace PartiesApi.Models;

internal class Party
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string? Name { get; set; }
    public string? Description { get; set; }
    public virtual DressCode? DressCode { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime FinishTime { get; set; }
    public virtual User? Organizer { get; set; }
    public virtual IList<User>? PartyMembers { get; set; }
    public virtual IList<User>? PartyEditors { get; set; }
    public double LocationLatitude { get; set; }
    public double LocationLongitude { get; set; }
    public virtual IList<PartyRule> PartyRules { get; set; }
}