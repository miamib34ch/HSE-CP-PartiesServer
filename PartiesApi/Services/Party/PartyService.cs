using AutoMapper;
using PartiesApi.DTO;
using PartiesApi.DTO.Party;
using PartiesApi.Exceptions;
using PartiesApi.Repositories.DressCode;
using PartiesApi.Repositories.Party;
using PartiesApi.Repositories.PartyRule;
using PartiesApi.Repositories.User;

namespace PartiesApi.Services.Party;

internal class PartyService(IPartyRepository partyRepository, IDressCodeRepository dressCodeRepository,
    IUserRepository userRepository, IPartyRuleRepository partyRuleRepository, IMapper mapper) : IPartyService
{
    private readonly PartyCreator _partyCreator = new(dressCodeRepository, userRepository, partyRuleRepository);

    public async Task<MethodResult<PartyResponse>> CreatePartyAsync(PartyRequest partyRequest)
    {
        const string methodName = "CreateParty";

        try
        {
            var newParty = await _partyCreator.CreatePartyAsync(partyRequest);
            var createdParty = await partyRepository.AddPartyAsync(newParty);
            var isPartyCreated = createdParty != null;
            
            if (!isPartyCreated)
                return new MethodResult<PartyResponse>(methodName, isPartyCreated, string.Empty);

            var partyResponse = mapper.Map<Models.Party, PartyResponse>(createdParty);
            return new MethodResult<PartyResponse>(methodName, isPartyCreated, string.Empty, partyResponse);
        }
        catch (ElementNotFoundException ex)
        {
            return new MethodResult<PartyResponse>(methodName, false,
                $"{ex.ElementName} with ID - {ex.ElementId} is not found");
        }
        catch (Exception ex)
        {
            return new MethodResult<PartyResponse>(methodName, false, $"Unknown error");
        }
    }

    public async Task<MethodResult> EditPartyAsync(PartyRequest partyRequest, Guid userId)
    {
        const string methodName = "EditParty";

        try
        {
            if (partyRequest.Id == null)
                return new MethodResult(methodName, false, "No party Id in request");

            var existingParty = await partyRepository.GetPartyOrDefaultAsync((Guid)partyRequest.Id);
            if (existingParty == null)
                return new MethodResult(methodName, false, $"Party with Id {partyRequest.Id} does not exist");

            var canUserEditParty = existingParty.Organizer.Id != userId
                                   || existingParty.PartyEditors.Select(editor => editor.Id).Contains(userId);
            if (!canUserEditParty)
                return new MethodResult(methodName, false, $"This party is not yours. You can't edit it!");

            var editedParty = await _partyCreator.CreatePartyAsync(partyRequest);
            var isPartyUpdated = await partyRepository.UpdatePartyAsync(editedParty);

            return new MethodResult(methodName, isPartyUpdated, string.Empty);
        }
        catch (ElementNotFoundException ex)
        {
            return new MethodResult(methodName, false,
                $"{ex.ElementName} with ID - {ex.ElementId} is not found");
        }
        catch (Exception ex)
        {
            return new MethodResult(methodName, false, $"Unknown error");
        }
    }

    public async Task<MethodResult<IEnumerable<PartyResponse>>> GetUserOrganizedPartiesAsync(Guid userId)
    {
        const string methodName = "GetUserOrganizedParties";

        try
        {
            var parties = await partyRepository.GetUserOrganizedPartiesAsync(userId);
            var partyResponses = parties.Select(mapper.Map<Models.Party, PartyResponse>).ToList();
            return new MethodResult<IEnumerable<PartyResponse>>(methodName, true, string.Empty, partyResponses);
        }
        catch (Exception e)
        {
            return new MethodResult<IEnumerable<PartyResponse>>(methodName, false, $"Unknown error");
        }
    }

    public async Task<MethodResult<IEnumerable<PartyResponse>>> GetUserMemberPartiesAsync(Guid userId)
    {
        const string methodName = "GetUserMemberParties";

        try
        {
            var parties = await partyRepository.GetUserMemberPartiesAsync(userId);
            var partyResponses = parties.Select(mapper.Map<Models.Party, PartyResponse>).ToList();
            return new MethodResult<IEnumerable<PartyResponse>>(methodName, true, string.Empty, partyResponses);
        }
        catch (Exception e)
        {
            return new MethodResult<IEnumerable<PartyResponse>>(methodName, false, $"Unknown error");
        }
    }

    public async Task<MethodResult> DeleteUserFromPartyAsync(Guid partyId, Guid userId)
    {
        const string methodName = "DeleteUserFromParty";

        try
        {
            var party = await partyRepository.GetPartyOrDefaultAsync(partyId);
            if (party == null)
                return new MethodResult(methodName, false, $"Party with id {partyId} doesn't exist");

            if (party.Organizer.Id == userId)
                return new MethodResult(methodName, false, $"Can't quit a party created by a user");

            var userToDelete = party.PartyMembers.FirstOrDefault(user => user.Id == userId);
            if (userToDelete == null)
                return new MethodResult(methodName, false, $"user is not a member of a party");

            party.PartyMembers.Remove(userToDelete);
            party.PartyEditors.Remove(userToDelete);

            var isPartyUpdated = await partyRepository.UpdatePartyAsync(party);

            return new MethodResult(methodName, isPartyUpdated, string.Empty);
        }
        catch (Exception e)
        {
            return new MethodResult(methodName, false, $"Unknown error");
        }
    }

    public async Task<MethodResult> AddUserToPartyAsync(Guid partyId, Guid userId)
    {
        const string methodName = "AddUserToParty";

        try
        {
            var party = await partyRepository.GetPartyOrDefaultAsync(partyId);
            if (party == null)
                return new MethodResult(methodName, false, $"Party with id {partyId} doesn't exist");

            if (party.Organizer.Id == userId)
                return new MethodResult(methodName, false, $"Can't enter a party created by a user");

            var isUserMemberOfParty = party.PartyMembers.FirstOrDefault(user => user.Id == userId) != null;
            if (isUserMemberOfParty)
                return new MethodResult(methodName, false, $"User is already a member of a party");

            var userToAdd = await userRepository.GetUserOrDefaultAsync(userId);
            if (userToAdd == null)
                return new MethodResult(methodName, false, $"User with id {userId} doesn't exist");

            party.PartyMembers.Add(userToAdd);

            var isPartyUpdated = await partyRepository.UpdatePartyAsync(party);

            return new MethodResult(methodName, isPartyUpdated, string.Empty);
        }
        catch (Exception e)
        {
            return new MethodResult(methodName, false, $"Unknown error");
        }
    }
}