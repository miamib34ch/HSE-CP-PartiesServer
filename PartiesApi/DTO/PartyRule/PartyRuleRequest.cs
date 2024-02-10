namespace PartiesApi.DTO.PartyRule;

public record PartyRuleRequest
{
    public Guid? Id { get; init; }
    public string Description { get; init; }
}