using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NicoKingdomManager.DataAccessLayer.Entities.Common;
using System.Reflection;

namespace NicoKingdomManager.DataAccessLayer;

public class DataContext : DbContext, IDataContext, IDisposable
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    /// <summary>
    /// removes an entity from the database
    /// </summary>
    /// <typeparam name="T"><see cref="BaseEntity"/></typeparam>
    /// <param name="entity">the entity that will be removed from the database</param>
    /// <exception cref="ArgumentNullException"></exception>
    public void Delete<T>(T entity) where T : BaseEntity
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        Set<T>().Remove(entity);
    }

    /// <summary>
    /// finds an entity with the given primary key values
    /// </summary>
    /// <typeparam name="T"><see cref="BaseEntity"/></typeparam>
    /// <param name="keyValues">The values of the primary key for the entity to be found</param>
    /// <returns>a task representing the current operation</returns>
    public ValueTask<T> GetAsync<T>(params object[] keyValues) where T : BaseEntity
    {
        return Set<T>().FindAsync(keyValues);
    }

    /// <summary>
    /// gets the <see cref="IQueryable{T}"/> object representing the entity of the database
    /// </summary>
    /// <typeparam name="T"><see cref="BaseEntity"/></typeparam>
    /// <param name="ignoreQueryFilters">true if the filters should be ignored otherwise false. default: false</param>
    /// <param name="trackingChanges">true if the entity should be modified otherwise false. default: false</param>
    /// <returns>the query with the link to the table</returns>
    public IQueryable<T> GetData<T>(bool ignoreQueryFilters = false, bool trackingChanges = false) where T : BaseEntity
    {
        var set = Set<T>().AsQueryable<T>();

        if (ignoreQueryFilters)
        {
            set = set.IgnoreQueryFilters();
        }

        return trackingChanges ?
            set.AsTracking() :
            set.AsNoTrackingWithIdentityResolution();
    }

    /// <summary>
    /// inserts entity in the database
    /// </summary>
    /// <typeparam name="T"><see cref="BaseEntity"/></typeparam>
    /// <param name="entity">the entity that will be added in the database</param>
    public void Insert<T>(T entity) where T : BaseEntity
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        Set<T>().Add(entity);
    }

    /// <summary>
    /// saves the changes in the database
    /// </summary>
    /// <returns>a task representing the current operation</returns>
    public Task SaveAsync() => SaveChangesAsync();
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        List<EntityEntry> entries = ChangeTracker.Entries()
            .Where(e => e.Entity.GetType().IsSubclassOf(typeof(BaseEntity))).ToList();

        foreach (EntityEntry entry in entries.Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
        {
            BaseEntity entity = (BaseEntity)entry.Entity;
            if (entry.State == EntityState.Added)
            {
                //I don't need to set the id of the entity
                //because the id is automatically calculated
                //by the sql server environment
                entity.CreatedDate = DateTime.UtcNow;
                entity.LastModifiedDate = null;
            }
            if (entry.State == EntityState.Modified)
            {
                entity.LastModifiedDate = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}