namespace BankSystemDataAccess.Entities.Views;

public partial class UserInfoView
{
    public int Id { get; set; }

    public string Username { get; set; }

    public string? FullName { get; set; }

    public string Gender { get; set; }

    public string Phone { get; set; }

    public string? Email { get; set; }
}
