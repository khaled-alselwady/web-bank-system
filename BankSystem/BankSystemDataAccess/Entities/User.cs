namespace BankSystemDataAccess.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public int Permissions { get; set; }

    public int PersonId { get; set; }

    public virtual Person Person { get; set; } = new();

    public virtual ICollection<RegisterLogin> RegisterLogins { get; set; } = new List<RegisterLogin>();

    public virtual ICollection<TransfersLog> TransfersLogs { get; set; } = new List<TransfersLog>();
}
