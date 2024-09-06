using BankSystemDataAccess.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystemDataAccess.Configurations.ViewsConfigurations
{
    public class ClientInfoViewConfiguration : IEntityTypeConfiguration<ClientInfoView>
    {
        public void Configure(EntityTypeBuilder<ClientInfoView> builder)
        {
            builder
                .HasNoKey()
                .ToView("vw_ClientInfo");

            builder.Property(e => e.AccountNumber).HasMaxLength(10);
            builder.Property(e => e.Balance).HasColumnType("decimal(18, 0)");
            builder.Property(e => e.Email).HasMaxLength(200);
            builder.Property(e => e.FullName).HasMaxLength(200);
            builder.Property(e => e.Phone).HasMaxLength(20);
        }
    }
}
