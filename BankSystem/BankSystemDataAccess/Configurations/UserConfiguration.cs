using BankSystemDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystemDataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Users__1788CCACAB45311C");

            builder.HasIndex(e => e.PersonId, "IX_Users_PersonId");

            builder.HasIndex(e => e.PersonId, "UQ__Users__AA2FFB84DC0D1567").IsUnique();

            builder.HasIndex(e => e.Username, "UQ__Users__C9F28456AD961997").IsUnique();

            builder.Property(e => e.Password).HasMaxLength(255);
            builder.Property(e => e.Username).HasMaxLength(100);

            builder.HasOne(d => d.Person).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.PersonId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK__Users__PersonID__3C69FB99");
        }
    }
}
