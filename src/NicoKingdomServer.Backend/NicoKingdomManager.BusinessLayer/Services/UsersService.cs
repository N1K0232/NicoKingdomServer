using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NicoKingdomManager.BusinessLayer.Services.Common;
using NicoKingdomManager.DataAccessLayer;
using NicoKingdomManager.Shared.Models;
using NicoKingdomManager.Shared.Models.Requests;
using Entities = NicoKingdomManager.DataAccessLayer.Entities;

namespace NicoKingdomManager.BusinessLayer.Services;

public class UsersService : IUsersService
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public UsersService(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await dataContext.GetAsync<Entities.User>(id);
        dataContext.Delete(user);
        await dataContext.SaveAsync();
    }
    public async Task<IEnumerable<User>> GetAsync(string nickName)
    {
        var query = dataContext.GetData<Entities.User>();
        if (!string.IsNullOrWhiteSpace(nickName))
        {
            query = query.Where(u => u.NickName == nickName);
        }

        var dbUsers = await query.ToListAsync();
        var users = mapper.Map<List<User>>(dbUsers);
        return users;
    }
    public async Task<User> SaveAsync(SaveUserRequest request)
    {
        var userQuery = dataContext.GetData<Entities.User>(trackingChanges: true);
        var roleQuery = dataContext.GetData<Entities.Role>();
        var dbUser = request.Id != null ?
            await userQuery.FirstOrDefaultAsync(user => user.Id == request.Id) : null;
        var dbRole = await roleQuery.FirstOrDefaultAsync(role => role.Name == request.Role);

        if (dbUser == null)
        {
            dbUser = mapper.Map<Entities.User>(request);
            dbUser.NickName = request.NickName ?? request.UserName;
            dbUser.RoleId = dbRole.Id;
            dbUser.Role = dbRole;
            dataContext.Insert(dbUser);
        }
        else
        {
            mapper.Map(request, dbUser);
            dbUser.RoleId = dbRole.Id;
            dbUser.Role = dbRole;
        }

        await dataContext.SaveAsync();
        return mapper.Map<User>(dbUser);
    }
}