namespace BankSystemDataAccess.Entities;

public partial class TransfersLog
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int SourceClientId { get; set; }

    public int DestinationClientId { get; set; }

    public decimal Amount { get; set; }

    public decimal SourceBalance { get; set; }

    public decimal DestinationBalance { get; set; }

    public int CreatedByUserId { get; set; }

    public virtual User CreatedByUser { get; set; } = new();

    public virtual Client DestinationClient { get; set; } = new();

    public virtual Client SourceClient { get; set; } = new();
}
