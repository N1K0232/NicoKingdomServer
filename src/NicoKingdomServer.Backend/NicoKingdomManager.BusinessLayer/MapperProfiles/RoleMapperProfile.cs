using AutoMapper;
using NicoKingdomManager.Shared.Models;
using NicoKingdomManager.Shared.Models.Requests;
using Entities = NicoKingdomManager.DataAccessLayer.Entities;

namespace NicoKingdomManager.BusinessLayer.MapperProfiles;

public class RoleMapperProfile : Profile
{
    public RoleMapperProfile()
    {
        CreateMap<Entities.Role, Role>();
        CreateMap<SaveRoleRequest, Entities.Role>();
    }
}