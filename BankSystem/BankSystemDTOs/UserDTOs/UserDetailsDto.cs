using BankSystemDTOs.PersonDTOs;

namespace BankSystem.DTOs.UserDTOs
{
    public record UserDetailsDto(
        int Id,
        string Username,
        string Password,
        int Permissions,
        bool IsActive,
        PersonDetailsDto Person);
}
