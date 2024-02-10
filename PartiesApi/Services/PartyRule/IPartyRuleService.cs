using PartiesApi.DTO;
using PartiesApi.DTO.PartyRule;

namespace PartiesApi.Services.PartyRule;

public interface IPartyRuleService
{
    Task<MethodResult<IEnumerable<PartyRuleResponse>>> GetPartyRulesAsync();
    Task<MethodResult> CreatePartyRuleAsync(PartyRuleRequest partyRuleRequest);
    Task<MethodResult> EditPartyRuleAsync(PartyRuleRequest partyRuleRequest);
}