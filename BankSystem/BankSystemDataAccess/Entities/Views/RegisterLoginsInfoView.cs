namespace BankSystemDataAccess.Entities.Views;

public partial class RegisterLoginsInfoView
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public DateTime DateTime { get; set; }
}
