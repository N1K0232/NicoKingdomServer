using NicoKingdomManager.Shared.Models;
using NicoKingdomManager.Shared.Models.Requests;

namespace NicoKingdomManager.BusinessLayer.Services.Common;

public interface IUsersService
{
    Task DeleteAsync(Guid id);
    Task<IEnumerable<User>> GetAsync(string nickName);
    Task<User> SaveAsync(SaveUserRequest request);
}