namespace PartiesApi.DTO.Party;

public record PartyRequest
{
    public string Name { get; init; }
    public string? Description { get; init; }
    public Guid? DressCodeId { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime FinishTime { get; init; }
    public Guid OrganizerId { get; init; }
    public IList<Guid>? PartyMembersIds { get; init; }
    public IList<Guid>? PartyEditorsIds { get; init; }
    public double? LocationLatitude { get; init; }
    public double? LocationLongitude { get; init; }
    public IList<Guid>? PartyRulesIds { get; init; }
}