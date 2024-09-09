using AutoMapper;
using BankSystem.DTOs.UserDTOs;
using BankSystemDataAccess.Entities;

namespace BankSystem.Mappers.UserMappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDetailsDto>();

            CreateMap<CreateOrUpdateUserDto, User>()
            .ForMember(dest => dest.TransfersLogs, opt => opt.Ignore())
            .ForMember(dest => dest.RegisterLogins, opt => opt.Ignore());
        }
    }
}
