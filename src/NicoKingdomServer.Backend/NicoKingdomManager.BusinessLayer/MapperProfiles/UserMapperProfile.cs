using AutoMapper;
using NicoKingdomManager.Shared.Models;
using NicoKingdomManager.Shared.Models.Requests;
using Entities = NicoKingdomManager.DataAccessLayer.Entities;

namespace NicoKingdomManager.BusinessLayer.MapperProfiles;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<Entities.User, User>();
        CreateMap<SaveUserRequest, Entities.User>();
    }
}