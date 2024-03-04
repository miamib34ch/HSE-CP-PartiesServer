using PartiesApi.DTO;
using PartiesApi.DTO.Party;

namespace PartiesApi.Services.Party;

public interface IPartyService
{
    Task<MethodResult<PartyResponse>> CreatePartyAsync(PartyRequest partyRequest);
    Task<MethodResult> EditPartyAsync(PartyRequest partyRequest, Guid userId);
    Task<MethodResult<IEnumerable<PartyResponse>>> GetUserOrganizedPartiesAsync(Guid userId);
    Task<MethodResult<IEnumerable<PartyResponse>>> GetUserMemberPartiesAsync(Guid userId);
    Task<MethodResult> DeleteUserFromPartyAsync(Guid partyId, Guid userId);
    Task<MethodResult> AddUserToPartyAsync(Guid partyId, Guid userId);
}