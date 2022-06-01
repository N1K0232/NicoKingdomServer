using NicoKingdomManager.DataAccessLayer.Entities;
using NicoKingdomManager.DataAccessLayer.Entities.Common;

namespace NicoKingdomManager.DataAccessLayer;

public interface IDataContext
{
    void Delete<T>(T entity) where T : BaseEntity;
    void Delete(IEnumerable<UserRole> entities);
    IQueryable<T> GetData<T>(bool ignoreQueryFilters = false, bool trackingChanges = false) where T : BaseEntity;
    ValueTask<T> GetAsync<T>(params object[] keyValues) where T : BaseEntity;
    void Insert<T>(T entity) where T : BaseEntity;
    void Insert(User user, params string[] roles);
    Task SaveAsync();
}