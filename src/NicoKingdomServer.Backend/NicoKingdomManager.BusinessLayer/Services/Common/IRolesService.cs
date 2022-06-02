using NicoKingdomManager.Shared.Models;
using NicoKingdomManager.Shared.Models.Requests;

namespace NicoKingdomManager.BusinessLayer.Services.Common;

public interface IRolesService
{
    Task DeleteAsync(Guid id);
    Task<IEnumerable<Role>> GetAsync(string name);
    Task<Role> SaveAsync(SaveRoleRequest request);
}