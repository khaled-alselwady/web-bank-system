using BankSystemDataAccess.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystemDataAccess.Configurations.ViewsConfigurations
{
    public class UserInfoViewConfiguration : IEntityTypeConfiguration<UserInfoView>
    {
        public void Configure(EntityTypeBuilder<UserInfoView> builder)
        {
            builder
                .HasNoKey()
                .ToView("vw_UserInfo");

            builder.Property(e => e.Email).HasMaxLength(200);
            builder.Property(e => e.FullName).HasMaxLength(200);
            builder.Property(e => e.Phone).HasMaxLength(20);
            builder.Property(e => e.Username).HasMaxLength(100);
            builder.Property(e => e.Status).HasMaxLength(10);
        }
    }
}
