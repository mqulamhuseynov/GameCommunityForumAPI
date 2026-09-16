using GamingForum.Application.IRepo.uow;
using GamingForum.Domain.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;


namespace GamingForum.Infrastructure.Contexts
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options),IUnitOfWork
    {
        private IDbContextTransaction? _transaction;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = now;
                    entry.Property(e => e.CreatedAt).IsModified = false; 
                }
            }

            return base.SaveChangesAsync(ct);
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            await Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction is null) return;
             
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            if(_transaction is null) return;

            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
}
