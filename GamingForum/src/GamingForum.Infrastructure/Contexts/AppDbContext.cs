using GamingForum.Application.IRepo.UoW;
using GamingForum.Domain.Entities.ForumEntities;
using GamingForum.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace GamingForum.Infrastructure.Contexts
{
    public class AppDbContext(DbContextOptions<AppDbContext> options)
        : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>(options), IUnitOfWork
    {
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Forum> Forums => Set<Forum>();
        public DbSet<Game> Games => Set<Game>();
        public DbSet<Topic> Topics => Set<Topic>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<UserFollow> UserFollows => Set<UserFollow>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base() birinci qalmalıdır: Identity cədvəllərini o konfiqurasiya edir
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        // audit və soft delete AuditableEntityInterceptor-dadır

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (Database.CurrentTransaction is not null)
                throw new InvalidOperationException("A transaction is already in progress.");

            await Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = Database.CurrentTransaction
                ?? throw new InvalidOperationException("No transaction in progress.");

            await transaction.CommitAsync(cancellationToken);
            await transaction.DisposeAsync();
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            var transaction = Database.CurrentTransaction;
            if (transaction is null) return;

            await transaction.RollbackAsync(cancellationToken);
            await transaction.DisposeAsync();
        }
    }
}
