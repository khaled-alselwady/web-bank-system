using AutoMapper;
using BankSystem.DTOs.ClientDTOs;
using BankSystemDataAccess.Data;
using BankSystemDataAccess.Entities;
using BankSystemDataAccess.Entities.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BankSystem.Business.Services
{
    public class ClientService : BaseService<Client, ClientDetailsDto>
    {
        public ClientService(AppDbContext context, IMapper mapper, ILogger<ClientService> logger)
            : base(context, mapper, logger)
        {
        }

        protected override IQueryable<Client> IncludeDependencies(IQueryable<Client> query)
        {
            return query.Include(c => c.Person);
        }

        public async Task<List<ClientInfoView>?> GetAllClientsAsync()
            => await GetAllUsingViewAsync<ClientInfoView>();

        public async Task<ClientDetailsDto?> FindByClientIdAsync(int id)
            => await FindAsync(c => c.Id == id);

        public async Task<ClientDetailsDto?> FindByPersonIdAsync(int personId)
            => await FindAsync(c => c.PersonId == personId);

        public async Task<ClientDetailsDto?> AddClientAsync(CreateOrUpdateClientDto newClientDto)
            => await AddAsync(newClientDto);

        public async Task<ClientDetailsDto?> UpdateClientAsync(int id, CreateOrUpdateClientDto updatedClientDto)
            => await UpdateAsync(updatedClientDto, (query) => query.FirstOrDefaultAsync(c => c.Id == id));

        public async Task<bool> DeleteClientAsync(int id)
            => await SoftDeleteAsync(c => c.Id == id);

        public async Task<bool> ExistsByClientIdAsync(int id)
            => await ExistsAsync(c => c.Id == id);

        public async Task<bool> ExistsByPersonIdAsync(int personId)
            => await ExistsAsync(c => c.PersonId == personId);
    }

}
