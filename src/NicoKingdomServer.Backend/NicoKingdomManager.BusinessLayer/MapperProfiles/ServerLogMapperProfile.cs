using AutoMapper;
using NicoKingdomManager.Shared.Models;
using NicoKingdomManager.Shared.Models.Requests;
using Entities = NicoKingdomManager.DataAccessLayer.Entities;

namespace NicoKingdomManager.BusinessLayer.MapperProfiles;

public class ServerLogMapperProfile : Profile
{
    public ServerLogMapperProfile()
    {
        CreateMap<Entities.ServerLog, ServerLog>();
        CreateMap<SaveLogRequest, Entities.ServerLog>();
    }
}