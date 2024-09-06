using AutoMapper;
using BankSystemDataAccess.Entities;
using BankSystemDTOs.PersonDTOs;

namespace BankSystemBusiness.Mappers.PersonMapper
{
    public class MappingPerson : Profile
    {
        public MappingPerson()
        {
            CreateMap<Person, PersonDetailsDto>();

            CreateMap<CreateOrUpdatePersonDto, Person>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom<GenderValueResolver>());
        }
    }
}
