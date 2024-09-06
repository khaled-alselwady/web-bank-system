namespace BankSystemDataAccess.Entities;

public partial class RegisterLogin
{
    public int Id { get; set; }

    public DateTime RegisterLoginsDateTime { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = new();
}
