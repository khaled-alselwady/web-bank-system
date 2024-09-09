using BankSystemDataAccess.Configurations;
using BankSystemDataAccess.Entities;
using BankSystemDataAccess.Entities.Views;
using Microsoft.EntityFrameworkCore;

namespace BankSystemDataAccess.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Person> People { get; set; }
    public virtual DbSet<Client> Clients { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<RegisterLogin> RegisterLogins { get; set; }
    public virtual DbSet<TransfersLog> TransfersLogs { get; set; }
    public virtual DbSet<ClientInfoView> ClientsInfoView { get; set; }
    public virtual DbSet<UserInfoView> UsersInfoView { get; set; }
    public virtual DbSet<RegisterLoginsInfoView> RegisterLoginsInfoView { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientConfiguration).Assembly);
        modelBuilder.UseCollation("Arabic_CI_AS");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
