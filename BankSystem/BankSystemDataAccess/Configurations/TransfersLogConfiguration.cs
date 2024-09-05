using BankSystemDataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BankSystemDataAccess.Configurations
{
    public class TransfersLogConfiguration : IEntityTypeConfiguration<TransfersLog>
    {
        public void Configure(EntityTypeBuilder<TransfersLog> builder)
        {
            builder.HasKey(e => e.Id).HasName("PK_TransfersLog");

            builder.HasIndex(e => e.CreatedByUserId, "IX_TransfersLogs_CreatedByUserId");

            builder.HasIndex(e => e.DestinationClientId, "IX_TransfersLogs_DestinationClientId");

            builder.HasIndex(e => e.SourceClientId, "IX_TransfersLogs_SourceClientId");

            builder.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.Date).HasColumnType("datetime");
            builder.Property(e => e.DestinationBalance).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.SourceBalance).HasColumnType("decimal(18, 2)");

            builder.HasOne(d => d.CreatedByUser).WithMany(p => p.TransfersLogs)
                .HasForeignKey(d => d.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_TransfersLogs_Users");

            builder.HasOne(d => d.DestinationClient).WithMany(p => p.TransfersLogDestinationClients)
                .HasForeignKey(d => d.DestinationClientId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_TransfersLog_Clients1");

            builder.HasOne(d => d.SourceClient).WithMany(p => p.TransfersLogSourceClients)
                .HasForeignKey(d => d.SourceClientId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_TransfersLog_Clients");
        }
    }
}
