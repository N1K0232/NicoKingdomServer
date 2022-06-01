using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NicoKingdomManager.DataAccessLayer.Entities;
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
    /// removes a collection of <see cref="UserRole"/> from the UserRoles table
    /// </summary>
    /// <param name="entities">the list of <see cref="UserRole"/></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void Delete(IEnumerable<UserRole> entities)
    {
        ArgumentNullException.ThrowIfNull(entities, nameof(entities));
        Set<UserRole>().RemoveRange(entities);
    }

    /// <summary>
    /// finds an entity with the given primary key values
    /// </summary>
    /// <typeparam name="T"><see cref="BaseEntity"/></typeparam>
    /// <param name="keyValues">The values of the primary key for the entity to be found</param>
    /// <returns></returns>
    public ValueTask<T> GetAsync<T>(params object[] keyValues) where T : BaseEntity
    {
        return Set<T>().FindAsync(keyValues);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ignoreQueryFilters"></param>
    /// <param name="trackingChanges"></param>
    /// <returns></returns>
    public IQueryable<T> GetData<T>(bool ignoreQueryFilters = false, bool trackingChanges = false) where T : BaseEntity
    {
        var set = Set<T>().AsQueryable();

        if (ignoreQueryFilters)
        {
            set = set.IgnoreQueryFilters();
        }

        return trackingChanges ?
            set.AsTracking() :
            set.AsNoTrackingWithIdentityResolution();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    public void Insert<T>(T entity) where T : BaseEntity => InsertInternal(entity);

    /// <summary>
    /// inserts the user in the database and creates the many to many relationship
    /// </summary>
    /// <param name="user">the user that will be added in the database</param>
    /// <param name="roles">the roles of the user</param>
    public void Insert(User user, string[] roles)
    {
        InsertInternal(user);
        foreach (string roleName in roles.Distinct())
        {
            Role role = Set<Role>().First(r => r.Name == roleName);
            Set<UserRole>().Add(new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id,
                User = user,
                Role = role
            });
        }
    }

    /// <summary>
    /// the logic for adding the entity in the database
    /// </summary>
    /// <typeparam name="T"><see cref="BaseEntity"/></typeparam>
    /// <param name="entity">the entity that will be added in the database</param>
    private void InsertInternal<T>(T entity) where T : BaseEntity
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));
        Set<T>().Add(entity);
    }

    /// <summary>
    /// saves the changes in the database
    /// </summary>
    /// <returns>a task representing the current function</returns>
    public Task SaveAsync() => SaveChangesAsync();
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        List<EntityEntry> entries = ChangeTracker.Entries()
            .Where(e => e.GetType().IsSubclassOf(typeof(BaseEntity))).ToList();

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

        modelBuilder.Entity<UserRole>(builder =>
        {
            builder.ToTable("UserRoles");

            builder.HasKey(userRole => new { userRole.UserId, userRole.RoleId });

            builder.HasOne(userRole => userRole.User)
                    .WithMany(user => user.UserRoles)
                    .HasForeignKey(userRole => userRole.UserId)
                    .IsRequired();

            builder.HasOne(userRole => userRole.Role)
                    .WithMany(role => role.UserRoles)
                    .HasForeignKey(userRole => userRole.RoleId)
                    .IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}