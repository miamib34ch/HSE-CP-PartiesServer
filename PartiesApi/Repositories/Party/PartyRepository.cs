using Microsoft.EntityFrameworkCore;
using PartiesApi.Database;

namespace PartiesApi.Repositories.Party;

internal class PartyRepository(ApplicationDbContext dbContext) : IPartyRepository
{
    public async Task<bool> AddPartyAsync(Models.Party newParty)
    {
        try
        {
            var createdParty = await dbContext.Parties.AddAsync(newParty);

            return createdParty.State == EntityState.Added;
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
}