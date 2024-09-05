namespace BankSystemDataAccess.Entities;

public partial class Client
{
    public int Id { get; set; }

    public string AccountNumber { get; set; }

    public string PinCode { get; set; }

    public decimal Balance { get; set; }

    public int PersonId { get; set; }

    public virtual Person Person { get; set; } = new();

    public virtual ICollection<TransfersLog> TransfersLogDestinationClients { get; set; } = [];

    public virtual ICollection<TransfersLog> TransfersLogSourceClients { get; set; } = [];
}
