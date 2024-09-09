namespace BankSystemDTOs.PersonDTOs
{
    //public record PersonDetailsDto(int Id, string FullName, string Gender, string Phone, string? Email);
    public record PersonDetailsDto(int Id, string FirstName, string LastName, string FullName, string Gender, string Phone, string? Email);
}
