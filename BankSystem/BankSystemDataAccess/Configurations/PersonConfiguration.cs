using BankSystemDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystemDataAccess.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK__Persons__AA2FFB8516B9919B");

            builder.Property(e => e.Email).HasMaxLength(200);
            builder.Property(e => e.FirstName).HasMaxLength(100);
            builder.Property(e => e.Gender).HasComment("0  =>  Male               \r\n1  =>  Female");
            builder.Property(e => e.LastName).HasMaxLength(100);
            builder.Property(e => e.Phone).HasMaxLength(20);
        }
    }
}
