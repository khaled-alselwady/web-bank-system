using BankSystemDTOs.PersonDTOs;

namespace BankSystem.DTOs.ClientDTOs
{
    public record ClientDetailsDto(
        int Id,
        string AccountNumber,
        string PinCode,
        decimal Balance,
        bool IsActive,
        PersonDetailsDto Person
        );
}
