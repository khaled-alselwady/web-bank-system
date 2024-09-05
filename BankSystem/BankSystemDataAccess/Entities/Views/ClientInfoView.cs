namespace BankSystemDataAccess.Entities.Views;

public partial class ClientInfoView
{
    public int Id { get; set; }

    public string AccountNumber { get; set; }

    public string? FullName { get; set; }

    public string Gender { get; set; }

    public string Phone { get; set; }

    public string? Email { get; set; }

    public decimal Balance { get; set; }
}
