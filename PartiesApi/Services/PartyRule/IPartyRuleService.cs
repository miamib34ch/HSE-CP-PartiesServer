namespace PartiesApi.Services.PartyRule;

internal interface IPartyRuleService
{
    Task<Models.PartyRule?> GetPartyRuleOrDefaultAsync(Guid partyRuleId);
}