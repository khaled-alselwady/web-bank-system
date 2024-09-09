using BankSystemDTOs.PersonDTOs;

namespace BankSystem.DTOs.UserDTOs
{
    public record CreateOrUpdateUserDto(
        string Username,
        string Password,
        int Permissions,
        bool IsActive,
        CreateOrUpdatePersonDto Person
        );
}
