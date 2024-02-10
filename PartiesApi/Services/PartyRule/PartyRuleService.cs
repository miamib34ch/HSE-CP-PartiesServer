using AutoMapper;
using PartiesApi.DTO;
using PartiesApi.DTO.PartyRule;
using PartiesApi.Repositories.PartyRule;

namespace PartiesApi.Services.PartyRule;

internal class PartyRuleService(IPartyRuleRepository partyRuleRepository, IMapper mapper) : IPartyRuleService
{
     public async Task<MethodResult<IEnumerable<PartyRuleResponse>>> GetPartyRulesAsync()
    {
        const string methodName = "GetPartyRules";

        try
        {
            var partyRules = await partyRuleRepository.GetPartyRulesAsync();

            var partyRuleResponses = partyRules.Select(mapper.Map<Models.PartyRule, PartyRuleResponse>).ToList();

            return new MethodResult<IEnumerable<PartyRuleResponse>>(methodName, true, string.Empty, partyRuleResponses);
        }
        catch (Exception ex)
        {
            return new MethodResult<IEnumerable<PartyRuleResponse>>(methodName, false, $"Unknown error",
                new List<PartyRuleResponse>());
        }
    }

    public async Task<MethodResult> CreatePartyRuleAsync(PartyRuleRequest partyRuleRequest)
    {
        const string methodName = "CreatePartyRule";

        try
        {
            var partyRule = mapper.Map<PartyRuleRequest, Models.PartyRule>(partyRuleRequest);
            var isPartyRuleCreated = await partyRuleRepository.AddPartyRuleAsync(partyRule);
            return new MethodResult(methodName, isPartyRuleCreated, string.Empty);
        }
        catch (Exception ex)
        {
            return new MethodResult(methodName, false, $"Unknown error");
        }
    }

    public async Task<MethodResult> EditPartyRuleAsync(PartyRuleRequest partyRuleRequest)
    {
        const string methodName = "EditPartyRule";

        try
        {
            if (partyRuleRequest.Id == null)
                return new MethodResult(methodName, false, "No dress code Id in request");

            var existingPartyRule = await partyRuleRepository.GetPartyRuleOrDefaultAsync((Guid)partyRuleRequest.Id);
            if (existingPartyRule == null)
                return new MethodResult(methodName, false, $"Dress code with Id {partyRuleRequest.Id} does not exist");

            var partyRule = mapper.Map<PartyRuleRequest, Models.PartyRule>(partyRuleRequest);
            var isPartyRuleEdited = await partyRuleRepository.EditPartyRuleAsync(partyRule);
            return new MethodResult(methodName, isPartyRuleEdited, string.Empty);
        }
        catch (Exception ex)
        {
            return new MethodResult(methodName, false, $"Unknown error");
        }
    }
}