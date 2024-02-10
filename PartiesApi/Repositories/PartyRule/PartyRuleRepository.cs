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

    public async Task<bool> AddPartyRuleAsync(Models.PartyRule partyRule)
    {
        try
        {
            var createdPartyRule = await dbContext.PartyRules.AddAsync(partyRule);

            return createdPartyRule.State == EntityState.Added;
        }
        catch (Exception e)
        {
            return false;
        }
        finally
        {
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<bool> EditPartyRuleAsync(Models.PartyRule newPartyRule)
    {
        try
        {
            var existingPartyRule = await dbContext.PartyRules
                .FirstOrDefaultAsync(partyRule => partyRule.Id == newPartyRule.Id);

            if (existingPartyRule != null)
            {
                existingPartyRule.Description = newPartyRule.Description;

                var updatedPartyRule = dbContext.PartyRules.Update(existingPartyRule);

                return updatedPartyRule.State == EntityState.Modified;
            }
        }
        catch (Exception e)
        {
            return false;
        }
        finally
        {
            await dbContext.SaveChangesAsync();
        }

        return false;
    }

    public async Task<IEnumerable<Models.PartyRule>> GetPartyRulesAsync()
    {
        try
        {
            var partyRules = await dbContext.PartyRules.ToListAsync();

            return partyRules;
        }
        catch (Exception e)
        {
            return new List<Models.PartyRule>();
        }
    }
}