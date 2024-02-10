namespace PartiesApi.Repositories.PartyRule;

internal interface IPartyRuleRepository
{
    Task<Models.PartyRule?> GetPartyRuleOrDefaultAsync(Guid partyRuleId);
    Task<bool> AddPartyRuleAsync(Models.PartyRule partyRule);
    Task<bool> EditPartyRuleAsync(Models.PartyRule partyRule);
    Task<IEnumerable<Models.PartyRule>> GetPartyRulesAsync();
}