using KAW.Application.Ports.Outbound;
using KAW.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace KAW.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<UserExpression> UserExpressions { get; set; } = null!; 

        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
        public Task<int> SaveChangesAsync(CancellationToken ct = default)
                => base.SaveChangesAsync(ct);
    }
}
