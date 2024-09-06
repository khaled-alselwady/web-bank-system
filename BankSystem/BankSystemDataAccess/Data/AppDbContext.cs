using BankSystemDataAccess.Configurations;
using BankSystemDataAccess.Entities;
using BankSystemDataAccess.Entities.Views;
using Microsoft.EntityFrameworkCore;

namespace BankSystemDataAccess.Data;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=BankSystem;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientConfiguration).Assembly);
        modelBuilder.UseCollation("Arabic_CI_AS");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
