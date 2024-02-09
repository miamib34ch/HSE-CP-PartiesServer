namespace PartiesApi.Repositories.Party;

public interface IPartyRepository
{
    Task<bool> AddPartyAsync(Models.Party newParty);
}