using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NicoKingdomManager.BusinessLayer.Services.Common;
using NicoKingdomManager.DataAccessLayer;
using NicoKingdomManager.Shared.Models;
using NicoKingdomManager.Shared.Models.Requests;
using Entities = NicoKingdomManager.DataAccessLayer.Entities;

namespace NicoKingdomManager.BusinessLayer.Services;

public class ServerLogsService : IServerLogsService
{
    private readonly IDataContext dataContext;
    private readonly IMapper mapper;

    public ServerLogsService(IDataContext dataContext, IMapper mapper)
    {
        this.dataContext = dataContext;
        this.mapper = mapper;
    }

    public async Task DeleteAsync(Guid id)
    {
        var dbLog = await dataContext.GetAsync<Entities.ServerLog>(id);
        dataContext.Delete(dbLog);
        await dataContext.SaveAsync();
    }
    public async Task<IEnumerable<ServerLog>> GetAsync(Guid? userId)
    {
        var query = dataContext.GetData<Entities.ServerLog>();

        if (userId != null && userId.GetValueOrDefault() != Guid.Empty)
        {
            query = query.Where(log => log.UserId == userId);
        }

        var dbLogs = await query.Include(log => log.User).ToListAsync();

        List<ServerLog> logs = new();

        foreach (var dbLog in dbLogs)
        {
            var log = mapper.Map<ServerLog>(dbLog);
            log.User = mapper.Map<User>(dbLog.User);
            logs.Add(log);
        }

        return logs;
    }
    public async Task<ServerLog> SaveAsync(SaveLogRequest request)
    {
        var logQuery = dataContext.GetData<Entities.ServerLog>(trackingChanges: true);
        var userQuery = dataContext.GetData<Entities.User>();
        var dbLog = request.Id != null ? await logQuery.FirstOrDefaultAsync(log => log.Id == request.Id) : null;
        var dbUser = await userQuery.FirstOrDefaultAsync(user => user.UserName == request.UserName);

        if (dbLog == null)
        {
            dbLog = mapper.Map<Entities.ServerLog>(request);
            dbLog.UserId = dbUser.Id;
            dataContext.Insert(dbLog);
        }
        else
        {
            mapper.Map(request, dbLog);
        }

        await dataContext.SaveAsync();

        ServerLog savedLog = mapper.Map<ServerLog>(dbLog);
        savedLog.User = mapper.Map<User>(dbUser);
        return savedLog;
    }
}