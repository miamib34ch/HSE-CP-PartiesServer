using Microsoft.EntityFrameworkCore;
using PartiesApi.Database;

namespace PartiesApi.Repositories.PartyRule;

internal class PartyRuleRepository(ApplicationDbContext dbContext) : IPartyRuleRepository
{
    public async Task<Models.PartyRule?> GetPartyRuleOrDefaultAsync(Guid partyRuleId)
    {
        try
        {
            var partyRule = await dbContext.PartyRules.FirstOrDefaultAsync(partyRule => partyRule.Id == partyRuleId);

            return partyRule;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}