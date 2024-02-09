namespace PartiesApi.Repositories.Party;

internal interface IPartyRepository
{
    Task<bool> AddPartyAsync(Models.Party newParty);
    Task<IEnumerable<Models.Party>> GetUserOrganizedPartiesAsync(Guid userId);
    Task<IEnumerable<Models.Party>> GetUserMemberPartiesAsync(Guid userId);
    Task<Models.Party?> GetPartyOrDefaultAsync(Guid partyId);
    Task<bool> UpdatePartyAsync(Models.Party party);
}