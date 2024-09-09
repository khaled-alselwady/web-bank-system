using BankSystemDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystemDataAccess.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {

            builder.HasKey(e => e.Id).HasName("PK__Clients__7AD04FF10A571613");

            builder.HasIndex(e => e.PersonId, "IX_Clients_PersonId");

            builder.HasIndex(e => e.PersonId, "UQ__Clients__AA2FFB84B567F7DF").IsUnique();

            builder.HasIndex(e => e.AccountNumber, "UQ__Clients__BE2ACD6FC14AB4AE").IsUnique();

            builder.Property(e => e.AccountNumber).HasMaxLength(10);
            builder.Property(e => e.Balance).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.PinCode).HasMaxLength(10);
            builder.Property(e => e.IsActive).HasDefaultValue(true);

            builder.HasOne(d => d.Person).WithOne(p => p.Client)
                .HasForeignKey<Client>(d => d.PersonId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK__Clients__Perso__398D8EEE");
        }
    }
}
