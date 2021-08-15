using Microsoft.EntityFrameworkCore;
using TrackingCode.Domain.Models;
using TrackingCode.Infrastructure.Implementations;

namespace TrackingCode.Infrastructure
{
    public class TrackingCodeDbContext : DbContextBase
    {
        public DbSet<TrackingCodeModel> TrackingCodeModels { get; set; }
        
        public TrackingCodeDbContext(DbContextOptions options) :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TrackingCodeModel>().HasKey(x => x.Id);
        }
    }
}