using PartiesApi.DTO;
using PartiesApi.DTO.Party;
using PartiesApi.Exceptions;
using PartiesApi.Repositories.Party;
using PartiesApi.Services.DressCode;
using PartiesApi.Services.PartyRule;
using PartiesApi.Services.User;

namespace PartiesApi.Services.Party;

public class PartyService(IPartyRepository partyRepository, IDressCodeService dressCodeService,
    IUserService userService, IPartyRuleService partyRuleService) : IPartyService
{
    private readonly PartyCreator _partyCreator = new(dressCodeService, userService, partyRuleService);

    public async Task<MethodResult> CreatePartyAsync(PartyRequest partyRequest)
    {
        const string createPartyMethodName = "CreateParty";

        try
        {
            var newParty = await _partyCreator.CreatePartyAsync(partyRequest);

            var isPartyCreated = await partyRepository.AddPartyAsync(newParty);

            return new MethodResult(createPartyMethodName, isPartyCreated, string.Empty);
        }
        catch (ElementNotFoundException ex)
        {
            return new MethodResult(createPartyMethodName, false,
                $"{ex.ElementName} with ID - {ex.ElementId} is not found");
        }
        catch (Exception ex)
        {
            return new MethodResult(createPartyMethodName, false, $"Unknown error");
        }
    }
}