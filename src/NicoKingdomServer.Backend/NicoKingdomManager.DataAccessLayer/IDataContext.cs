using NicoKingdomManager.DataAccessLayer.Entities.Common;

namespace NicoKingdomManager.DataAccessLayer;

public interface IDataContext
{
    /// <summary>
    /// removes an entity from the database
    /// </summary>
    /// <typeparam name="T"><see cref="BaseEntity"/></typeparam>
    /// <param name="entity">the entity that will be removed from the database</param>
    /// <exception cref="ArgumentNullException"></exception>
    void Delete<T>(T entity) where T : BaseEntity;

    /// <summary>
    /// gets the <see cref="IQueryable{T}"/> object representing the entity of the database
    /// </summary>
    /// <typeparam name="T"><see cref="BaseEntity"/></typeparam>
    /// <param name="ignoreQueryFilters">true if the filters should be ignored otherwise false. default: false</param>
    /// <param name="trackingChanges">true if the entity should be modified otherwise false. default: false</param>
    /// <returns>the query with the link to the table</returns>
    IQueryable<T> GetData<T>(bool ignoreQueryFilters = false, bool trackingChanges = false) where T : BaseEntity;

    /// <summary>
    /// finds an entity with the given primary key values
    /// </summary>
    /// <typeparam name="T"><see cref="BaseEntity"/></typeparam>
    /// <param name="keyValues">The values of the primary key for the entity to be found</param>
    /// <returns>a task representing the current operation</returns>
    ValueTask<T> GetAsync<T>(params object[] keyValues) where T : BaseEntity;

    /// <summary>
    /// inserts entity in the database
    /// </summary>
    /// <typeparam name="T"><see cref="BaseEntity"/></typeparam>
    /// <param name="entity">the entity that will be added in the database</param>
    void Insert<T>(T entity) where T : BaseEntity;

    /// <summary>
    /// saves the changes in the database
    /// </summary>
    /// <returns>a task representing the current operation</returns>
    Task SaveAsync();

    /// <summary>
    /// commits the changes in a database
    /// </summary>
    /// <param name="action">the action to perform</param>
    /// <returns></returns>
    Task ExecuteTransactionAsync(Func<Task> action);
}