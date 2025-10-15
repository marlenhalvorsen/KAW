using KAW.Application.Interfaces;
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

            modelBuilder.Entity<UserExpression>(entity =>
            {
                entity.HasKey(e => e.Id);       //set id as pk
                entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsRequired();                  //name is required
                entity.HasIndex(e => e.Name).IsUnique(); //name is also supposed to be unique
            });
        }
        public Task<int> SaveChangesAsync(CancellationToken ct = default)
                => base.SaveChangesAsync(ct);
    }
}
