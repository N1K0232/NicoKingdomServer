using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NicoKingdomManager.BusinessLayer.Services.Common;
using NicoKingdomManager.DataAccessLayer;
using NicoKingdomManager.Shared.Models;
using NicoKingdomManager.Shared.Models.Common;
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
        dataContext.Delete(user.UserRoles);
        await dataContext.SaveAsync();
    }
    public async Task<ListResult<User>> GetAsync(int pageIndex, int itemsPerPage)
    {
        var query = dataContext.GetData<Entities.User>().Include(u => u.UserRoles);
        int totalCount = await query.CountAsync();
        var dbUsers = await query.Skip(pageIndex * itemsPerPage)
            .Take(itemsPerPage + 1)
            .ToListAsync();
        var users = mapper.Map<List<User>>(dbUsers);

        if (totalCount > 1)
        {
            return new ListResult<User>(users.Take(itemsPerPage), totalCount, totalCount > itemsPerPage);
        }
        return new ListResult<User>(users);
    }
    public async Task<User> GetAsync(string nickName)
    {
        var query = dataContext.GetData<Entities.User>().Include(u => u.UserRoles);
        var dbUser = await query.FirstAsync();
        var user = mapper.Map<User>(dbUser);
        return user;
    }
    public async Task<User> SaveAsync(SaveUserRequest request)
    {
        var query = dataContext.GetData<Entities.User>(trackingChanges: true);
        var dbUser = request.Id != null ?
            await query.FirstOrDefaultAsync(u => u.Id == request.Id) : null;

        if (dbUser == null)
        {
            dbUser = mapper.Map<Entities.User>(request);
            dbUser.NickName = request.NickName ?? request.UserName;
            dataContext.Insert(dbUser, request.Roles);
        }
        else
        {
            mapper.Map(request, dbUser);
        }

        await dataContext.SaveAsync();
        return mapper.Map<User>(dbUser);
    }
}