namespace PartiesApi.Repositories.PartyRule;

public interface IPartyRuleRepository
{
    Task<Models.PartyRule?> GetPartyRuleOrDefaultAsync(Guid partyRuleId);
}