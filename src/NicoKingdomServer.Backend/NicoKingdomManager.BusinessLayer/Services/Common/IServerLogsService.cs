using NicoKingdomManager.Shared.Models;
using NicoKingdomManager.Shared.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NicoKingdomManager.BusinessLayer.Services.Common
{
    public interface IServerLogsService
    {
        Task DeleteAsync(Guid id);
        Task<IEnumerable<ServerLog>> GetAsync(Guid? userId);
        Task<ServerLog> SaveAsync(SaveLogRequest request);
    }
}
