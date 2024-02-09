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

    public async Task<IEnumerable<Models.Party>> GetUserOrganizedPartiesAsync(Guid userId)
    {
        try
        {
            var parties = await dbContext.Parties
                .Where(party => party.Organizer.Id == userId)
                .Include(party => party.Organizer)
                .Include(party => party.DressCode)
                .Include(party => party.PartyEditors)
                .Include(party => party.PartyMembers)
                .Include(party => party.PartyRules)
                .ToListAsync();

            return parties;
        }
        catch (Exception e)
        {
            return new List<Models.Party>();
        }
    }

    public async Task<IEnumerable<Models.Party>> GetUserMemberPartiesAsync(Guid userId)
    {
        try
        {
            var parties = await dbContext.Parties
                .Where(party => party.PartyMembers.Any(user => user.Id == userId))
                .Include(party => party.Organizer)
                .Include(party => party.DressCode)
                .Include(party => party.PartyEditors)
                .Include(party => party.PartyMembers)
                .Include(party => party.PartyRules)
                .ToListAsync();
    
            return parties;
        }
        catch (Exception e)
        {
            return new List<Models.Party>();
        }
    }

    public async Task<Models.Party?> GetPartyOrDefaultAsync(Guid partyId)
    {
        try
        {
            var parties = await dbContext.Parties
                .Include(party => party.Organizer)
                .Include(party => party.DressCode)
                .Include(party => party.PartyEditors)
                .Include(party => party.PartyMembers)
                .Include(party => party.PartyRules)
                .FirstOrDefaultAsync(party => party.Id == partyId);
    
            return parties;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public async Task<bool> UpdatePartyAsync(Models.Party party)
    {
        try
        {
            var createdParty = dbContext.Parties.Update(party);

            return createdParty.State == EntityState.Modified;
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