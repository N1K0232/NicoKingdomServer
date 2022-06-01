using NicoKingdomManager.Shared.Models;
using NicoKingdomManager.Shared.Models.Common;
using NicoKingdomManager.Shared.Models.Requests;

namespace NicoKingdomManager.BusinessLayer.Services.Common;

public interface IUsersService
{
    Task DeleteAsync(Guid id);
    Task<ListResult<User>> GetAsync(int pageIndex, int itemsPerPage);
    Task<User> SaveAsync(SaveUserRequest request);
}