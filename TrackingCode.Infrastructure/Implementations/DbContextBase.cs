using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrackingCode.Domain.Interfaces;

namespace TrackingCode.Infrastructure.Implementations
{
    public abstract class DbContextBase : DbContext, IUnitOfWork
    {

        protected DbContextBase(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> CompleteAsync(CancellationToken cancellationToken = default)
        {
            return await SaveChangesAsync(cancellationToken) > 0;
        }
    }
}