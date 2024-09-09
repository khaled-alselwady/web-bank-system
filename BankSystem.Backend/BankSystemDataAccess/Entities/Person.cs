
using BankSystemDataAccess.Enums;

namespace BankSystemDataAccess.Entities;

public partial class Person
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string FullName => $"{FirstName} {LastName}";

    public Gender Gender { get; set; }

    public string Phone { get; set; }

    public string? Email { get; set; }

    public Client? Client { get; set; }
    public User? User { get; set; }
}
