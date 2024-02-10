using PartiesApi.DTO.DressCode;
using PartiesApi.DTO.PartyRule;
using PartiesApi.DTO.User;

namespace PartiesApi.DTO.Party;

public record PartyResponse
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public DressCodeResponse? DressCode { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime FinishTime { get; init; }
    public UserResponse? Organizer { get; init; }
    public IList<UserResponse>? PartyMembers { get; init; }
    public IList<UserResponse>? PartyEditors { get; init; }
    public double LocationLatitude { get; init; }
    public double LocationLongitude { get; init; }
    public IList<PartyRuleResponse>? PartyRules { get; init; }
}