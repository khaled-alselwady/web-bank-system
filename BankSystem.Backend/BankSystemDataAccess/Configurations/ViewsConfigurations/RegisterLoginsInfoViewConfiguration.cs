using BankSystemDataAccess.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystemDataAccess.Configurations.ViewsConfigurations
{
    public class RegisterLoginsInfoViewConfiguration : IEntityTypeConfiguration<RegisterLoginsInfoView>
    {
        public void Configure(EntityTypeBuilder<RegisterLoginsInfoView> builder)
        {
            builder
                .HasNoKey()
                .ToView("vw_RegisterLoginsInfo");

            builder.Property(e => e.DateTime).HasColumnType("datetime");
            builder.Property(e => e.Username).HasMaxLength(100);
        }
    }
}
