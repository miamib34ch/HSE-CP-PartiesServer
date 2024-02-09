namespace PartiesApi.Repositories.PartyRule;

internal interface IPartyRuleRepository
{
    Task<Models.PartyRule?> GetPartyRuleOrDefaultAsync(Guid partyRuleId);
}