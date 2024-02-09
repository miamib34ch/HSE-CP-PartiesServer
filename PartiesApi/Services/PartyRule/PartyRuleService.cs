using PartiesApi.Repositories.PartyRule;

namespace PartiesApi.Services.PartyRule;

internal class PartyRuleService(IPartyRuleRepository partyRuleRepository) : IPartyRuleService
{
    public async Task<Models.PartyRule?> GetPartyRuleOrDefaultAsync(Guid partyRuleId)
    {
        var partyRule = await partyRuleRepository.GetPartyRuleOrDefaultAsync(partyRuleId);

        return partyRule;
    }
}