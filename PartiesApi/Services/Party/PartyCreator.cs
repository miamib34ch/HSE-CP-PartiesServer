using PartiesApi.DTO.Party;
using PartiesApi.Exceptions;
using PartiesApi.Repositories.DressCode;
using PartiesApi.Repositories.PartyRule;
using PartiesApi.Repositories.User;
using PartiesApi.Utils;

namespace PartiesApi.Services.Party;

internal class PartyCreator(IDressCodeRepository dressCodeRepository, IUserRepository userRepository,
    IPartyRuleRepository partyRuleRepository)
{
    public async Task<Models.Party> CreatePartyAsync(PartyRequest partyRequest)
    {
        var newParty = new Models.Party();
        
        if (partyRequest.Id != null)
            newParty.Id = (Guid)partyRequest.Id;

        if (partyRequest.DressCodeId != null)
            newParty.DressCode = await GetDressCodeAsync((Guid)partyRequest.DressCodeId);

        if (partyRequest.OrganizerId != null)
            newParty.Organizer = await GetOrganizerAsync((Guid)partyRequest.OrganizerId);

        if (partyRequest.PartyMembersIds != null)
            newParty.PartyMembers = await GetPartyUsersAsync(partyRequest.PartyMembersIds, PartyRole.Member);

        if (partyRequest.PartyEditorsIds != null)
            newParty.PartyEditors = await GetPartyUsersAsync(partyRequest.PartyEditorsIds, PartyRole.Editor);

        if (partyRequest.PartyRulesIds != null)
            newParty.PartyRules = await GetPartyRulesAsync(partyRequest.PartyRulesIds);

        if (partyRequest.LocationLatitude != null)
            newParty.LocationLatitude = (double)partyRequest.LocationLatitude;

        if (partyRequest.LocationLongitude != null)
            newParty.LocationLongitude = (double)partyRequest.LocationLongitude;

        newParty.Name = partyRequest.Name;
        newParty.Description = partyRequest.Description;
        newParty.StartTime = partyRequest.StartTime;
        newParty.FinishTime = partyRequest.FinishTime;

        return newParty;
    }

    private async Task<Models.DressCode> GetDressCodeAsync(Guid dressCodeId)
    {
        var dressCode = await dressCodeRepository.GetDressCodeOrDefaultAsync(dressCodeId);

        if (dressCode == null)
            throw new ElementNotFoundException("Dress Code", dressCodeId);

        return dressCode;
    }

    private async Task<Models.User> GetOrganizerAsync(Guid organizerId)
    {
        var organizer = await userRepository.GetUserOrDefaultAsync(organizerId);

        if (organizer == null)
            throw new ElementNotFoundException($"{EnumDescriptionReader.GetEnumDescription(PartyRole.Organizer)}",
                organizerId);

        return organizer;
    }

    private async Task<IList<Models.User>> GetPartyUsersAsync(IEnumerable<Guid> partyMemberIds, PartyRole partyRole)
    {
        var partyMembers = new List<Models.User>();

        foreach (var partyMemberId in partyMemberIds)
        {
            var partyMember = await userRepository.GetUserOrDefaultAsync(partyMemberId);

            if (partyMember == null)
                throw new ElementNotFoundException($"Party {EnumDescriptionReader.GetEnumDescription(partyRole)}",
                    partyMemberId);

            partyMembers.Add(partyMember);
        }

        return partyMembers;
    }

    private async Task<IList<Models.PartyRule>> GetPartyRulesAsync(IEnumerable<Guid> partyRuleIds)
    {
        var partyRules = new List<Models.PartyRule>();

        foreach (var partyRuleId in partyRuleIds)
        {
            var partyRule = await partyRuleRepository.GetPartyRuleOrDefaultAsync(partyRuleId);

            if (partyRule == null)
                throw new ElementNotFoundException($"Party Party Rule", partyRuleId);

            partyRules.Add(partyRule);
        }

        return partyRules;
    }
}