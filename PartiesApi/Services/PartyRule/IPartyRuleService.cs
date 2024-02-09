namespace PartiesApi.Services.PartyRule;

public interface IPartyRuleService
{
    Task<Models.PartyRule?> GetPartyRuleOrDefaultAsync(Guid partyRuleId);
}