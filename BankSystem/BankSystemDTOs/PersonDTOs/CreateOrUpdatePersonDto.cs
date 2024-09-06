namespace BankSystemDTOs.PersonDTOs
{
    public record CreateOrUpdatePersonDto(
    string FirstName,
    string LastName,
    string Gender,
    string Phone,
    string? Email
    );

}
