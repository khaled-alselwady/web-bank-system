namespace BankSystemDataAccess.Entities;

public partial class Person
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string FullName => $"{FirstName} {LastName}";

    /// <summary>
    /// 0  =&gt;  Male               
    /// 1  =&gt;  Female
    /// </summary>
    public byte Gender { get; set; }

    public string Phone { get; set; }

    public string? Email { get; set; }
}
