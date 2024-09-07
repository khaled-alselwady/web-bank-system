using BankSystemDTOs.PersonDTOs;

namespace BankSystem.DTOs.ClientDTOs
{
    public record CreateOrUpdateClientDto(
        CreateOrUpdatePersonDto PersonDto,
        string AccountNumber,
        string PinCode,
        decimal Balance,
        bool IsActive);
}
