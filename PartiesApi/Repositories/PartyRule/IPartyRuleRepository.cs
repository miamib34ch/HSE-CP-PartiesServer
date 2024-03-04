namespace PartiesApi.Repositories.PartyRule;

internal interface IPartyRuleRepository
{
    Task<Models.PartyRule?> GetPartyRuleOrDefaultAsync(Guid partyRuleId);
    Task<Models.PartyRule?> AddPartyRuleAsync(Models.PartyRule partyRule);
    Task<bool> EditPartyRuleAsync(Models.PartyRule partyRule);
    Task<IEnumerable<Models.PartyRule>> GetPartyRulesAsync();
}