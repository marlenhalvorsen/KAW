using KAW.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KAW.Infrastructure.Persistence.Configurations
{
    public class UserExpressionConfiguration : IEntityTypeConfiguration<UserExpression>
    {
        public void Configure(EntityTypeBuilder<UserExpression> builder)
        {
                builder.HasKey(e => e.Id);       //set id as pk
                builder.Property(e => e.Name)
                .HasMaxLength(200)
                .IsRequired();                  //name is required
                builder.HasIndex(e => e.Name).IsUnique(); //name is also supposed to be unique
           
        }
    }
}
