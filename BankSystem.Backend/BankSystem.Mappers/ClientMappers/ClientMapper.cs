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
           .ForMember(dest => dest.TransfersLogDestinationClients, opt => opt.Ignore())
           .ForMember(dest => dest.TransfersLogSourceClients, opt => opt.Ignore());
        }
    }
}
