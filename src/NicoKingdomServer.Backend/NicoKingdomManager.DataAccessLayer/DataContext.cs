using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NicoKingdomManager.DataAccessLayer.Entities;
using NicoKingdomManager.DataAccessLayer.Entities.Common;
using System.Reflection;

namespace NicoKingdomManager.DataAccessLayer;

public class DataContext : DbContext, IDataContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public void Delete<T>(T entity) where T : BaseEntity
    {
        ArgumentNullException.ThrowIfNull(entity);
        Set<T>().Remove(entity);
    }
    public void Delete(IEnumerable<UserRole> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);
        Set<UserRole>().RemoveRange(entities);
    }
    public ValueTask<T> GetAsync<T>(params object[] keyValues) where T : BaseEntity
    {
        return Set<T>().FindAsync(keyValues);
    }
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
    public void Insert<T>(T entity) where T : BaseEntity
    {
        ArgumentNullException.ThrowIfNull(entity);
        Set<T>().Add(entity);
    }
    public void Insert(User user, params string[] roles)
    {
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

        modelBuilder.Entity<UserRole>(userRole =>
        {
            userRole.ToTable("UserRoles");

            userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

            userRole.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

            userRole.HasOne(ur => ur.Role)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}