using PartiesApi.DTO;
using PartiesApi.DTO.Party;

namespace PartiesApi.Services.Party;

public interface IPartyService
{
    Task<MethodResult> CreatePartyAsync(PartyRequest partyRequest);
}