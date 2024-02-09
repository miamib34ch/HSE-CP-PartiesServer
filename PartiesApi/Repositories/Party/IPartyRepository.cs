namespace PartiesApi.Repositories.Party;

internal interface IPartyRepository
{
    Task<bool> AddPartyAsync(Models.Party newParty);
}