using AutoMapper;
using BankSystem.DTOs.ClientDTOs;
using BankSystemBusiness.Services;
using BankSystemDataAccess.Data;
using BankSystemDataAccess.Entities;
using BankSystemDataAccess.Entities.Views;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BankSystem.Business.Services
{
    public class ClientService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly PersonService _personService;

        public ClientService(AppDbContext context, IMapper mapper, PersonService personService)
        {
            _context = context;
            _mapper = mapper;
            _personService = personService;
        }

        private async Task<ClientDetailsDto?> _FindAsync(Expression<Func<Client, bool>> predicate)
        {
            var client = await _context.Clients
                .Include(c => c.Person)
                .FirstOrDefaultAsync(predicate);

            if (client == null)
            {
                return null;
            }

            return _mapper.Map<ClientDetailsDto>(client);
        }

        private async Task<bool> _ExistsAsync(Expression<Func<Client, bool>> predicate)
            => await _context.Clients.AnyAsync(predicate);

        public async Task<List<ClientInfoView>?> AllAsync()
        {
            var clients = await _context.ClientsInfoView.ToListAsync();
            return clients;
        }

        public async Task<ClientDetailsDto?> FindByClientIdAsync(int id)
             => await _FindAsync(c => c.Id == id);

        public async Task<ClientDetailsDto?> FindByPersonIdAsync(int personId)
             => await _FindAsync(c => c.PersonId == personId);

        public async Task<ClientDetailsDto?> AddAsync(CreateOrUpdateClientDto newClientDto)
        {
            if (newClientDto == null)
            {
                return null;
            }

            // Map the Client entity from DTO and set the PersonId
            var client = _mapper.Map<Client>(newClientDto);
            if (client == null)
            {
                return null;
            }

            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();

            return _mapper.Map<ClientDetailsDto?>(client);
        }

        public async Task<ClientDetailsDto?> UpdateAsync(int id, CreateOrUpdateClientDto updatedClientDto)
        {
            if (updatedClientDto == null)
            {
                return null;
            }

            // Load the client along with the related person in a single database call
            var client = await _context.Clients
                .AsTracking()
                .Include(c => c.Person)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (client == null)
            {
                return null;
            }

            _mapper.Map(updatedClientDto, client);

            await _context.SaveChangesAsync();

            return _mapper.Map<ClientDetailsDto?>(client);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _context.Clients
                  .Where(c => c.Id == id)
                  .ExecuteUpdateAsync(set => set
                  .SetProperty(c => c.IsActive, false));

            return true;

            // Create a SqlParameter for the ClientId
            //var clientIdParam = new SqlParameter("@ClientId", id);

            //await _context.Database.ExecuteSqlRawAsync("EXEC sp_SoftDeleteClient @ClientId", clientIdParam);

            //return true;
        }

        public async Task<bool> ExistsByClientIdAsync(int id)
            => await _ExistsAsync(c => c.Id == id);

        public async Task<bool> ExistsByPersonIdAsync(int personId)
           => await _ExistsAsync(c => c.PersonId == personId);
    }
}
