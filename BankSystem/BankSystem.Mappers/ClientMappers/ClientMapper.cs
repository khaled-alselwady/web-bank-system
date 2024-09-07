using AutoMapper;
using BankSystem.DTOs.ClientDTOs;
using BankSystemDataAccess.Entities;

namespace BankSystem.Mappers.ClientMappers
{
    public class ClientMapper : Profile
    {
        public ClientMapper()
        {
            CreateMap<Client, ClientDetailsDto>();

            CreateMap<CreateOrUpdateClientDto, Client>()
            .ForMember(dest => dest.Person, opt => opt.Ignore()) // Temporarily ignore Person mapping
            .ForMember(dest => dest.TransfersLogDestinationClients, opt => opt.Ignore())
            .ForMember(dest => dest.TransfersLogSourceClients, opt => opt.Ignore())
            .AfterMap((src, dest, context) =>
            {
                if (src.PersonDto != null)
                {
                    if (dest.Person == null)
                    {
                        dest.Person = new Person();
                    }
                    context.Mapper.Map(src.PersonDto, dest.Person);
                }
            });
        }
    }
}
