namespace PartiesApi.DTO.PartyRule;

public record PartyRuleResponse
{
    public Guid Id { get; init; }
    public string Description { get; init; }
}