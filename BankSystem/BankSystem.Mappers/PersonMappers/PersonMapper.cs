using AutoMapper;
using BankSystemDataAccess.Entities;
using BankSystemDTOs.PersonDTOs;

namespace BankSystem.Mappers.PersonMappers
{
    public class PersonMapper : Profile
    {
        public PersonMapper()
        {
            CreateMap<Person, PersonDetailsDto>();

            CreateMap<CreateOrUpdatePersonDto, Person>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom<GenderValueResolver>());
        }
    }
}
