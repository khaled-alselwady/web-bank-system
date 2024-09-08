using BankSystemDTOs.PersonDTOs;

namespace BankSystem.DTOs.ClientDTOs
{
    public record CreateOrUpdateClientDto(
        string AccountNumber,
        string PinCode,
        decimal Balance,
        bool IsActive,
        CreateOrUpdatePersonDto Person);
}
