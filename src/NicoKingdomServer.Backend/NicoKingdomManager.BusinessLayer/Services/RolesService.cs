using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NicoKingdomManager.BusinessLayer.Services.Common;
using NicoKingdomManager.DataAccessLayer;
using NicoKingdomManager.Shared.Models;
using NicoKingdomManager.Shared.Models.Requests;
using Entities = NicoKingdomManager.DataAccessLayer.Entities;

namespace NicoKingdomManager.BusinessLayer.Services;

public class RolesService : IRolesService
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public RolesService(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    public async Task DeleteAsync(Guid id)
    {
        var dbRole = await dataContext.GetAsync<Entities.Role>(id);
        dataContext.Delete(dbRole);
        await dataContext.SaveAsync();
    }
    public async Task<IEnumerable<Role>> GetAsync(string name)
    {
        var query = dataContext.GetData<Entities.Role>();

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(r => r.Name.Contains(name));
        }

        var dbRoles = await query.ToListAsync();
        var roles = mapper.Map<List<Role>>(dbRoles);
        return roles;
    }
    public async Task<Role> SaveAsync(SaveRoleRequest request)
    {
        var query = dataContext.GetData<Entities.Role>(trackingChanges: true);
        var dbRole = request.Id != null ? await query.FirstAsync(role => role.Id == request.Id) : null;

        if (dbRole == null)
        {
            dbRole = mapper.Map<Entities.Role>(request);
            dataContext.Insert(dbRole);
        }
        else
        {
            mapper.Map(request, dbRole);
        }

        await dataContext.SaveAsync();
        return mapper.Map<Role>(dbRole);
    }
}