using BankSystemDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystemDataAccess.Configurations
{
    public class RegisterLoginConfiguration : IEntityTypeConfiguration<RegisterLogin>
    {
        public void Configure(EntityTypeBuilder<RegisterLogin> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Register__A13C533E51AEE68A");

            builder.HasIndex(e => e.UserId, "IX_RegisterLogins_UserId");

            builder.Property(e => e.RegisterLoginsDateTime).HasColumnType("datetime");

            builder.HasOne(d => d.User).WithMany(p => p.RegisterLogins)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK__RegisterL__UserI__02FC7413");
        }
    }
}
