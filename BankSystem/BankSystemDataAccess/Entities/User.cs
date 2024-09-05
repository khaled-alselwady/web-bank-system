using System;
using System.Collections.Generic;

namespace BankSystemDataAccess.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Permissions { get; set; }

    public int PersonId { get; set; }

    public virtual Person Person { get; set; } = null!;

    public virtual ICollection<RegisterLogin> RegisterLogins { get; set; } = new List<RegisterLogin>();

    public virtual ICollection<TransfersLog> TransfersLogs { get; set; } = new List<TransfersLog>();
}
