using AutoMapper;
using BankSystemDataAccess.Entities;
using BankSystemDataAccess.Enums;
using BankSystemDTOs.PersonDTOs;

namespace BankSystem.Mappers.PersonMappers
{
    public class GenderValueResolver : IValueResolver<CreateOrUpdatePersonDto, Person, Gender>
    {
        public Gender Resolve(CreateOrUpdatePersonDto source, Person destination, Gender destMember, ResolutionContext context)
        {
            return Enum.TryParse<Gender>(source.Gender, ignoreCase: true, out var gender)
                ? gender
                : Gender.Male; // Default value if parsing fails
        }
    }
}
