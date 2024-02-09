using AutoMapper;
using PartiesApi.DTO.DressCode;
using PartiesApi.DTO.Party;
using PartiesApi.DTO.PartyRule;
using PartiesApi.DTO.User;
using PartiesApi.Models;

namespace PartiesApi.Mapping;

internal class AutoMappingProfile : Profile
{
    public AutoMappingProfile()
    {
        CreateMap<DressCode, DressCodeResponse>();
        CreateMap<User, UserResponse>();
        CreateMap<PartyRule, PartyRuleResponse>();
        CreateMap<Party, PartyResponse>();
    }
}